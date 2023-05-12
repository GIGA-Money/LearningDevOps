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

# Leet code: 242. Valid Anagram
class Solution:
    def isAnagram(self, s: str, t: str) -> bool:
        """
        @Params: s: str, t: str
        @Returns: bool
        I did decide to use the count method, knowing it is slower.
        This was just want came to mind at the time. 
        """
        if len(s) != len(t):
            return False
        # sorted(s)
        # sorted(t)
        for i in s:
            if s.count(i) != t.count(i):
                return False
        return True

solution = Solution()
print(solution.isAnagram("aacc", "ccac"))

# Road-map: solution involved the following:
class Solution:
    def isAnagram(self, s: str, t: str) -> bool:
        if len(s) != len(t):
            return False

        countS, countT = {}, {}

        for i in range(len(s)):
            countS[s[i]] = 1 + countS.get(s[i], 0)
            countT[t[i]] = 1 + countT.get(t[i], 0)
        return countS == countT
# Starting out the same, Neet code, went with the length check.
# proceeded to create dictionaries for the counts of each letter.
# intreating through the dictionaries, if the counts are not the same,
# the result will always return the expected result. 
# this solution, uses a dictionary to get the count of each element.
# condenses down what I had above but into a dictionary.
# and only hvaeing 2 returns rather than a possible 3