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