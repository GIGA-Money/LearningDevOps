# Python solutions
# Problems from LeetCode

# Running Sum of 1d Array
# date: 11/16/22
# %%
class Solution:
    def runningSum(self, nums: List[int]) -> List[int]:
        curNums = []
        if (len(nums) == 1):
            return nums
        if (len(nums) <= 1000):
            for x in range(len(nums)):
                if x == 0:
                    curNums.append(nums[x])
                else:
                    curNums.append(curNums[x-1] + nums[x])
        else:
            return nums
        return curNums

# Richest Customer Wealth
# date: 11/17/22
# %%


class Solution:
    def maximumWealth(self, accounts: List[List[int]]) -> int:
        maxList = []
        for x in accounts:
            maxList.append(sum(x))
        return max(maxList)


# Number of Steps to Reduce a Number to Zero
# date 11/18/22
# %%
class Solution:
    def numberOfSteps(self, num: int) -> int:
        stepCount = 0
        while num != 0:
            if (num % 2) == 0:
                num /= 2
                stepCount += 1
            else:
                num -= 1
                stepCount += 1
        return stepCount

# FizzBuzz
# Date 11/18/22
# %%


class Solution:
    def fizzBuzz(self, n: int) -> List[str]:
        retStr = []
        if n == 1:
            retStr.append(str(n))
            return retStr
        iter = 1
        while iter <= n:
            if (iter % 3) == 0 and (iter % 5) == 0:
                retStr.append("FizzBuzz")
            elif (iter % 5) == 0:
                retStr.append("Buzz")
            elif (iter % 3) == 0:
                retStr.append("Fizz")
            else:
                retStr.append(str(iter))
            iter += 1
        return retStr


# Max Consecutive Ones
# 11/21/22
# %%
class Solution:
    def findMaxConsecutiveOnes(self, nums: List[int]) -> int:
        storeList = []
        counter = 0
        for x in nums:
            if x == 0:
                counter = 0
            elif x == 1:
                storeList.append(counter)
                counter += 1
            storeList.append(counter)
        return max(storeList)

# Middle of the Linked List
# Skipped PY version, odd useage of singly-linked list by LC

# Find Numbers with Even Number of Digits
# 11/22/22
# %%


class Solution:
    def findNumbers(self, nums: List[int]) -> int:
        count = 0
        tempStr = " "
        for num in nums:
            tempStr = str(num)
            if (len(tempStr) % 2) == 0:
                count += 1
        return count


# Squares of a Sorted Arrayh
# 11/23/22
# %%
class Solution:
    def sortedSquares(self, nums: List[int]) -> List[int]:
        index = 0
        for num in nums:
            nums[index] = num**2
            index += 1
        nums.sort()
        return nums


# %%
"""
12/1/2022
Remove Element

Given a list and a to remove value.
When the remove value is found, the fast pointer will be 2 ahead, so we'll move that value into the to remove position.
The first (slow) pointer will represent new length of the list/array.
The judgement uses this to check if the "new" list/array DOES NOT have the to remove value, not if the full array/list has the value or not.
As the remove value is hit, we move the fast pointer to the slow pointer and increment both pointers,
and repeat till at the end of the list.
Internal dialog:
* looks like this question had most languages in mind when designed.
* need to capture the 2 pointer with arrays idea, I started to use it but could grable with the concept.
"""


class Solution:
    def removeElement(self, nums: List[int], val: int) -> int:
        i = 0
        for j in range(len(nums)):
            if nums[j] != val:
                nums[i] = nums[j]
                i += 1
        return i


# %%
"""
12/1/2022
Remove Element
ALT
"""


class Solution:
    def removeElement(self, nums: List[int], val: int) -> int:
        while val in nums:
            nums.remove(val)
        return len(nums)
# %%


"""
12/02/2022
remove duplicates
"""
# %%


class Solution:
    def removeDuplicates(self, nums: List[int]) -> int:
        if nums[-1] == nums[0]:
            return 1
        i = 0
        for j in range(len(nums)):
            if nums[j] != nums[i-1]:
                nums[i] = nums[j]
                i += 1
        return i
# %%


"""
12/06/2022
Check If N and Its Double Exist
"""
# %%
"""
this can't handle the cases of 0,0,1.
        for i in range(len(arr)):
            if arr[i] == 0:
                print(arr[i])
                continue
            for j in range(len(arr)):
                if (arr[j] + arr[j]) == arr[i]:
                    if arr[j] == 0:
                        print(arr[j])
                        continue
                    else:
                        return True
        return False
"""


class Solution:
    def checkIfExist(self, arr: List[int]) -> bool:
        arrSet = set()
        for i in arr:
            if float(i)/2 in arrSet or i * 2 in arrSet:
                return True
            arrSet.add(i)
        return False

