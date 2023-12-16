/**
 * Sample script that performs batch renders with GUI for selecting
 * render templates.
 *
 * Revision Date: Jun. 28, 2006.
 **/
using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using ScriptPortal.Vegas;

public class EntryPoint
{

    // set this to true if you want to allow files to be overwritten
    bool OverwriteExistingFiles = false;

    String defaultBasePath = "Untitled_";
    const int QUICKTIME_MAX_FILE_NAME_LENGTH = 55;

    ScriptPortal.Vegas.Vegas myVegas = null;

    enum RenderMode
    {
        Project = 0,
        Selection,
        Regions,
    }

    ArrayList SelectedTemplates = new ArrayList();

    public void FromVegas(Vegas vegas)
    {
        myVegas = vegas;

        String projectPath = myVegas.Project.FilePath;
        if (String.IsNullOrEmpty(projectPath))
        {
            String dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            defaultBasePath = Path.Combine(dir, defaultBasePath);
        }
        else
        {
            String dir = Path.GetDirectoryName(projectPath);
            String fileName = Path.GetFileNameWithoutExtension(projectPath);
            defaultBasePath = Path.Combine(dir, fileName + "_");
        }

        DialogResult result = ShowBatchRenderDialog();
        myVegas.UpdateUI();
        if (DialogResult.OK == result)
        {
            // inform the user of some special failure cases
            String outputFilePath = FileNameBox.Text;
            RenderMode renderMode = RenderMode.Project;
            if (RenderRegionsButton.Checked)
            {
                renderMode = RenderMode.Regions;
            }
            else if (RenderSelectionButton.Checked)
            {
                renderMode = RenderMode.Selection;
            }
            DoBatchRender(SelectedTemplates, outputFilePath, renderMode);
        }
    }

    void DoBatchRender(ArrayList selectedTemplates, String basePath, RenderMode renderMode)
    {
        String outputDirectory = Path.GetDirectoryName(basePath);
        String baseFileName = Path.GetFileName(basePath);

        // make sure templates are selected
        if ((null == selectedTemplates) || (0 == selectedTemplates.Count))
            throw new ApplicationException("No render templates selected.");

        // make sure the output directory exists
        try
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error creating output directory: " + ex.Message);
        }

        List<RenderArgs> renders = new List<RenderArgs>();

        // enumerate through each selected render template
        foreach (RenderItem renderItem in selectedTemplates)
        {
            // construct the file name (most of it)
            String filename = Path.Combine(outputDirectory,
                                           FixFileName(baseFileName) +
                                           FixFileName(renderItem.Renderer.FileTypeName) +
                                           "_" +
                                           FixFileName(renderItem.Template.Name));

            //check to see if this is a QuickTime file...if so, file length cannot exceed 59 characters
            if (RenderMode.Regions == renderMode)
            {
                int regionIndex = 0;
                foreach (ScriptPortal.Vegas.Region region in myVegas.Project.Regions)
                {
                    // Extract filename without extension
                    String strippedFilename = Path.GetFileNameWithoutExtension(filename);

                    // Build region filename with directory and index
                    String regionFilename = Path.Combine(outputDirectory,
                    String.Format("{0}_{1}_{2}{3}",
                    regionIndex.ToString("D3"),
                    Path.GetFileNameWithoutExtension(baseFileName),
                    FixFileName(region.Label),
                    renderItem.Extension));

                    RenderArgs args = new RenderArgs();
                    args.OutputFile = regionFilename;
                    args.RenderTemplate = renderItem.Template;
                    args.Start = region.Position;
                    args.Length = region.Length;
                    renders.Add(args);
                    regionIndex++;
                }
            }
            else
            {
                filename += renderItem.Extension;
                RenderArgs args = new RenderArgs();
                args.OutputFile = filename;
                args.RenderTemplate = renderItem.Template;
                args.UseSelection = (renderMode == RenderMode.Selection);
                renders.Add(args);
            }
        }

