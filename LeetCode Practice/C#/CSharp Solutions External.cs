/* C# taylored solutions.
Problems from LeetCode
*/

/*
Running Sum of 1d Array
date: 11/16/22
*/

public class Solution {
    public int[] RunningSum(int[] nums) {
            if (nums == null || nums.Length <= 1)
                return nums;

            int total = nums[0];
            for (int i = 1; i < nums.Length; ++i)
            {
                nums[i] += total;
                total = nums[i];
            }
        return nums;
        }
    }
	
/*
Richest Customer Wealth
date: 11/17/22
*/

public class Solution {
    public int MaximumWealth(int[][] accounts) {        
        List<int> maxList = new List<int>(accounts.Length);
        foreach (var x in accounts)
            maxList.Add(x.Sum(y => y));
        return maxList.Max();
    }
}