# the corrected version:


class Solution:
    def checkIfExist(self, arr: List[int]) -> bool:
        for i in range(len(arr)):
            for j in range(len(arr)):
                if i != j and arr[i] * 2 == arr[j]:
                    return True


'''
12/28/22
Valid Mountain Array
'''


class Solution:
    def validMountainArray(self, arr: List[int]) -> bool:
        top, bottom, = 0, 0
        for i in range(1, len(arr) - 1):
            if arr[i-1] < arr[i] > arr[i+1]:
                top += 1
            if arr[i-1] >= arr[i] <= arr[i+1]:
                bottom += 1
        return top == 1 and bottom == 0


'''
Replace elements with greatest element on right side
12/29/22
'''


class Solution:
    def replaceElements(self, arr: List[int]) -> List[int]:
        if len(arr) <= 1:
            arr[:] = [-1]
            return arr

        curMax = -1
        for i in range(len(arr) - 1, -1, -1):
            actual = arr[i]
            arr[i] = curMax
            curMax = max(curMax, actual)

        return arr
        '''
        maxIndex = arr.index(max(arr))
        tempArr = arr[arr.index(max(arr))+1: len(arr)]
        
        trackIndex = 0
        while trackIndex != tempArr.index(max(tempArr)):
            trackIndex += 1
        
        toIndex = 0
        while toIndex != trackIndex:
            tempArr[toIndex] = max(tempArr)
            toIndex += 1    
        print(tempArr)

        toIndex = 0
        for i in range(len(tempArr)):
            arr[maxIndex+toIndex] = tempArr[i]
            toIndex += 1
        arr.append(-1)
        #print(arr)
        '''


"""
01/06/2023
Move Zeros
"""


class Solution:
    def moveZeroes(self, nums: List[int]) -> None:
        """
        Do not return anything, modify nums in-place instead.
        """
        foot = 0
        temp = 0
        for head in range(len(nums)):
            if nums[head] != 0:
                temp = nums[head]
                nums[head] = nums[foot]
                nums[foot] = temp
                foot += 1


def isEvenOdd(float: number) -> bool:
    return (number % 2) == 0
    if number % 2 == 0:
        return True
    else:
        return False

# %%


def isPositive(float: number) -> bool:
    return number >= 0
# %%


'''
1/10/23
Sort by Parity
'''


def isEven(number) -> bool:
    return (number % 2) == 0


class Solution:
    def sortArrayByParity(self, nums: List[int]) -> List[int]:
        fast = 0
        for slow in range(len(nums)):
            if isEven(nums[slow]):
                nums[slow], nums[fast] = nums[fast], nums[slow]
                fast += 1
        return nums


'''
1/13/2022
Height Checker
'''


class Solution:
    def heightChecker(self, heights: List[int]) -> int:
        expected = heights[:]
        expected.sort()
        missmatch = 0
        for i in range(len(heights)):
            if heights[i] != expected[i]:
                missmatch += 1

        return missmatch


'''
1/17/23
Thrid Max value
'''


class Solution:
    def thirdMax(self, nums: List[int]) -> int:

        nums.sort(reverse=True)
        counter = 1
        lastEl = nums[0]

        for slow in range(len(nums)):
            if nums[slow] != lastEl:
                counter += 1
                lastEl = nums[slow]
            if counter == 3:
                return nums[slow]

        return nums[0]


for l in range(len(longarray)):
    print(l)


# %%
"""
1/29/23
FillGap
"""
stringList = ['a', None, 'b', None, 'e']
gap = "bruh"


def fillGap(list: stringList = [None], string: gap = 'Missing'):
    for i in range(len(stringList)):
        if stringList[i] is None:
            stringList[i] = gap


fillGap(stringList, gap)
print(stringList)
# %%


class Guitar:
    '''
    body - physical structure.
    neck - action area
    head - string attach point
    brand - brand (and model)
    '''

    def __init__(self, numOfStrings, lengthOfNeck,
                 numOfFrets, weight, brand) -> None:
        """
        @params numOfStrings, lengthOfNeck, numOfFrets, weight, brand
        @return None
        """
        self.__numberOfStrings = numOfStrings
        self.__lengthOfNeck = lengthOfNeck
        self.__numberOfFrets = numOfFrets
        self.__guitarWeight = weight
        self.__brand = brand

    def setNumOfStrings(self, stringCount=0):
        self.__numberOfStrings = stringCount

    def getNumOfStrings(self):
        return self.__numberOfStrings


