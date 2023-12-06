import unittest
import os
from mainSnippetClassification import process_snippets


class TestSnippetClassification(unittest.TestCase):
    """
    A test suite for the Snippet Classification project.
    This suite tests the functionality of processing and classifying
    code snippets from text files within a specified directory.
    """

    def test_process_all_snippets(self):
        """
        Test the processing of all snippets in the given directory.
        If no directory is specified, a default path is used.
        Results are logged into a file.
        """
        default_directory = "testtext"
        test_directory = os.getenv('TEST_DIRECTORY', default_directory)
        log_file = "combined_results.log"
        with open(log_file, 'w') as log:
            for filename in os.listdir(test_directory):
                if filename.endswith(".txt"):
                    language_type = filename.split(
                        '_')[0]  # Extracting language type
                    file_path = os.path.join(test_directory, filename)
                    results = process_snippets(file_path)
                    for _, type in results:
                        log.write(
                            f"File: {filename}, Language: {language_type}, Type: {type}\n")


if __name__ == '__main__':
    unittest.main()
