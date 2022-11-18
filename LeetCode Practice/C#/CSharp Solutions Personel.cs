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

/*
Number of Steps to Reduce a Number to Zero
date 11/18/22
*/
public class Solution {
    public int NumberOfSteps(int num) {
        int stepCount = 0;
        while (num != 0){
            if ((num % 2 ) == 0){
                num /= 2;
                ++stepCount;
            }
            else{
                --num;
                ++stepCount;
            }
        }
        return stepCount;
    }
}

/*
FizzBuzz
date: 11/18/22
*/
public class Solution {
    public IList<string> FizzBuzz(int n) {
        IList<string> retStr = new List<string>();
        if (n == 1){
            retStr.Add(n.ToString());
            return retStr;
        }
        int iter = 1;
        while (iter <= n){
            if ((iter % 3) == 0 && (iter % 5) == 0)
                retStr.Add("FizzBuzz");
            else if ((iter % 5) == 0)
                 retStr.Add("Buzz");
            else if ((iter % 3) == 0)
                retStr.Add("Fizz");
            else
                retStr.Add(iter.ToString());
            ++iter;
        }        
        return retStr;
    }
}