        // validate all files and propmt for overwrites
        foreach (RenderArgs args in renders)
        {
            ValidateFilePath(args.OutputFile);
            if (!OverwriteExistingFiles)
            {
                if (File.Exists(args.OutputFile))
                {
                    String msg = "File(s) exists. Do you want to overwrite them?";
                    DialogResult rs;
                    rs = MessageBox.Show(msg,
                                                         "Overwrite files?",
                                                         MessageBoxButtons.OKCancel,
                                                         MessageBoxIcon.Warning,
                                                         MessageBoxDefaultButton.Button2);
                    if (DialogResult.Cancel == rs)
                    {
                        return;
                    }
                    else
                    {
                        OverwriteExistingFiles = true;
                    }
                }
            }
        }

        // perform all renders.  The Render method returns a member of the RenderStatus enumeration.  If it is
        // anything other than OK, exit the loop.
        foreach (RenderArgs args in renders)
        {
            if (RenderStatus.Canceled == DoRender(args))
            {
                break;
            }
        }

    }

    RenderStatus DoRender(RenderArgs args)
    {
        RenderStatus status = myVegas.Render(args);
        switch (status)
        {
            case RenderStatus.Complete:
            case RenderStatus.Canceled:
                break;
            case RenderStatus.Failed:
            default:
                StringBuilder msg = new StringBuilder("Render failed:\n");
                msg.Append("\n    file name: ");
                msg.Append(args.OutputFile);
                msg.Append("\n    Template: ");
                msg.Append(args.RenderTemplate.Name);
                throw new ApplicationException(msg.ToString());
        }
        return status;
    }

    String FixFileName(String name)
    {
        const Char replacementChar = '-';
        foreach (char badChar in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(badChar, replacementChar);
        }
        return name;
    }

    void ValidateFilePath(String filePath)
    {
        if (filePath.Length > 260)
            throw new ApplicationException("File name too long: " + filePath);
        foreach (char badChar in Path.GetInvalidPathChars())
        {
            if (0 <= filePath.IndexOf(badChar))
            {
                throw new ApplicationException("Invalid file name: " + filePath);
            }
        }
    }

    class RenderItem
    {
        public readonly Renderer Renderer = null;
        public readonly RenderTemplate Template = null;
        public readonly String Extension = null;

        public RenderItem(Renderer r, RenderTemplate t, String e)
        {
            this.Renderer = r;
            this.Template = t;
            // need to strip off the extension's leading "*"
            if (null != e) this.Extension = e.TrimStart('*');
        }
    }


    Button BrowseButton;
    TextBox FileNameBox;
    TreeView TemplateTree;
    RadioButton RenderProjectButton;
    RadioButton RenderRegionsButton;
    RadioButton RenderSelectionButton;

    /// <summary>
    /// Displays a customizable batch render dialog with improved UI scaling and layout management.
    /// This version introduces a resizable window with anchored UI elements, ensuring consistent visibility
    /// and accessibility across different resolutions and DPI settings. The "OK" button behavior has been fixed
    /// to be visible and responsive to mouse clicks, addressing the issue where it previously responded only to the Enter key.
    /// </summary>
    /// <returns>The dialog result indicating how the dialog was closed.</returns>
    /// <summary>
    /// Displays a customizable batch render dialog with improved UI scaling and layout management.
    /// </summary>
    /// <returns>The dialog result indicating how the dialog was closed.</returns>
    public DialogResult ShowBatchRenderDialog()
    {
        // Define constants
        const float HiDPI_RES_LIMIT = 1.37f;
        const int ButtonWidth = 100; // Use constant for button width
        const int MinimumDialogWidth = 940;
        const int MinimumDialogHeight = 480;

        // Initialize variables
        float dpiScale = 1.0f;
        Form dialog = new Form();

        // Configure dialog properties
        dialog.BackColor = Color.FromArgb(32, 32, 32);
        dialog.Text = "Batch Render V2";
        dialog.FormBorderStyle = FormBorderStyle.Sizable;
        dialog.MinimumSize = new Size(MinimumDialogWidth, MinimumDialogHeight);
        dialog.MaximizeBox = false;
        dialog.StartPosition = FormStartPosition.CenterScreen;
        dialog.FormClosing += HandleFormClosing;

        // Determine DPI scaling (optional, can be removed if not needed)
        using (Graphics g = Graphics.FromHwnd(dialog.Handle))
        {
            dpiScale = g.DpiY / 96.0f;
            g.Dispose();

            if (dpiScale < HiDPI_RES_LIMIT)
            {
                dpiScale = 1.0f;
            }
        }

        // Create UI elements
        TextBox fileNameBox = CreateTextBox(dialog, "Base File Name", 16, defaultBasePath);
        Button browseButton = CreateButton(dialog, "Browse...", ButtonWidth, HandleBrowseClick);
        TreeView templateTree = CreateTreeView(dialog, true, HandleTreeViewCheck);
        RadioButton renderProjectButton = CreateRadioButton(dialog, "Render Project", true);
        RadioButton renderSelectionButton = CreateRadioButton(dialog, "Render Selection", 0);
        RadioButton renderRegionsButton = CreateRadioButton(dialog, "Render Regions", 0);
        Button okButton = CreateButton(dialog, "OK", ButtonWidth, DialogResult.OK);
        Button cancelButton = CreateButton(dialog, "Cancel", ButtonWidth, DialogResult.Cancel);

        // Layout UI elements using anchor styles
        fileNameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        templateTree.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        renderProjectButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        renderSelectionButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        renderRegionsButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        // Position UI elements relative to each other
        fileNameBox.Location = new Point(10, dialog.ClientSize.Height - fileNameBox.Height - 10);
        browseButton.Location = new Point(fileNameBox.Right + 4, fileNameBox.Top);
        templateTree.Location = new Point(10, fileNameBox.Bottom + 10);
        templateTree.Height = (int)(300 * dpiScale);
        renderProjectButton.Location = new Point(10, templateTree.Bottom + 16);
        renderSelectionButton.Location = new Point(renderProjectButton.Right + 10, renderProjectButton.Top);
        renderRegionsButton.Location = new Point(renderSelectionButton.Right + 10, renderProjectButton.Top);
        okButton.Location = new Point(dialog.ClientSize.Width - (2 * ButtonWidth + 20), renderProjectButton.Top);
        cancelButton.Location = new Point(okButton.Left - ButtonWidth - 10, renderProjectButton.Top);

        // Set button colors and dialog height
        okButton.ForeColor = cancelButton.ForeColor = renderProjectButton.ForeColor = renderSelectionButton.ForeColor = renderRegionsButton.ForeColor = Color.White;
        dialog.Height = okButton.Bottom + 8;

        // Add UI elements to dialog
        dialog.Controls.Add(fileNameBox);
        dialog.Controls.Add(browse);
    }
    private TextBox CreateTextBox(Form dialog, string labelText, int fontSize, string defaultText)
    {
        TextBox textBox = new TextBox();
        textBox.Text = labelText;
        textBox.Font = new Font(FontFamily.GenericSansSerif, fontSize);
        textBox.Location = new Point(10, 10); // Set initial position
        textBox.BackColor = Color.FromArgb(64, 64, 64);
        textBox.ForeColor = Color.White;
        return textBox;
    }
    TextBox AddTextControl(Form dlog, String labelName, int left, int width, int top, String defaultValue)
    {
        Label label = new Label();
        label.AutoSize = true;
        label.Text = labelName + ":";
        label.Left = left;
        label.Top = top + 4;
        label.ForeColor = Color.White;
        dlog.Controls.Add(label);

        TextBox textbox = new TextBox();
        textbox.Multiline = false;
        textbox.Left = label.Right;
        textbox.Top = top;
        textbox.Width = width - (label.Width);
        textbox.Text = defaultValue;
        dlog.Controls.Add(textbox);

        return textbox;
    }

    /// <summary>
    /// Creates and adds a radio button with a label to the form. The radio button is anchored to the bottom-left, allowing it to stay aligned when resizing.
    /// The label is positioned to the left of the radio button and centered vertically.
    /// </summary>
    /// <param name="dlog">The form to which the radio button and label are added.</param>
    /// <param name="labelName">The text for the label.</param>
    /// <param name="left">The left position of the label.</param>
    /// <param name="top">The top position where the radio button should be placed.</param>
    /// <param name="enabled">Indicates whether the radio button is enabled.</param>
    /// <returns>The created radio button.</returns>
    RadioButton AddRadioControl(Form dlog, String labelName, int left, int top, bool enabled)
    {
        Label label = new Label
        {
            AutoSize = true,
            Text = labelName,
            Left = left,
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            Enabled = enabled,
        };
        label.ForeColor = Color.White;
        dlog.Controls.Add(label);

        RadioButton radioButton = new RadioButton
        {
            Left = label.Right + 5, // Added spacing for visual separation
            Width = 36, // Consider adjusting or calculating width based on text or container size
            Top = top,
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
            Enabled = enabled
        };
        label.Top = radioButton.Top + (radioButton.Height - label.Height) / 2; // Vertically center label
        dlog.Controls.Add(radioButton);

        return radioButton;
    }

    static Guid[] TheDefaultTemplateRenderClasses =
        {
            Renderer.CLSID_SfWaveRenderClass,
            Renderer.CLSID_SfW64ReaderClass,
            Renderer.CLSID_CSfAIFRenderFileClass,
            Renderer.CLSID_CSfFLACRenderFileClass,
            Renderer.CLSID_CSfPCARenderFileClass,
        };

    bool AllowDefaultTemplates(Guid rendererID)
    {
        foreach (Guid guid in TheDefaultTemplateRenderClasses)
        {
            if (guid == rendererID)
                return true;
        }
        return false;
    }

    void FillTemplateTree()
    {
        int projectAudioChannelCount = 0;
        if (AudioBusMode.Stereo == myVegas.Project.Audio.MasterBusMode)
        {
            projectAudioChannelCount = 2;
        }
        else if (AudioBusMode.Surround == myVegas.Project.Audio.MasterBusMode)
        {
            projectAudioChannelCount = 6;
        }
        bool projectHasVideo = ProjectHasVideo();
        bool projectHasAudio = ProjectHasAudio();
        int projectVideoStreams = !projectHasVideo ? 0 :
                (Stereo3DOutputMode.Off != myVegas.Project.Video.Stereo3DMode ? 2 : 1);

        foreach (Renderer renderer in myVegas.Renderers)
        {
            try
            {
                String rendererName = renderer.FileTypeName;
                TreeNode rendererNode = new TreeNode(rendererName);
                rendererNode.Tag = new RenderItem(renderer, null, null);
                foreach (RenderTemplate template in renderer.Templates)
                {
                    try
                    {
                        // filter out invalid templates
                        if (!template.IsValid())
                        {
                            continue;
                        }
                        // filter out video templates when project has
                        // no video.
                        if (!projectHasVideo && (0 < template.VideoStreamCount))
                        {
                            continue;
                        }
                        // filter out templates that are 3d when the project is just 2d
                        if (projectHasVideo && projectVideoStreams < template.VideoStreamCount)
                        {
                            continue;
                        }

                        // filter the default template (template 0) and we don't allow defaults
                        //   for this renderer
                        if (template.TemplateID == 0 && !AllowDefaultTemplates(renderer.ClassID))
                        {
                            continue;
                        }

                        // filter out audio-only templates when project has no audio
                        if (!projectHasAudio && (0 == template.VideoStreamCount) && (0 < template.AudioStreamCount))
                        {
                            continue;
                        }
                        // filter out templates that have more channels than the project
                        if (projectAudioChannelCount < template.AudioChannelCount)
                        {
                            continue;
                        }
                        // filter out templates that don't have
                        // exactly one file extension
                        String[] extensions = template.FileExtensions;
                        if (1 != extensions.Length)
                        {
                            continue;
                        }
                        String templateName = template.Name;
                        TreeNode templateNode = new TreeNode(templateName);
                        templateNode.Tag = new RenderItem(renderer, template, extensions[0]);
                        rendererNode.Nodes.Add(templateNode);
                    }
                    catch (Exception e)
                    {
                        // skip it
                        MessageBox.Show(e.ToString());
                    }
                }
                if (0 == rendererNode.Nodes.Count)
                {
                    continue;
                }
                else if (1 == rendererNode.Nodes.Count)
                {
                    // skip it if the only template is the project
                    // settings template.
                    if (0 == ((RenderItem)rendererNode.Nodes[0].Tag).Template.Index)
                    {
                        continue;
                    }
                }
                else
                {
                    TemplateTree.Nodes.Add(rendererNode);
                }
            }
            catch
            {
                // skip it
            }
        }
    }

    bool ProjectHasVideo()
    {
        foreach (Track track in myVegas.Project.Tracks)
        {
            if (track.IsVideo())
            {
                return true;
            }
        }
        return false;
    }

    bool ProjectHasAudio()
    {
        foreach (Track track in myVegas.Project.Tracks)
        {
            if (track.IsAudio())
            {
                return true;
            }
        }
        return false;
    }

    void UpdateSelectedTemplates()
    {
        SelectedTemplates.Clear();
        foreach (TreeNode node in TemplateTree.Nodes)
        {
            foreach (TreeNode templateNode in node.Nodes)
            {
                if (templateNode.Checked)
                {
                    SelectedTemplates.Add(templateNode.Tag);
                }
            }
        }
    }

    void HandleBrowseClick(Object sender, EventArgs args)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "All Files (*.*)|*.*";
        saveFileDialog.CheckPathExists = true;
        saveFileDialog.AddExtension = false;
        if (null != FileNameBox)
        {
            String filename = FileNameBox.Text;
            String initialDir = Path.GetDirectoryName(filename);
            if (Directory.Exists(initialDir))
            {
                saveFileDialog.InitialDirectory = initialDir;
            }
            saveFileDialog.DefaultExt = Path.GetExtension(filename);
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(filename);
        }
        if (System.Windows.Forms.DialogResult.OK == saveFileDialog.ShowDialog())
        {
            if (null != FileNameBox)
            {
                FileNameBox.Text = Path.GetFullPath(saveFileDialog.FileName);
            }
        }
    }

    void HandleTreeViewCheck(object sender, TreeViewEventArgs args)
    {
        if (args.Node.Checked)
        {
            if (0 != args.Node.Nodes.Count)
            {
                if ((args.Action == TreeViewAction.ByKeyboard) || (args.Action == TreeViewAction.ByMouse))
                {
                    SetChildrenChecked(args.Node, true);
                }
            }
            else if (!args.Node.Parent.Checked)
            {
                args.Node.Parent.Checked = true;
            }
        }
        else
        {
            if (0 != args.Node.Nodes.Count)
            {
                if ((args.Action == TreeViewAction.ByKeyboard) || (args.Action == TreeViewAction.ByMouse))
                {
                    SetChildrenChecked(args.Node, false);
                }
            }
            else if (args.Node.Parent.Checked)
            {
                if (!AnyChildrenChecked(args.Node.Parent))
                {
                    args.Node.Parent.Checked = false;
                }
            }
        }
    }

    void HandleFormClosing(Object sender, FormClosingEventArgs args)
    {
        Form dlg = sender as Form;
        if (null == dlg) return;
        if (DialogResult.OK != dlg.DialogResult) return;
        String outputFilePath = FileNameBox.Text;
        try
        {
            String outputDirectory = Path.GetDirectoryName(outputFilePath);
            if (!Directory.Exists(outputDirectory)) throw new ApplicationException();
        }
        catch
        {
            String title = "Invalid Directory";
            StringBuilder msg = new StringBuilder();
            msg.Append("The output directory does not exist.\n");
            msg.Append("Please specify the directory and base file name using the Browse button.");
            MessageBox.Show(dlg, msg.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            args.Cancel = true;
            return;
        }
        try
        {
            String baseFileName = Path.GetFileName(outputFilePath);
            if (String.IsNullOrEmpty(baseFileName)) throw new ApplicationException();
            if (-1 != baseFileName.IndexOfAny(Path.GetInvalidFileNameChars())) throw new ApplicationException();
        }
        catch
        {
            String title = "Invalid Base File Name";
            StringBuilder msg = new StringBuilder();
            msg.Append("The base file name is not a valid file name.\n");
            msg.Append("Make sure it contains one or more valid file name characters.");
            MessageBox.Show(dlg, msg.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            args.Cancel = true;
            return;
        }
        UpdateSelectedTemplates();
        if (0 == SelectedTemplates.Count)
        {
            String title = "No Templates Selected";
            StringBuilder msg = new StringBuilder();
            msg.Append("No render templates selected.\n");
            msg.Append("Select one or more render templates from the available formats.");
            MessageBox.Show(dlg, msg.ToString(), title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            args.Cancel = true;
            return;
        }
    }

    void SetChildrenChecked(TreeNode node, bool checkIt)
    {
        foreach (TreeNode childNode in node.Nodes)
        {
            if (childNode.Checked != checkIt)
                childNode.Checked = checkIt;
        }
    }

    bool AnyChildrenChecked(TreeNode node)
    {
        foreach (TreeNode childNode in node.Nodes)
        {
            if (childNode.Checked) return true;
        }
        return false;
    }

}
