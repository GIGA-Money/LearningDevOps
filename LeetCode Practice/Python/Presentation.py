"""
building:
* 'if' statements
* 'for' loops
* 'methods and functions'
* 'classes' 
* 'arrays and list'
* 'Namespaces' - scope in python begins at the file level, broken down by the built-in namespace -> global namespace -> local namespace.
                - its life time is depended upon the scope of objects, as the objects scope the lifetime of the namespace comes to an end.
                - this will also affect how we built, public, private, and protected variables in our classes.
* complied to bytecode - then the interpreter will take it in. such as Cpython(the default interpreter), Jython(to java) and ironPy(to C#).
"""

"""
Overload and overriding rules are fundamentally the same (as they are in most OOP languages).
"""

"""
declaring variables
Semi-colon ';' yeah we'll be passing on this guy.
x = 0.0
x = 0

"""

"""
user input/output
input(<string>) its a writelines and readline all in one.
"""

"""
Null or None
Well we can't compare to Null, passing null around isn't to safe, 
so lets use an intermediary and call it None, functionally the something.
"""

'''
Methods, classes and functions (with returning)
functions can be passed around like hot cakes.
def funnyFunc(x,y,z):
    print(x,y,z)
def funnyFunc(int: x, int: y, list: z):
    print(x,y,z)
def funnyFunc(x = 0, y = 1, z = []):
    print(x,y,z)
def funnyFunc(x,y,z):
    print(x,y,z)
    return True
def funnyFunc(x,y,z) -> bool:
    print(x,y,z)
    return True
'''

"""
for loop - similar to most languages, the py for does not require a need for stepping incrementor for the iterator, 
    its default behavior is quite similar to the foreach loop in C#, so no messing with the iterator in the loop.
    but well have to pull another trick out of the bag to do multi-step iteration, AKA the Stride.
    the Range function:  returns an iterable that yields integers starting with 0, range(begin, end, stride), default range(end).
    list, tuples, strings, dictionaries and sets.

    We won't be using  == to do most complex checking, well use another trick, a keyword in fact 'is'.
"""

"""
Length - isn't a property, its a function.
where list, strings and arrays had properties and other object features, not all of these live in python in the same way.
"""
# %%
z = []
z: list = []
z.append(77)
x, y = 1, 0
x, y = y, x
def funnyFunc(x,y,z):
    print(x,y,z)
def funnyFunc(int: x, y, list: z):
    print(x,y,z)
def funnyFunc(x = 0, y = 1, z = []):
    print(x,y,z)
def funnyFunc(x,y,z):
    print(x,y,z)
    return True
def funnyFunc(x,y,z) -> bool:
    print(x,y,z)
    return True
print(x, y, z)
# %%
