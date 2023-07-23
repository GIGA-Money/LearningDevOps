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

/*
Move Zeros
01/06/23
*/
public class Solution {
    public void MoveZeroes(int[] nums) {
        int foot = 0;
        int temp = 0;
        for(int head = 0; head <= nums.Length-1; ++head){
            if(nums[head] != 0){
                temp = nums[head];
                nums[head] = nums[foot];
                nums[foot] = temp;
                ++foot;
            }
        }
    }
    public bool isEvenOdd(double numbers){
        if((numbers % 2) == 0){
            return true;
        }
        else{
            return false;
        }
    }
    public bool isPositive(double number){
        return number >= 0;
    }
}

/*
1/10/2023
Sort By Parity
*/
public class Solution {
    protected bool isPositive(int number){
        return (number % 2) == 0;
    }
    public int[] SortArrayByParity(int[] nums) {
        int fast = 0;
        for(int slow = 0; slow <= nums.Length - 1; ++slow){
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
public class Solution {
    public int HeightChecker(int[] heights) {
        int[] expected;
        expected = heights.Clone() as int[];
        Array.Sort(expected);
        int missmatch = 0;
        for(var i = 0; i <= heights.Length - 1; ++i){
            if(heights[i] != expected[i]){
                ++missmatch;
            }
        }
        return(missmatch);
    }
}


long[] longArray = new long[6];
longArray[0] = 1000;
longArray[1] = 300;
longArray[2] = 900;
longArray[3] = 420;
longArray[4] = 750;
longArray[5] = -666;

foreach (long l in longArray)
{
    Console.WriteLine(l);
}

for(long l = 0; l < longArray.Length - 1; ++l)
{
    Console.WriteLine(longArray[l]);
}

/*
6-23-23
238. Product of Array Except Self
*/
public class Solution {
    public int[] ProductExceptSelf(int[] nums) {
        int length = nums.Length;

        int[] leftSide = new int[nums.Length];
        int[] rightSide = new int[nums.Length];
        int[] ans = new int[nums.Length];

        leftSide[0] = 1;
        for(int i = 1; i < length; i++)
            leftSide[i] = nums[i-1] * leftSide[i-1];

        rightSide[length - 1] = 1;
        for(int i = length - 2; i >= 0; i--)
            rightSide[i] = nums[i+1] * rightSide[i+1];

        for(int i = 0; i < length; i++)
            ans[i] = leftSide[i] * rightSide[i];

        return ans;
    }
}

/*
36. Valid Sudoku
6-26-23
*/
public class Solution {
    public bool IsValidSudoku(char[][] board) {
        bool[][] rows = new bool[9][];
        bool[][] columns = new bool[9][];
        bool[][] boxes = new bool[9][];
         for(int i = 0; i < 9; i++){
             rows[i] = new bool[9];
             columns[i] = new bool[9];
             boxes[i] = new bool[9];
         }
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

//128 consecutive sequence
// 6-30-23
public class Solution {
    public int LongestConsecutive(int[] nums) {
        HashSet<int> nums_set = new HashSet<int>();
        foreach(int num in nums)
            nums_set.Add(num);
        int streak = 0;
        foreach(int num in nums_set){
            if(!nums_set.Contains(num-1)){
                int current = num;
                int curr_streak = 1;
                while(nums_set.Contains(current + 1)){
                    current++;
                    curr_streak++;
                }
                streak = Math.Max(streak, curr_streak);
            }
        }
        return streak;
    }
}

/*
7/11/23
20. Valid Parentheses
*/
public class Solution {
    public bool IsValid(string s) {
        if(s.Length % 2 != 0)
            return false;

        // Note we can make a non-generic object stack, or a static type stack, so <T> is optional, but would require casting later.   
        Stack<char> stack = new Stack<char>();
        // Given a hashtable, this would be duable, but to keep with the static typing, we wil lstay with the dictionary.
        Dictionary<char, char> charSet = new Dictionary<char, char>();
        charSet.Add(')', '(');
        charSet.Add(']', '[');
        charSet.Add('}', '{');
        // C# does let us iterrate over a String like its a char array.
        foreach(char top in s){
            // Lets not forget our Caps on the properties and methods.
            if(charSet.ContainsKey(top)){
                char topMe = stack.Count == 0 ? '#' : stack.Pop();
                if(topMe != charSet[top])
                    return false;
            }
            else
                stack.Push(top);
        }
        // We don't get a empty like java, so we have to check the count against 0, same as we see above.
        return stack.Count == 0;
    }
} 

/*
155 min stack
*/ 
public class MinStack {

    Stack<int> stack; 
    Stack<int> minStack;

    public MinStack() {
        stack = new Stack<int>();
        minStack = new Stack<int>();
    }
    
    public void Push(int val) {
        if(this.stack.Count == 0)
            this.minStack.Push(val);
        else
            this.minStack.Push(Math.Min(this.minStack.Peek(), val));
        
        this.stack.Push(val); 
    }
    
    public void Pop() {
        this.stack.Pop();
        this.minStack.Pop();            
    }
    
    public int Top() {
        return this.stack.Count == 0 ? -1 : this.stack.Peek();
    }
    
    public int GetMin() {
         return this.minStack.Count == 0 ? -1 : this.minStack.Peek();
    }
}

// 7/23/23 150. Evaluate Reverse Polish Notation

    public int EvalRPN(string[] tokens) {
        Stack<int> stack = new Stack<int>();
        Dictionary<string, Func<int, int, int>> operators = new Dictionary<string, Func<int, int, int>>();
        operators.Add("+", (y,x) => x + y);
        operators.Add("-", (y,x) => x - y);
        operators.Add("*", (y,x) => x * y);
        operators.Add("/", (y,x) => x / y);
        int result = 0;
        foreach(string token in tokens){
            if(operators.ContainsKey(token)){
                int y = stack.Pop();
                int x = stack.Pop();
                result = operators[token](y, x);
                stack.Push(result);
            }
            else
                stack.Push(int.Parse(token));
        }
        return stack.Pop();
    }