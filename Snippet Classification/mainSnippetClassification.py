from nltk.tokenize import word_tokenize
import logging
import re


def check_recursion(tokens):
    """
    Check if there is recursion in the given tokens.

    >>> check_recursion(['def', 'foo', 'foo'])
    True
    >>> check_recursion(['def', 'foo', 'bar'])
    False
    """
    function_name = None
    for idx, token in enumerate(tokens):
        if token == "def" or token == "function":
            function_name = tokens[idx + 1]
        elif token == function_name:
            return True
    return False


def check_nested_conditionals(tokens):
    """
    Check for nested conditional statements in tokens.

    >>> check_nested_conditionals(['if', 'if', 'end', 'end'])
    True
    >>> check_nested_conditionals(['if', 'end'])
    False
    """
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
    """
    Check for multiple assignments in a single statement.

    >>> check_multiple_assignments(['a,', 'b', '=', '5,', '6'])
    True
    >>> check_multiple_assignments(['a', '=', '5'])
    False
    """
    assignment_pattern = re.compile(r'(\w+,\s*)+\w+\s*=')
    return bool(assignment_pattern.search(' '.join(tokens)))


def read_snippets(filename):
    snippets = []
    with open(filename) as localfile:
        snippets.append(localfile.read())
    return snippets


def tokenize_code(code):
    """
    Tokenize the given code using NLTK's word tokenizer.

    >>> tokenize_code("def foo(): pass")
    ['def', 'foo', '(', ')', ':', 'pass']
    """
    return word_tokenize(code)


def check_type3(tokens):
    """
    Check if the tokens match a Type 3 pattern (Regular/Finite State).

    >>> check_type3(['for', 'i', 'in', 'range', '(', '10', ')'])
    True
    >>> check_type3(['def', 'foo', '(', ')', ':', 'pass'])
    False
    """
    keywords = ["for", "while", "if"]
    return any(kw in tokens for kw in keywords)


def check_type2(tokens):
    """
    Check if the tokens match a Type 2 pattern (Context-Free).

    >>> check_type2(['def', 'foo', '(', ')', ':', 'pass'])
    True
    >>> check_type2(['print', '(', '"Hello"', ')'])
    False
    """
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
    """
    Classify the given tokens into a Chomsky hierarchy type.

    >>> classify_code(['def', 'foo', '(', ')', ':', 'pass'])
    'Context-Free'
    >>> classify_code(['print', '(', '"Hello"', ')'])
    'Regular/Finite State'
    """
    # First check for context-free (Type 2) patterns
    if check_type2(tokens):
        return "Context-Free"
    # Then check for regular/finite state (Type 3) patterns
    elif check_type3(tokens):
        return "Regular/Finite State"
    else:
        return "Unclassified"


def log_result(snippet, type):
    """
    Log the classification result of a snippet.

    Note: This function does not return anything and is used for logging purposes.
    """
    logging.info(f"{snippet} -> {type}")


def process_snippets(filename):
    """
    Process snippets from a file and classify them.

    Note: This function is used to process and classify code snippets from a given file.
    """
    snippets = read_snippets(filename)
    results = []
    for snippet in snippets:
        tokens = tokenize_code(snippet)
        type = classify_code(tokens)
        print(type)
        # Note for most logging use the attched Unit test.
        # log_result(snippet, type)
        results.append((snippet, type))
    return results


def main():
    filename = "Snippet Classification\\test.txt"
    process_snippets(filename)


if __name__ == "__main__":
    """
    python -m doctest -v mainSnippetClassification.py
    Main function to process and classify code snippets.

    Note: Modify 'filename' as needed to point to the desired test file.    
    """
    main()
