import re
import logging
from nltk.tokenize import word_tokenize

# Set up logging
logging.basicConfig(filename='classification_log.txt', level=logging.INFO)


def read_snippets(filename):
    snippets = []
    with open(filename) as f:
        snippets.append(f.read())
    return snippets


def tokenize_code(code):
    return word_tokenize(code)


def check_type3(tokens):
    keywords = ["for", "while", "if"]
    return any(kw in tokens for kw in keywords)


def check_type2(tokens):
    # Implement logic for identifying nested structures or recursion
    pass


def classify_code(tokens):
    if check_type3(tokens):
        return "Regular/Finite State"
    elif check_type2(tokens):
        return "Context-Free"
    else:
        return "Unclassified"


def log_result(snippet, type):
    logging.info(f"{snippet} -> {type}")


def main():
    snippets = read_snippets("path_to_file")
    for snippet in snippets:
        tokens = tokenize_code(snippet)
        type = classify_code(tokens)
        print(type)
        log_result(snippet, type)


if __name__ == "__main__":
    main()
