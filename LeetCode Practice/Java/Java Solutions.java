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

        for (int i = 0; i < nums.length; ++i) {
            if (i == 0) {
                simpleReturn[i] = nums[i];
            } else {
                simpleReturn[i] = nums[i - 1] + simpleReturn[i];
            }
        }
        return simpleReturn;
    }
}

/*
 * Richest Customer Wealth
 * date: 11/17/22
 */
class Solution {
    public int maximumWealth(int[][] accounts) {
        int[] returnWealth = new int[accounts.length + 1];
        int insertSum = 0;
        int inter = 0;
        for (int[] i : accounts) {
            for (int j : i) {
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
 * Number of Steps to Reduce a Number to Zero
 * date 11/18/22
 */
class Solution {
    public int numberOfSteps(int num) {
        int stepCount = 0;
        while (num != 0) {
            if ((num % 2) == 0) {
                num /= 2;
                ++stepCount;
            } else {
                --num;
                ++stepCount;
            }
        }
        return stepCount;
    }
}

/*
 * FizzBuzz
 * date: 11/18/22
 */
class Solution {
    public List<String> fizzBuzz(int n) {
        List<String> retStr = new ArrayList<String>();
        if (n == 1) {
            retStr.add(Integer.toString(n));
            return retStr;
        }
        int iter = 1;
        while (iter <= n) {
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
 * Middle of the Linked List
 * 11/21/22
 */
class Solution {
    public ListNode middleNode(ListNode head) {
        ListNode cursor = head;
        ArrayList<ListNode> cursorStore = new ArrayList<ListNode>();
        int iter = 0;
        for (cursor = head; cursor != null; cursor = cursor.next) {
            cursorStore.add(cursor);
            ++iter;
        }
        iter /= 2;
        return cursor = cursorStore.get(iter);
    }
}

/*
 * Max Consecutive Ones
 * 11/21/22
 */
class Solution {
    public int findMaxConsecutiveOnes(int[] nums) {
        List<Integer> retStr = new ArrayList<Integer>();
        int counter = 0;
        for (int j : nums) {
            if (j == 0)
                counter = 0;
            else if (j == 1) {
                retStr.add(counter);
                ++counter;
            }
            retStr.add(counter);
        }
        return Collections.max(retStr);
    }
}

/*
 * Squares of a Sorted Arrayh
 * 11/23/22
 */
class Solution {
    public int[] sortedSquares(int[] nums) {
        int iter = 0;
        for (int num : nums) {
            nums[iter] = (int) Math.pow(num, 2);
            ++iter;
        }
        Arrays.sort(nums);
        return nums;
    }
}

/*
 * 12/1/2022
 * Remove Element
 */
class Solution {
    public int removeElement(int[] nums, int val) {
        int i = 0;
        int j = i;
        for (; j < nums.length; ++j) {
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
class Solution {
    public int removeDuplicates(int[] nums) {
        if (nums.length == 0)
            return 0;
        int i = 0;
        if (nums[nums.length - 1] == nums[i])
            return ++i;
        for (int j = 1; j < nums.length; ++j) {
            if (nums[j] == nums[j - 1])
                ++i;
            else
                nums[j - i] = nums[j];
        }
        return nums.length - i;
    }
}

/*
 * 12/06/2022
 * Check If N and Its Double Exist
 */
class Solution {
    public boolean checkIfExist(int[] arr) {
        int i = 0;
        int j = i;
        for (; i < arr.length; ++i) {
            for (; j < arr.length; ++j) {
                if (i != j & arr[i] * 2 == arr[j])
                    return true;
            }
            j = 0;
        }
        return false;
    }
}

/*
 * 12/28/22
 * Valid Mountain Array
 */
class Solution {
    public boolean validMountainArray(int[] arr) {
        int size = arr.length, i = 0, j = size - 1;
        while (i + 1 < size && arr[i] < arr[i + 1])
            ++i;
        while (j > 0 && arr[j - 1] > arr[j])
            --j;
        return i > 0 && i == j && j < size - 1;
    }
}

/*
 * Replace elements with greatest element on right side
 * 12/29/22
 */
class Solution {
    public int[] replaceElements(int[] arr) {
        if (arr.length <= 1) {
            Arrays.fill(arr, -1);
            return arr;
        }
        int actual, max = -1;
        for (int i = arr.length - 1; i >= 0; --i) {
            actual = arr[i];
            arr[i] = max;
            max = Math.max(actual, max);
        }
        return arr;
    }
}

/*
 * Move Zeros
 * 01/06/23
 */
class Solution {
    public void moveZeroes(int[] nums) {
        int foot = 0;
        int temp = 0;
        for (int head = 0; head <= nums.length - 1; ++head) {
            if (nums[head] != 0) {
                temp = nums[head];
                nums[head] = nums[foot];
                nums[foot] = temp;
                ++foot;
            }
        }
    }

    public boolean isEvenOdd(double numbers) {
        if ((numbers % 2) == 0) {
            return true;
        } else {
            return false;
        }
    }

    public boolean isPositive(double number) {
        return number >= 0;
    }
}

/*
 * 1/10/2023
 * Sort By Parity
 */
class Solution {
    protected boolean isPositive(int number) {
        return (number % 2) == 0;/**
                                  * XOR swap, but doesn't work on conditions such as [0,2], might be a result of
                                  * the isPositive helper'
                                  * nums[slow] ^= nums[fast]; nums[fast] ^= nums[slow]; nums[slow] ^= nums[fast];
                                  */
    }

    public int[] sortArrayByParity(int[] nums) {
        int fast = 0;
        for (int slow = 0; slow <= nums.length - 1; ++slow) {
            if (isPositive(nums[slow])) {
                nums[slow] = (nums[slow] + nums[fast]) - (nums[fast] = nums[slow]);
                ++fast;
            }
        }
        return nums;
    }
}

/*
 * 1/13/23
 * Height Checker
 */
class Solution {
    public int heightChecker(int[] heights) {
        int[] expected;
        expected = heights.clone();
        Arrays.sort(expected);
        int missmatch = 0;
        for (var i = 0; i <= heights.length - 1; ++i) {
            if (heights[i] != expected[i]) {
                ++missmatch;
            }
        }
        return (missmatch);
    }}

    /*
     * Dr. Adams' class
     * 
     */
    for(

    long l = 0;l<longArray.length-1;++l)
    {
        System.out.println(longArray[l]);
    }

/**
 * 6-13-23 Converted the Py solution for isAnagram
 */
class Solution {
    public boolean isAnagram(String s, String t) {
        if (s.length() != t.length()){
            return false;
        }
        char sArr[] = s.toCharArray();
        char tArr[] = t.toCharArray();
        Arrays.sort(sArr);
        Arrays.sort(tArr);
        for(int i = 0; i < s.length(); i++){
            if(sArr[i] != tArr[i]){
                return false;
            }
        }
        return true;
    }
}

/*
 * 6-13-23 - Two sum, failed brute force.
 */
// // dont forget to declare variables, Py is great, but java will have to do.
// int j = nums.length-1;
// for(int i = 0; i < nums.length; i++){
// //forget how to compare things with ==?
// if((nums[i] + nums[j]) == target){
// return new int[]{i, j};
// }
// else if((nums[i] + nums[j]) > target){
// j -= 1;
// }
// }
// //dont forget array dimensions when making new.
// //well this brute force approach won't work it can't handle all of the
// conditions.
// return new int[]{0,1};

/**
 * 6-13-23 two sum, hashmap.
 */
class Solution {
    public int[] twoSum(int[] nums, int target) {
        Map<Integer, Integer> seen = new HashMap<>();
        for(int i = 0; i < nums.length; i++){
            int complement = target - nums[i];
            if(seen.containsKey(complement)){
                return new int[]{seen.get(complement), i};
            }
            seen.put(nums[i],i);
        }
        return new int[]{};
    }
}

/**
 * 6-16-23
 */
class Solution {
    public List<List<String>> groupAnagrams(String[] strs) {
        if(strs.length <= 0){
            List<List<String>> emptyReturn = new ArrayList<>();
            emptyReturn.add(new ArrayList<>());
            return emptyReturn;
        }
        Map<String, List<String>> outList = new HashMap<>();
        //Using a ranged for loop.
        for(String str : strs){
            char[] sortStr = str.toCharArray();
            Arrays.sort(sortStr);
            String sorted = new String(sortStr);
            if(!outList.containsKey(sorted)){
                outList.put(sorted, new ArrayList<>());
            }
            outList.get(sorted).add(str);
        }
        return new ArrayList<>(outList.values());
    }
}

/**
 * 6-20-23
 * 347. Top K Frequent Elements.
 * Using the bucket sort method.
 */
class Solution {
    // Performing with the Bucket Sort solution.
    public int[] topKFrequent(int[] nums, int k) {
        /*
        Count the frequency of each element. 
        A hashmap counts is created to count the frequency of each element in nums.
        for each number in nums, if the number is already a key, in counts, its count ins incremented.
        if not a key in counts, its added with a count of 1.
        */ 
        Map<Integer, Integer> counts = new HashMap<>();
        for(int num : nums)
            counts.put(num, counts.getOrDefault(num, 0) + 1);

        /*
        Create a list of buckets.
        An array of list buckets, is created where the index of each bucket corresponds to a frequency.
        Each bucket will hold the elements with that frequency. 
        For each number in counts, the number is added to the bucket that corresponds to its frequency.
        */ 
        List<Integer>[] buckets = new List[nums.length + 1];
        for(int num : counts.keySet()){
            int frequency = counts.get(num);
            if(buckets[frequency] == null)
                buckets[frequency] = new ArrayList<>();
            buckets[frequency].add(num);
        }

        // collect the elemetns from the buckets
        List<Integer> topK = new ArrayList<>();
        for(int i = buckets.length - 1; i >= 0 && topK.size() < k; i--){
            if(buckets[i] != null)
                topK.addAll(buckets[i]);
        }

        /*
        Convert the list to an array.
        The elements in the 'topk' list are copied into the new array, results, which is then returned.
        */ 
        int[] result = new int[k];
        for(int i = 0; i < k; i++)
            result[i] = topK.get(i);

        return result;
    }
}



/**
 * 6-23-23
 * 238. Product of Array Except Self
 */
class Solution {
    public int[] productExceptSelf(int[] nums) {
        // lets have an int length for easy of access.
        int length = nums.length;

        // Now lets generate new arrays of the same length of nums, these will be (by default) populated with 0.
        int[] leftSide = new int[nums.length];
        int[] rightSide = new int[nums.length];
        int[] ans = new int[nums.length];

        // Lets set the first value of leftSide to 1, as to not multiply anything by zero unintentionally.
        leftSide[0] = 1;
        // Lets start the array at value 1, and that will fill in place, 
        // the product from 1->length, where 0 will be the destination of that product in the anwser.
        for(int i = 1; i < length; i++) 
        //Lets remember that we want to stop before we hit length, in going from the begining to the end (left to right), of the array.
            leftSide[i] = nums[i-1] * leftSide[i-1];

        // Now lets begin from the end of the array, and make our way to the front, same prmise, we will have to remove any potential zeros from calcuation.
        rightSide[length - 1 ] = 1;
        // now we have to start 2 away from length, one for correcting the 0->n-1 range, and another one because we don't want to compute the n-1 postion (which should be 1, but would prove errounius to the anwswer.)
        for(int i = length - 2; i >= 0; i--)
            rightSide[i] = nums[i+1] * rightSide[i+1];
        
        // Here we fill the answer array in with our product.
        for(int i = 0; i < length; i++)
            ans[i] = leftSide[i] * rightSide[i];

        return ans;
    }
}

/*
36. Valid Sudoku
6-26-23
*/ 
class Solution {
    public boolean isValidSudoku(char[][] board) {
        boolean[][] rows = new boolean[9][9];
        boolean[][] columns = new boolean[9][9];
        boolean[][] boxes = new boolean[9][9];

        for(int i = 0; i < 9; i++){
            for(int j = 0; j < 9; j++){
                if(board[i][j] != '.'){
                    int num = board[i][j] - '1';
                    int box_index = (i/3) * 3 + (j/3);
                    if(rows[i][num] || columns[j][num] || boxes[box_index][num])
                        return false;
                    rows[i][num] = true;
                    columns[j][num] = true;
                    boxes[box_index][num] = true;
                }
            }
        }
        return true;
    }
}

// 128 longest consecutive sequence
// 6-30-23
class Solution {
    public int longestConsecutive(int[] nums) {
        Set<Integer> nums_set = new HashSet<>();
        for(int num : nums)
            nums_set.add(num);
        int streak = 0;
        for(int num : nums_set){
            if(!nums_set.contains(num-1)){
                int current = num;
                int curr_streak = 1;
                while(nums_set.contains(current + 1)){
                    current++;
                    curr_streak++;
                }
                streak = Math.max(streak, curr_streak);
            }
        }
        return streak;
    }
}

/*
7/11/23
20. Valid Parentheses
*/ 
class Solution {
    public boolean isValid(String s) {
        // I check for the edge cases of valid length, odd lengths will never be possible.
        if(s.length() % 2 != 0)
            return false;
        // Lets create a stack, using the untils stack, must recall that Java for type generation to use the full spelling and capped.
        Stack<Character> stack = new Stack<>();
        // Lets use a hasmap.
        Map<Character, Character> char_set = new HashMap<>();
        // because of how few items we have, we will just create and put here in the method.
        char_set.put(')','(');
        char_set.put('}','{');
        char_set.put(']','[');

        // lets iterate throught the string, but we will have to make it a char array first. 
        for(char top : s.toCharArray()){
            // we need to use Contains Key, to check if any object mapping exist, returns a bool.
            if(char_set.containsKey(top)){
                // Here we will either eigher assign topMe the '#' char (indicating not valid) or the top of the stack (that must also be popped).
                char topMe = stack.empty() ? '#' : stack.pop();
                // Now if the topMe is not a valid option in the char set, then we can just return false.
                if(topMe != char_set.get(top)){
                    return false;
                }
            }
            // if we continue not see a fail, we can push the (should be valid) top char into thte stack.
            else
                stack.push(top);
        }
        // the status of the stack will be our return preference.
        return stack.empty();
    }
}

/*
155 min Stack
*/ 
import java.util.EmptyStackException;
class MinStack {

    Stack<Integer> stack;
    Stack<Integer> minStack;

    public MinStack() {
      stack = new Stack<Integer>();
      minStack = new Stack<Integer>();
    }
    
    public void push(int val) {
        if(stack.isEmpty())
            minStack.push(val);
        else
            minStack.push(Math.min(minStack.peek(), val));
        stack.push(val);        
    }
    
    public void pop() {
        if(stack.isEmpty())
            throw new EmptyStackException();
        stack.pop();
        minStack.pop();
    }
    
    public int top() {
        return stack.isEmpty() ? -1 : stack.peek();
    }
    
    public int getMin() {
        return minStack.isEmpty() ? -1 : minStack.peek();
    }
}

/*
150. Evaluate Reverse Polish Notation
7/23/23
 */
import java.util.function.BiFunction;
class Solution {
    public int evalRPN(String[] tokens) {
        Stack<Integer> stack = new Stack<>();
        Map<String, BiFunction<Integer, Integer, Integer>> operators = new HashMap<>();
        operators.put("+", (y,x) -> x + y);
        operators.put("-", (y,x) -> x - y);
        operators.put("*", (y,x) -> x * y);
        operators.put("/", (y,x) -> x / y);
        int result = 0;
        for(String token : tokens){
            if(operators.containsKey(token)){
                int y = stack.pop();
                int x = stack.pop();
                result = operators.get(token).apply(y,x);
                stack.push(result);
            }
            else
                stack.push(Integer.parseInt(token));
        }
        return stack.pop();
    }
}