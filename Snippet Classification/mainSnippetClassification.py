from nltk.tokenize import word_tokenize
import logging
import re


# logging.basicConfig(filename='classification_log.txt', level=logging.INFO)


def check_recursion(tokens):
    function_name = None
    for idx, token in enumerate(tokens):
        if token == "def" or token == "function":
            function_name = tokens[idx + 1]
        elif token == function_name:
            return True
    return False


def check_nested_conditionals(tokens):
    depth = 0
    max_depth = 0
    for t in tokens:
        if t in ["if", "for", "while"]:
            depth += 1
            max_depth = max(depth, max_depth)
        elif t == "end":
            depth -= 1
    return max_depth > 1


def check_multiple_assignments(tokens):
    assignment_pattern = re.compile(r'(\w+,\s*)+\w+\s*=')
    return bool(assignment_pattern.search(' '.join(tokens)))


def read_snippets(filename):
    snippets = []
    with open(filename) as localfile:
        snippets.append(localfile.read())
    return snippets


def tokenize_code(code):
    return word_tokenize(code)


def check_type3(tokens):
    keywords = ["for", "while", "if"]
    return any(kw in tokens for kw in keywords)


def check_type2(tokens):
    # Check 1: Nested constructs
    if check_nested_conditionals(tokens) > 1:
        return True

    # Check 2: Recursion
    if check_recursion(tokens):
        return True

    # Check 3: Multiple variables
    if check_multiple_assignments(tokens) > 1:
        return True

    return False


def classify_code(tokens):
    if check_type3(tokens):
        return "Regular/Finite State"
    elif check_type2(tokens):
        return "Context-Free"
    else:
        return "Unclassified"


def log_result(snippet, type):
    logging.info(f"{snippet} -> {type}")


def process_snippets(filename):
    snippets = read_snippets(filename)
    results = []
    for snippet in snippets:
        tokens = tokenize_code(snippet)
        type = classify_code(tokens)
        print(type)
        log_result(snippet, type)
        results.append((snippet, type))
    return results


def main():
    filename = "Snippet Classification\\test.txt"
    process_snippets(filename)


if __name__ == "__main__":
    main()
