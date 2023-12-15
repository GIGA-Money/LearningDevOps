/**
 * Sample script that performs batch renders with GUI for selecting
 * render templates.
 *
 * Revision Date: 12/13/23 - iAmGiG edit via GitHub.
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
        SelectedRegions,
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
            else if (RenderRegionsInSelection.Checked)
            {
                renderMode = RenderMode.SelectedRegions;
            }
            DoBatchRender(SelectedTemplates, outputFilePath, renderMode);
        }
    }

    /// <summary>
    /// This script for Vegas Pro enables advanced batch rendering. 
    /// It allows rendering based on specific modes like regions, selected regions, and entire projects. 
    /// The script intelligently handles file naming, output directory creation, and user prompts for file overwriting. 
    /// In 'Selected Regions' mode, it discerns and renders only the regions falling within a defined selection. 
    /// The script features robust error handling and is optimized for user convenience, 
    ///     ensuring efficient and targeted rendering processes.
    /// </summary>
    /// <param name="selectedTemplates"></param>
    /// <param name="basePath"></param>
    /// <param name="renderMode"></param>
    /// <exception cref="ApplicationException"></exception>
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
            if (RenderMode.Regions == renderMode)
            {
                int regionIndex = 0;
                foreach (ScriptPortal.Vegas.Region region in myVegas.Project.Regions)
                {
                    if (renderMode == RenderMode.Regions)
                    {
                        // Build region filename with directory and index
                        String regionFilename = BuildRegionFilename(outputDirectory, baseFileName, renderItem, region, regionIndex);

                        RenderArgs args = new RenderArgs();
                        args.OutputFile = regionFilename;
                        args.RenderTemplate = renderItem.Template;
                        args.Start = region.Position;
                        args.Length = region.Length;
                        renders.Add(args);
                        regionIndex++;
                    }

                }
            //check to see if this is a QuickTime file...if so, file length cannot exceed 59 characters
           else if (RenderMode.SelectedRegions == renderMode)
                {
                    int regionIndex = 0;
                    // Calculate selection bounds once
                    int startTime = (int)(myVegas.Selection.Start.Nanos / 1000000);
                    int endTime = (int)(myVegas.Selection.End.Nanos / 1000000);

                }
                foreach (ScriptPortal.Vegas.Region region in myVegas.Project.Regions)
                {
                    // Extract filename without extension
                    String strippedFilename = Path.GetFileNameWithoutExtension(filename);
                    // Check for overlap if "Selected Regions" mode
                    //bool overlaps = region.Position >= startTime && region.Position + region.Length <= endTime;
                    bool overlap = (region.Position >= startTime && region.Position < endTime) || (region.Position + region.Length > startTime && region.Position + region.Length <= endTime);
                    if (overlaps || renderMode == RenderMode.Regions)
                    {
                        // Build region filename with directory and index
                        String regionFilename = BuildRegionFilename(outputDirectory, baseFileName, renderItem, region, regionIndex);

                        RenderArgs args = new RenderArgs();
                        args.OutputFile = regionFilename;
                        args.RenderTemplate = renderItem.Template;
                        args.Start = region.Position;
                        args.Length = region.Length;
                        renders.Add(args);
                        regionIndex++;
                    }
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

        // Validate all files and prompt for overwrites
        foreach (RenderArgs args in renders)
        {
            try
            {
                /*
                This code checks if the output file for each render already exists. 
                If a file exists and the user has not chosen to overwrite, 
                it prompts the user to either overwrite the existing files or cancel the operation. 
                If the user chooses to cancel, 
                the process is halted. If the user opts to overwrite, 
                it sets a flag to overwrite all subsequent existing files without prompting.
                */
                ValidateFilePath(args.OutputFile);
                if (!OverwriteExistingFiles && File.Exists(args.OutputFile))
                {
                    string msg = "File(s) exists. Do you want to overwrite them?";
                    DialogResult rs = MessageBox.Show(msg, "Overwrite files?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (DialogResult.Cancel == rs)
                    {
                        return; // Exit the loop and method if the user selects Cancel
                    }
                    else
                    {
                        OverwriteExistingFiles = true; // Set to true to overwrite files in subsequent iterations
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or show an error message to the user
                MessageBox.Show("An error occurred during file validation: " + ex.Message + "\nRecommendation: Check file access permissions and ensure the file is not in use.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dlog.Close();
                return;
            }
        }


        // perform all renders.  The Render method returns a member of the RenderStatus enumeration.  If it is
        // anything other than OK, exit the loop.
        try
        {
            foreach (RenderArgs args in renders)
            {
                if (RenderStatus.Canceled == DoRender(args))
                {
                    break; // Stop rendering if the user cancels
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred during rendering: " + ex.Message, "Rendering Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Optionally, log the exception details for further analysis
        }

    }
    private String BuildRegionFilename(String outputDirectory, String baseFileName, RenderItem renderItem, ScriptPortal.Vegas.Region region, int regionIndex)
    {
        return Path.Combine(outputDirectory,
            String.Format("{0}_{1}_{2}{3}",
            regionIndex.ToString("D3"),
            Path.GetFileNameWithoutExtension(baseFileName),
            FixFileName(region.Label),
            renderItem.Extension));
    }

    RenderStatus DoRender(RenderArgs args)
    {
        try
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
        catch (Exception ex)
        {
            MessageBox.Show("A rendering error occurred: " + ex.Message, "Rendering Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return RenderStatus.Failed; // Indicate failure
        }
    }

    String FixFileName(String name)
    {
        try
        {
            const Char replacementChar = '-';
            foreach (char badChar in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(badChar, replacementChar);
            }
            return name;
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred while fixing the file name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return "default_filename"; // or handle differently as needed 
        }
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
    RadioButton RenderRegionsInSelection;

    /// <summary>
    /// Displays a customizable batch render dialog with improved UI scaling and layout management.
    /// This version introduces a resizable window with anchored UI elements, ensuring consistent visibility
    /// and accessibility across different resolutions and DPI settings. The "OK" button behavior has been fixed
    /// to be visible and responsive to mouse clicks, addressing the issue where it previously responded only to the Enter key.
    /// </summary>
    /// <returns>The dialog result indicating how the dialog was closed.</returns>
    DialogResult ShowBatchRenderDialog()
    {
        const float HiDPI_RES_LIMIT = 1.37f; // based on the original HiDPI changes for DVP-667
        float dpiScale = 1.0f;

        Form dlog = new Form();
        // Set the form's properties here
        dlog.BackColor = Color.FromArgb(32, 32, 32); // after this line

        TextBox FileNameBox = new TextBox();
        // Set FileNameBox properties here
        FileNameBox.BackColor = Color.FromArgb(64, 64, 64); // after this line
        FileNameBox.ForeColor = Color.White; // after this line

        Button BrowseButton = new Button();
        // Set BrowseButton properties here
        BrowseButton.FlatStyle = FlatStyle.Flat; // for a flat style
        BrowseButton.ForeColor = Color.White; // after this line
        BrowseButton.BackColor = Color.FromArgb(64, 64, 64); // after this line

        // Determine if DPI scale adjustments need to be made (ref. DVP-667)
        Graphics g = ((Control)dlog).CreateGraphics();
        if (g != null)
        {
            dpiScale = (float)g.DpiY / 96.0f;
            g.Dispose();

            if (dpiScale < HiDPI_RES_LIMIT)  // only apply if DPI scale > 150%
                dpiScale = 1.0f;
        }

        dlog.Text = "Batch Render V3";
        //changed from fixed dialog size in the windows froms to sizable.
        dlog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        //Now I have to use a minimum size on the dlog so it won't be made into a shrunk up 
        dlog.MinimumSize = new Size(940, 480);
        dlog.MaximizeBox = false;
        dlog.StartPosition = FormStartPosition.CenterScreen;
        dlog.Width = (int)(720 * dpiScale);
        dlog.FormClosing += this.HandleFormClosing;

        int titleBarHeight = dlog.Height - dlog.ClientSize.Height;
        int buttonWidth = (int)(100 * dpiScale);
        int fileNameWidth = (int)(480 * dpiScale);

        FileNameBox = AddTextControl(dlog, "Base File Name", titleBarHeight + 6, fileNameWidth, 16, defaultBasePath);

        BrowseButton = new Button();
        BrowseButton.Left = FileNameBox.Right + 4;
        BrowseButton.Top = FileNameBox.Top - 2;
        BrowseButton.Width = buttonWidth;
        BrowseButton.Height = BrowseButton.Font.Height + 14;
        BrowseButton.Text = "Browse...";
        BrowseButton.Click += new EventHandler(this.HandleBrowseClick);
        dlog.Controls.Add(BrowseButton);

        TemplateTree = new TreeView();

        TemplateTree.Left = 10;
        TemplateTree.Width = dlog.Width - 35;
        TemplateTree.Top = BrowseButton.Bottom + 10;
        TemplateTree.Height = (int)(300 * dpiScale);
        TemplateTree.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
        TemplateTree.CheckBoxes = true;
        TemplateTree.AfterCheck += new TreeViewEventHandler(this.HandleTreeViewCheck);
        dlog.Controls.Add(TemplateTree);

        int buttonTop = TemplateTree.Bottom + 16;
        int buttonsLeft = dlog.Width - (2 * (buttonWidth + 10));

        //anchor adjusting is done in the "AddRadioControl" method.
        RenderProjectButton = AddRadioControl(dlog,
                                              "Render Project",
                                              6,
                                              buttonTop,
                                              true);
        RenderSelectionButton = AddRadioControl(dlog,
                                                "Render Selection",
                                                RenderProjectButton.Right,
                                                buttonTop,
                                                (0 != myVegas.SelectionLength.Nanos));
        RenderRegionsButton = AddRadioControl(dlog,
                                              "Render Regions",
                                              RenderSelectionButton.Right,
                                              buttonTop,
                                              (0 != myVegas.Project.Regions.Count));
        //The last parameter (0 != myVegas.Project.Regions) is intended to enable the button if there are regions in the project.
        RenderRegionsInSelection = AddRadioControl(dlog,
                                                    "Render Regions in Selection",
                                                    RenderRegionsButton.Right,
                                                    buttonTop,
                                                    (myVegas.Project.Regions.Count > 0 && myVegas.SelectionLength > 0))

        RenderProjectButton.Checked = true;

        int buttonRightGap = (int)(dpiScale * 5);

        Button okButton = new Button();

        okButton.Text = "OK";
        okButton.Left = dlog.Width - (2 * (buttonWidth + 20)) - buttonRightGap;
        okButton.Top = buttonTop;
        okButton.Width = buttonWidth;
        okButton.Height = okButton.Font.Height + 12;
        okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        dlog.AcceptButton = okButton;
        dlog.Controls.Add(okButton);

        Button cancelButton = new Button();
        cancelButton.Text = "Cancel";
        cancelButton.Left = dlog.Width - (1 * (buttonWidth + 20)) - buttonRightGap;
        cancelButton.Top = buttonTop;
        cancelButton.Width = buttonWidth;
        cancelButton.Height = cancelButton.Font.Height + 12;
        cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        dlog.CancelButton = cancelButton;
        dlog.Controls.Add(cancelButton);

        dlog.Height = titleBarHeight + okButton.Bottom + 8;
        dlog.ShowInTaskbar = false;

        FileNameBox.ForeColor = Color.White;
        FileNameBox.BackColor = Color.FromArgb(32, 32, 32);
        FileNameBox.Width = (TemplateTree.Width / 2);
        FileNameBox.Left = (dlog.ClientSize.Width - FileNameBox.Width) / 2;
        FileNameBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        BrowseButton.Top = FileNameBox.Top + (FileNameBox.Height - BrowseButton.Height) / 2;
        BrowseButton.Left = FileNameBox.Right + 4;
        BrowseButton.ForeColor = Color.White;
        BrowseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        dlog.Controls.Add(BrowseButton); // Add button first
        dlog.Controls.Add(FileNameBox); // Add text box second

        RenderProjectButton.ForeColor = Color.White;
        RenderSelectionButton.ForeColor = Color.White;
        RenderRegionsButton.ForeColor = Color.White;
        okButton.ForeColor = Color.White;
        cancelButton.ForeColor = Color.White;
        TemplateTree.ForeColor = Color.White;
        TemplateTree.BackColor = Color.FromArgb(32, 32, 32);
        FillTemplateTree();

        return dlog.ShowDialog(myVegas.MainWindow);
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

    void AddSeparator(Form dlog, Control leftControl, int top)
    {
        Label separator = new Label
        {
            Text = "|",
            ForeColor = Color.White,
            AutoSize = true,
            Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular),
            Left = leftControl.Right + 5, // Place to the right of the radio button
            Top = leftControl.Top, // Align with the top of the radio button
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left
        };
        dlog.Controls.Add(separator);
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
        try
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
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred while browsing for files: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
