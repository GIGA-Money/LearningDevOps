# Following solutions are the results of my Python solutions from LeetCode practices
# that I followed along with the road-map from NeetCode (a YTer).
"""
header: int = 0; footer: int = 0
print(header, footer)
# header = header + 1
# print(header)
# header = header + 1
# %%
nums = [1,2,3,4]
for i in range(len(nums)):
    # print(nums[i])
    header = nums[i]
    for j in range(i, len(nums)):
        # print(nums[j])
        # footer = nums[j]
        if header == nums[j]:
            print(header, j)
        # print(i, header, j)
# %%
The above code was practice for the following problem:
"""

# Problem number: 217. Contains Duplicate
"""
Given an integer array nums, return true if any value appears at least twice in the array, 
and return false if every element is distinct.
https://leetcode.com/problems/contains-duplicate/
"""
class Solution:
    def containsDuplicate(self, nums: List[int]) -> bool:
        """
        @Params: nums: List[int]
        @Returns: bool
        I opted for the set length comparison, my 2 pointer solution was 
        actually timing out because it was to slow for the super sized testcases.
        """    
        if len(nums) <= 1:
            return False
        nums2 = set(nums)
        if len(nums2) == len(nums):
            return False
        return True

"""
Neet Code's solution opted for the hashset, added the visited elements to the
set and if they are in the hashset, it means there is a duplicate.
class Solution:
    def containsDuplicate(self, nums: List[int]) -> bool:
        hashset = set()

        for n in nums:
            if n in hashset:
                return True
            hashset.add(n)
        return False
"""