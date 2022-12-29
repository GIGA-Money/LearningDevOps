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

/*
Middle of the Linked List
11/21/22
*/
public class Solution {
    public ListNode MiddleNode(ListNode head) {
        ListNode cursor;
        List<ListNode> cursorStore = new List<ListNode>();
        int iter = 0;
         for(cursor = head; cursor != null; cursor = cursor.next){
            cursorStore.Add(cursor);
            ++iter;
        }
        iter /= 2;
        return cursor = cursorStore[iter];
    }
}

/*
Max Consecutive Ones
11/21/22
*/
public class Solution {
    public int FindMaxConsecutiveOnes(int[] nums) {
        List<int> retCnt = new List<int>();
        int count = 0;
        foreach(int index in nums){
            if (index == 0)
                count = 0;
            else if (index == 1){
                retCnt.Add(count);
                ++count;
            }
            retCnt.Add(count);
        }
        return retCnt.Max();
    }
}

/*
Squares of a Sorted Arrayh
11/23/22
*/
public class Solution {
    public int[] SortedSquares(int[] nums) {
        int index = 0;
        foreach(int num in nums){
            nums[index] = num*num;
            ++index;
        }
        Array.Sort(nums);
        return nums;
    }
}

/*
12/1/2022
Remove Element
*/
public class Solution {
    public int RemoveElement(int[] nums, int val) {
        int i = 0;
        int j = i;
        for(; j < nums.Length; ++j){
            if (nums[j] != val) {
                nums[i] = nums[j];
                ++i;
            }
        }
        return i;
    }
}

/*
 * 12/02/2022
 * remove duplicates
*/
public class Solution {
    public int RemoveDuplicates(int[] nums) {
        if (nums.Length == 0)
            return 0;
        int i = 0;
        if (nums.Last() == nums[i])
            return ++i;
        
         for(int j = 1; j < nums.Length; ++j){
             if(nums[j] == nums[j-1])
                 ++i;
             else
                 nums[j-i] = nums[j];
         }
        return nums.Length - i;
    }
}


/*
12/06/2022
Check If N and Its Double Exist
*/
public class Solution {
    public bool CheckIfExist(int[] arr) {
        int i = 0;
        int j = i;
        for(; i < arr.Length; ++i){
            for(; j < arr.Length; ++j){
                if (i != j & arr[i] * 2 == arr[j])
                    return true;
            }
            j = 0;
        }                    
        return false;
    }
}

/*
12/28/22
Valid Mountain Array
*/
public class Solution {
    public bool ValidMountainArray(int[] arr) {
        int size = arr.Length, i = 0, j = size - 1;
        while(i + 1 < size && arr[i] < arr[i+1]) ++i;
        while(j > 0 && arr[j-1] > arr[j]) --j;
        return i > 0 && i == j && j < size - 1;
    }
}

/*
Replace elements with greatest element on right side
12/29/22
*/
public class Solution {
    public int[] ReplaceElements(int[] arr) {
         if(arr.Length <= 1){
            Array.Fill(arr, -1);
            return arr;
        }
        int actual, max = -1;
        for(int i =  arr.Length - 1; i >= 0; --i){
            actual = arr[i];
            arr[i] = max;
            max = Math.Max(actual, max);
         }
        return arr;
    }
}