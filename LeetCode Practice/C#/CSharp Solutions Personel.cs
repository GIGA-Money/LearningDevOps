/* C# solutions
Problems from LeetCode
*/

/*
Running Sum of 1d Array
date: 11/16/22
*/
public class Solution {
    public int[] RunningSum(int[] nums) {
        if (nums.Length == 1)
            return nums;
            
        for (int i = 0; i < nums.Length; ++i){
            if (i == 0){
                nums[i] = nums[i]; 
            }
            else {
                nums[i] = nums[i-1] + nums[i];
            }
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
        int[] returnWealth = new int[accounts.GetLength(0)];
        int insertSum = 0;
        int inter = 0;
        foreach (int[] i in accounts){
            foreach (int j in i){
                insertSum += j;
            }
            returnWealth[inter] = insertSum;
            insertSum = 0;
            ++inter;
        }
        Array.Sort(returnWealth);
        return returnWealth.Last();
    }
}