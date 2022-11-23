# Python solutions
# Problems from LeetCode

# Running Sum of 1d Array
# date: 11/16/22
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

class Solution:
    def maximumWealth(self, accounts: List[List[int]]) -> int:
        maxList = []
        for x in accounts:
            maxList.append(sum(x))
        return max(maxList) 


# Number of Steps to Reduce a Number to Zero
# date 11/18/22

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
class Solution:
    def findNumbers(self, nums: List[int]) -> int:
        count = 0
        tempStr = " "
        for num in nums:
            tempStr = str(num)
            if (len(tempStr) % 2) == 0:
                count += 1        
        return count