/* Java solutions
Problems from LeetCode
*/

/*
Running Sum of 1d Array
date: 11/16/22
*/

class Solution {
    public int[] runningSum(int[] nums) {
        int simpleReturn[] = nums;
        if (nums.length == 1)    
            return nums;
        
        for (int i = 0; i < nums.length; ++i){
            if (i == 0){
                simpleReturn[i] = nums[i]; 
            }
            else {
                simpleReturn[i] = nums[i-1] + simpleReturn[i];
            }
        }
        return simpleReturn;
        }
    }
	
/*
Richest Customer Wealth
date: 11/17/22
*/

class Solution {
    public int maximumWealth(int[][] accounts) {
        int[] returnWealth = new int[accounts.length + 1];
        int insertSum = 0;
        int inter = 0;
        for (int[] i : accounts){
            for (int j : i){
                insertSum += j;
            }
            returnWealth[inter] = insertSum;
            insertSum = 0;
            inter++;
        }
        Arrays.sort(returnWealth);
        return returnWealth[returnWealth.length - 1];
    }
}