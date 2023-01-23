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

/*
Number of Steps to Reduce a Number to Zero
date 11/18/22
*/
class Solution {
    public int numberOfSteps(int num) {
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
class Solution {
    public List<String> fizzBuzz(int n) {
        List<String> retStr = new ArrayList<String>();
        if (n == 1){
            retStr.add(Integer.toString(n));
            return retStr;
        }
        int iter = 1;
        while (iter <= n){
            if ((iter % 3) == 0 && (iter % 5) == 0)
                retStr.add("FizzBuzz");
            else if ((iter % 5) == 0)
                 retStr.add("Buzz");
            else if ((iter % 3) == 0)
                retStr.add("Fizz");
            else
                retStr.add(Integer.toString(iter));
            ++iter;
        }
        return retStr;
    }
}

/*
Middle of the Linked List
11/21/22
*/
class Solution {
    public ListNode middleNode(ListNode head) {
        ListNode cursor = head;
        ArrayList<ListNode> cursorStore = new ArrayList<ListNode>();
        int iter = 0;
        for(cursor = head; cursor != null; cursor = cursor.next){
            cursorStore.add(cursor);
            ++iter;
        }
        iter /= 2;        
        return cursor = cursorStore.get(iter);
    }
}

/*
Max Consecutive Ones
11/21/22
*/
class Solution {
    public int findMaxConsecutiveOnes(int[] nums) {
        List<Integer> retStr = new ArrayList<Integer>();
        int counter = 0;
        for (int j : nums){
            if (j == 0)
                counter = 0;
            else if (j == 1){
                retStr.add(counter);
                ++counter;
            }
            retStr.add(counter);
        }
        return Collections.max(retStr);
    }
}

/*
Squares of a Sorted Arrayh
11/23/22
*/
class Solution {
    public int[] sortedSquares(int[] nums) {
        int iter = 0;
        for(int num : nums){
            nums[iter] = (int) Math.pow(num, 2);
            ++iter;
        }
        Arrays.sort(nums);
        return nums;
    }
}

/*
12/1/2022
Remove Element
*/
class Solution {
    public int removeElement(int[] nums, int val) {
        int i = 0;
        int j = i;
        for(;j < nums.length; ++j){
            if (nums[j] != val)
            {
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
class Solution {
    public int removeDuplicates(int[] nums) {
        if (nums.length == 0)
            return 0;
        int i = 0;
        if (nums[nums.length - 1] == nums[i])
            return ++i;
         for(int j = 1; j < nums.length; ++j){
             if(nums[j] == nums[j-1]) ++i;
             else nums[j-i] = nums[j];
         }
        return nums.length - i;
    }
}

/*
12/06/2022
Check If N and Its Double Exist
*/
class Solution {
    public boolean checkIfExist(int[] arr) {
        int i = 0;
        int j = i;
        for(; i < arr.length; ++i){
            for(; j < arr.length; ++j){
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
class Solution {
    public boolean validMountainArray(int[] arr) {
        int size = arr.length, i = 0, j = size - 1;
        while(i + 1 < size && arr[i] < arr[i+1]) ++i;
        while(j > 0 && arr[j-1] > arr[j]) --j;
        return i > 0 && i == j && j < size - 1;
    }
}

/*
Replace elements with greatest element on right side
12/29/22
*/
class Solution {
    public int[] replaceElements(int[] arr) {
        if(arr.length <= 1){
            Arrays.fill(arr, -1);
            return arr;
        }
        int actual, max = -1;
        for(int i =  arr.length - 1; i >= 0; --i){
            actual = arr[i];
            arr[i] = max;
            max = Math.max(actual, max);
        }
        return arr;
    }
}

/*
Move Zeros
01/06/23
*/
class Solution {
    public void moveZeroes(int[] nums) {
        int foot = 0;
        int temp = 0;
        for(int head = 0; head <= nums.length-1; ++head){
            if(nums[head] != 0){
                temp = nums[head];
                nums[head] = nums[foot];
                nums[foot] = temp;
                ++foot;
            }
        }
    }
    public boolean isEvenOdd(double numbers){
        if((numbers % 2) == 0){
            return true;
        }
        else{
            return false;
        }
    }
    public boolean isPositive(double number){
        return number >= 0;
    }   
}

/*
1/10/2023
Sort By Parity
*/
class Solution {
    protected boolean isPositive(int number){
        return (number % 2) == 0;
        /** XOR swap, but doesn't work on conditions such as [0,2], might be a result of the isPositive helper'
        nums[slow] ^= nums[fast]; nums[fast] ^= nums[slow]; nums[slow] ^= nums[fast];
         */
    }
    public int[] sortArrayByParity(int[] nums) {
         int fast = 0;
          for(int slow = 0; slow <= nums.length - 1; ++slow){
              if (isPositive(nums[slow])){
                  nums[slow] = (nums[slow] + nums[fast]) - (nums[fast] = nums[slow]);
                  ++fast;
              }
          }
          return nums;
    }
}

/*
1/13/23
Height Checker
*/
class Solution {
    public int heightChecker(int[] heights) {
        int[] expected;
        expected = heights.clone();
        Arrays.sort(expected);
        int missmatch = 0;
        for(var i = 0; i <= heights.length - 1; ++i){
            if(heights[i] != expected[i]){
                ++missmatch;
            }
        }
        return(missmatch);
    }
}


/*
 * Dr. Adams' class
 * 
*/
for(long l = 0; l < longArray.length - 1; ++l)
{
    System.out.println(longArray[l]);
}