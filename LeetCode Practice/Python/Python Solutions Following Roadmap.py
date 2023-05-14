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
# and only having 2 returns rather than a possible 3


class Solution:
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        """
        @Params: nums: List[int], target: int
        @Returns: List[int]
        """
        # hash map
        seen: dict = {}
        for i, num in enumerate(nums):
            complement = target - num
            if complement in seen:
                return [seen[complement], i]
            seen[num] = i
        return []
        # brute force
        # foot = len(nums)-1
        # for head in range(len(nums)):
        #     if (nums[head] + nums[foot]) == target:
        #         return [nums.index(nums[head]), nums.index(nums[foot])]
        #     elif (nums[head] + nums[foot]) > target:
        #         foot -= 1
        #         print(foot)
        # brute force solution:
        #  if len(nums) <= 2:
        #     return [nums.index(nums[0]), nums.index(nums[1])]
        # for i in range(len(nums)):
        #     for j in range(len(nums)):
        #         if i == j:
        #             continue
        #         elif nums[i] + nums[j] == target:
        #             return [nums.index(nums[i]), nums.index(nums[j])]


# 49: group Anagrams problem:
# https://leetcode.com/problems/group-anagrams
class Solution:
    def isAnagram(self, s: str, t: str) -> bool:
        """
        @Params: s, t
        @Returns: bool
        """
        if len(s) != len(t):
            return False

        countS, countT = {}, {}

        for i in range(len(s)):
            countS[s[i]] = 1 + countS.get(s[i], 0)
            countT[t[i]] = 1 + countT.get(t[i], 0)
        return countS == countT
    
    def groupAnagrams(self, strs: List[str]) -> List[List[str]]:
        """
        @Params: List of strings strs: List[str]
        @Returns: List of lists of strings
        the idea was to loop through,
        do the comparison on each anagram to each other word,
        then append them.
        """
         if len(strs) <= 0:
             return [[""]]
        outList = []
        for i in strs:
            print(i)
            for j in strs:
                if self.isAnagram(i,j):
                    outList.append(j)
                    print(i,j)
        return outList
    
    """
    This function groupAnagrams takes a list of strings as input and groups all the anagrams together into separate lists. 
    It uses a dictionary to store the anagrams where the keys are the sorted version of the string and the values are the original strings.
    The function loops through each string in the input list, 
    sorts the string to create a sorted version of the string and stores it in storString. 
    It then checks if this storString is already in the dictionary, 
    if yes, it appends the original string to the list of strings associated with the key (the sorted string) 
    in the dictionary. If not, it adds a new key-value pair to the dictionary with the key as the sorted string 
    and the value as a list containing the original string.
    The output of the function is a list of lists of strings, 
    where each list contains all the anagrams. 
    The function returns the values of the dictionary as a list using list(outList.values()).
    """    
    def groupAnagrams(self, strs: List[str]) -> List[List[str]]:
        # This is a proper brute force solution.
        """
        @Params: List of strings strs: List[str]
        @Returns: List of lists of strings
        This solution uses a dictionary to store the anagrams.
        We loop through the list of strings, using storString to hold a sorted string
        and store sorted strings in a dictionary.
        If the storString is already in the dictionary, we append it to the list of lists.
        else, we append it to the dictionary at the location of string without sorting.
        """
        if len(strs) <= 0:
            return [[""]]
        outList = {}
        for i in strs:
            storString=str(sorted(i))
            if storString in outList:
                outList[storString].append(i)
            else:
                outList[storString] = [i]
        return list(outList.values())