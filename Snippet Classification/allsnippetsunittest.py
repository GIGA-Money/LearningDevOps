import unittest
import os
from mainSnippetClassification import process_snippets


class TestSnippetClassification(unittest.TestCase):

    def test_process_all_snippets(self):
        test_directory = "C:\\Users\\gigac\\Documents\\Projects\\LearningDevOps\\Snippet Classification\\testtext"
        log_file = "combined_results.log"
        with open(log_file, 'w') as log:
            for filename in os.listdir(test_directory):
                if filename.endswith(".txt"):
                    # Extracting language type from filename
                    language_type = filename.split('_')[0]
                    file_path = os.path.join(test_directory, filename)
                    results = process_snippets(file_path)
                    for _, type in results:
                        log.write(
                            f"File: {filename}, Language: {language_type}, Type: {type}\n")


if __name__ == '__main__':
    unittest.main()
