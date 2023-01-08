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
            if (num  % 2) == 0:
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
The judgement uses this to check if the "new" list/array DOESNOT have the to remove value, not if the full array/list has the value or not.
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
                i+=1         
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