# %%
myGuitar = Guitar(0, 0, 0, 0, "Fender")
myGuitar.setNumOfStrings(7)
print(myGuitar.getNumOfStrings())
# %%

"""
when iterating through a dictionary,
by default we will get the value, not the key,
during an enumeration.
"""
ages = {"Alice": 25, "Bob": 30, "Charlie": 35}
for name in ages:
    print(ages[name])

# %%
nums = (1, 2, 3, 4, 5)
sum = 0
for num in nums:
    sum += num
print(sum)
# %%

# 347. Top K Frequent Elements: 6-20-2023.
"""
This heap based solution will require the collections and heapq libraries.
Collections provides the Counter class, this takes in a dictionary subclass for counting hashable objects.
Heapq provides and implementation of the heap queue algo, aka, the priority queue algo.
"""
import collections
import heapq

class Solution:
    def topKFrequent(self, nums: List[int], k: int) -> List[int]:
        '''
        @params: int list numns, an integer array numberes, integer k, where k is the count 
        of the most frequent elements that we wish to return.
        @return: A list of the most frequent numbers requested.
        Additional test cases can include but not limited to:
        k equals 1. 
        k equals the number of unique elements in the array. 
        k is much smaller than the number of unique elements in the array.
        k is much larger than the number of unique elements in the array.
        All elements in the array are the same.
        '''
        # This handle edge case: checks if the list is just one element or less.
        if len(nums) <= 1:
            return nums
        
        # Counter the frequency of each element: the counter class is used to count the frequency of each element in 
        # the list nums. The results, a dictionary were the keys are the elements in nums, 
        # and the values are their frequencies.
        freqs = collections.Counter(nums)
        # initialize a heap.
        heap = []
        # Push each element ont o the heap. given each element-frequency pair in 'freqs',
        # a tuple of (frequency, element) is pushed to the heap.
        # The frequency is negated to make the heap behave like a max-heap.
        # This is a result of python's implementation of a max-heap.
        # only provides a min-heap, but we want the largest frequencies to be at the top of the heap.
        for num, freq in freqs.items():
            heapq.heappush(heap, (freq, num))
            # Pop the least frequent elements from the heap:
            # if the size of the heap exceeds 'k' the least frequent element is popped from the heap. 
            # This is done within the loop, so the heap always contains 'k' 
            # most frequent elements at any point during the iteration.
            while len(heap) > k:
                heapq.heappop(heap)
        # Return the elements in the heap: the 'k' elements remaining in the heap are the 'k' most frequent elements.
        # Then return in list format.
        return [x[1] for x in heap]
    

# 6-23-23 238. Product of array except self
# %%
class Solution:
    def productExceptSelf(self, nums: List[int]) -> List[int]:
        # Pre-assign lenght so we don't have to bring it forward each time we need it.
        length = len(nums)
    
        # Initialize two empty arrays, leftSide and rightSide.
        leftSide, rightSide, answer = [0]*length, [0]*length, [0]*length
    
        # leftSide[i] contains the product of all the numbers to the left
        leftSide[0] = 1 
        for i in range(1, length):
            leftSide[i] = nums[i - 1] * leftSide[i - 1]
    
        # rightSide[i] contains the product of all the numbers to the right
        rightSide[length - 1] = 1
        for i in reversed(range(length - 1)):
            rightSide[i] = nums[i + 1] * rightSide[i + 1]
    
        # Construct the answer array
        for i in range(length):
            answer[i] = leftSide[i] * rightSide[i]
    
        return answer

# 6-26-23
# 36. Valid Sudoku
class Solution:
    def isValidSudoku(self, board: List[List[str]]) -> bool:
        rows = [set() for _ in range(9)]
        columns = [set() for _ in range(9)]
        boxes = [set() for _ in range(9)]

        for i in range(9):
            for j in range(9):
                if board[i][j] != '.':
                    num = board[i][j]
                    box_index = (i//3)*3+j//3

                    if num in rows[i] or num in columns[j] or num in boxes[box_index]:
                        return False
                
                    rows[i].add(num)
                    columns[j].add(num)
                    boxes[box_index].add(num)

        return True
    

# 6-30-23
# Longest consecutive sequence
class Solution:
    def longestConsecutive(self, nums: List[int]) -> int:
        # The set that contains all the numbers in the input list
        nums_set = set(nums)
        #  the length of the longest consecutive sequence found
        streak = 0
        # for each number in the set, if its thhe first number of a sequence (IE num-1 is not in the set),
        # we check for all other numbers in the sequence.
        for num in nums_set:
            if num - 1 not in nums_set:
                current = num
                current_streak = 1
                while current + 1 in nums_set:
                    current += 1
                    current_streak += 1
                
                streak = max(streak, current_streak)
            
        return streak