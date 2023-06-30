/* C++ solutions
Problems from LeetCode
*/

/*
Running Sum of 1d Array
date: 11/16/22
*/
class Solution
{
public:
    vector<int> runningSum(vector<int> &nums)
    {
        vector<int> simpleReturn{};
        /* this code being here does increas the run time.
        but was helpful for edge caes.
        if (nums.size() == 1)
             return nums;
        */
        for (auto i = nums.begin(); i != nums.end(); ++i)
        {
            if (i == nums.begin())
            {
                simpleReturn.push_back(nums.front());
            }
            else
            {
                simpleReturn.push_back(simpleReturn.back() + *i);
            }
        }
        return simpleReturn;
    }
};

/*
Richest Customer Wealth
date: 11/17/22
*/
class Solution
{
public:
    int maximumWealth(vector<vector<int>> &accounts)
    {
        vector<int> maxList = {};
        int insertSum = 0;
        for (auto &i : accounts)
        {
            for (auto &j : i)
            {
                insertSum += j;
            }
            maxList.push_back(insertSum);
            insertSum = 0;
        }
        return *std::max_element(maxList.begin(), maxList.end());
    }
};

/*
Number of Steps to Reduce a Number to Zero
date 11/18/22
*/
class Solution
{
public:
    int numberOfSteps(int num)
    {
        int stepCount = 0;
        while (num != 0)
        {
            if ((num % 2) == 0)
            {
                num /= 2;
                ++stepCount;
            }
            else
            {
                --num;
                ++stepCount;
            }
        }
        return stepCount;
    }
};

/*
Number of Steps to Reduce a Number to Zero
*/
/* a solution from "mizardx" on leet code, walking through the bitwise solution.
```Here is a expanded version,
without built-in functions.
Speed is dependant upon size of integer (32 bits in this case),
not the value itself.
```
if (num == 0) return 0;
// Bitcount of num
int n = num;
n = ((n&~0x55555555)>>1) + (n&0x55555555); // 10
n = ((n&~0x33333333)>>2) + (n&0x33333333); // _100
n = ((n&~0x0F0F0F0F)>>4) + (n&0x0F0F0F0F); // ____1000
n += n >> 8; // ___10000
n += n >> 16; // // __100000
n &= 0x3F;
// Copy all ones rightwards. 00101001 -> 00111111 (Not exactly clz, but close enough)
int m = num;
m |= m >> 1;
m |= m >> 2;
m |= m >> 4;
m |= m >> 8;
m |= m >> 16;
// Bitcount again
m = ((m&~0x55555555)>>1) + (m&0x55555555); // 10
m = ((m&~0x33333333)>>2) + (m&0x33333333); // _100
m = ((m&~0x0F0F0F0F)>>4) + (m&0x0F0F0F0F); // ____1000
m += m >> 8; // ___10000
m += m >> 16; // // __100000
m &= 0x3F;
return m + n - 1;
*/

/*
FizzBuzz
date: 11/18/22
*/
class Solution
{
public:
    vector<string> fizzBuzz(int n)
    {
        vector<string> retStr = {};
        if (n == 1)
        {
            retStr.push_back(std::to_string(n));
            return retStr;
        }
        int iter = 1;
        while (iter <= n)
        {
            if ((iter % 3) == 0 && (iter % 5) == 0)
                retStr.push_back("FizzBuzz");
            else if ((iter % 5) == 0)
                retStr.push_back("Buzz");
            else if ((iter % 3) == 0)
                retStr.push_back("Fizz");
            else
                retStr.push_back(std::to_string(iter));

            ++iter;
        }

        return retStr;
    }
};

/*
Middle of the Linked List
11/21/22
*/
class Solution
{
public:
    ListNode *middleNode(ListNode *head)
    {
        int iter = 0;
        ListNode *cursor;
        vector<ListNode *> cursorStore;
        // length of LL;
        for (cursor = head; cursor != NULL; cursor = cursor->next)
        {
            cursorStore.push_back(cursor);
            ++iter;
        }
        // divide by 2 to get middle
        iter /= 2;
        return cursor = cursorStore[iter];
    }
};

/*
Max Consecutive Ones
11/21/22
*/
class Solution
{
public:
    int findMaxConsecutiveOnes(vector<int> &nums)
    {
        vector<int> maxCount = {};
        int counter = 0;
        for (auto &j : nums)
        {
            if (j == 0)
                counter = 0;
            else if (j == 1)
            {
                maxCount.push_back(counter);
                ++counter;
            }
            maxCount.push_back(counter);
        }
        return *std::max_element(maxCount.begin(), maxCount.end());
    }
};

/*
Find Numbers with Even Number of Digits
*/
class Solution
{
public:
    int findNumbers(vector<int> &nums)
    {
        int count = 0;
        string tempStr = " ";
        for (auto &num : nums)
        {
            tempStr = std::to_string(num);
            if ((tempStr.length() % 2) == 0)
                ++count;
        }
        return count;
    }
};

/*
Squares of a Sorted Arrayh
11/23/22
*/
class Solution
{
public:
    vector<int> sortedSquares(vector<int> &nums)
    {
        int iter = 0;
        for (auto &num : nums)
        {
            nums[iter] = pow(num, 2);
            ++iter;
        }
        std::sort(nums.begin(), nums.end());
        return nums;
    }
};

/*
12/1/2022
Remove Element
*/
class Solution
{
public:
    int removeElement(vector<int> &nums, int val)
    {
        int i = 0;
        for (int j = 0; j < nums.size(); ++j)
        {
            if (nums[j] != val)
            {
                nums[i] = nums[j];
                ++i;
            }
        }
        return i;
    }
};

/*
 * 12/02/2022
 * remove duplicates
 */
class Solution
{
public:
    int removeDuplicates(vector<int> &nums)
    {
        if (nums.size() == 0)
            return 0;

        int i = 0;
        if (nums.back() == nums[i])
            return ++i;

        for (int j = 1; j < nums.size(); ++j)
        {
            if (nums[j] == nums[j - 1])
            {
                ++i;
            }
            else
            {
                nums[j - i] = nums[j];
            }
        }
        return nums.size() - i;
    }
};

/*
12/06/2022
Check If N and Its Double Exist
*/
class Solution
{
public:
    bool checkIfExist(vector<int> &arr)
    {
        int i = 0;
        for (; i < arr.size(); ++i)
            for (int j = 0; j < arr.size(); ++j)
                if (i != j & arr[i] * 2 == arr[j])
                    return true;
        return false;
    }
};

/*
12/28/22
Valid Mountain Array
*/
class Solution
{
public:
    bool validMountainArray(vector<int> &arr)
    {
        int size = arr.size(), i = 0, j = size - 1;
        while (i + 1 < size && arr[i] < arr[i + 1])
            ++i;
        while (j > 0 && arr[j - 1] > arr[j])
            --j;
        return i > 0 && i == j && j < size - 1;
    }
};

/*
Replace elements with greatest element on right side
12/29/22
*/
class Solution
{
public:
    vector<int> replaceElements(vector<int> &arr)
    {
        if (arr.size() <= 1)
        {
            arr.clear();
            arr.push_back(-1);
            return arr;
        }
        int max = -1;
        for (int i = arr.size() - 1; i >= 0; --i)
        {
            max = std::max(max, exchange(arr[i], max));
        }
        return arr;
    }
};

/*
Move Zeros
01/06/23
*/
class Solution
{
public:
    void moveZeroes(vector<int> &nums)
    {
        int foot = 0;
        int temp = 0;
        for (int head = 0; head <= nums.size() - 1; ++head)
        {
            if (nums[head] != 0)
            {
                temp = nums[head];
                nums[head] = nums[foot];
                nums[foot] = temp;
                ++foot;
            }
        }
    }
    bool isEvenOdd(double numbers)
    {
        if ((numbers % 2) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool isPositive(double number)
    {
        return number >= 0;
    }
};

/*
1/10/2023
Sort By Parity
*/
class Solution
{
public:
    bool isPositive(int number)
    {
        return (number % 2) == 0;
    }
    vector<int> sortArrayByParity(vector<int> &nums)
    {
        int fast = 0;
        for (int slow = 0; slow <= nums.size() - 1; ++slow)
        {
            if (isPositive(nums[slow]))
            {
                swap(nums[slow], nums[fast]);
                ++fast;
            }
        }
        return nums;
    }
};

/*
1/13/23
Height Checker
*/
class Solution
{
public:
    int heightChecker(vector<int> &heights)
    {
        int missmatch = 0;
        vector<int> expected;
        copy(heights.begin(), heights.end(), back_inserter(expected));
        sort(expected.begin(), expected.end());
        for (int i = 0; i <= heights.size() - 1; ++i)
        {
            if (heights[i] != expected[i])
            {
                ++missmatch;
            }
        }
        return missmatch;
    }
};

/*
1/17/23
Thrid Max value
*/
class Solution
{
public:
    int thirdMax(vector<int> &nums)
    {
        sort(nums.begin(), nums.end(), greater<int>());
        int counter = 1;
        int lastEl = nums[0];
        for (int slow = 0; slow < nums.size(); ++slow)
        {
            if (nums[slow] != lastEl)
            {
                ++counter;
                lastEl = nums[slow];
            }
            if (counter == 3)
            {
                return nums[slow];
            }
        }
        return nums[0];
    }
};

/*
 * Dr. Adams' class
 *
 */
for (long l = 0; l < longArray.size() - 1; ++l)
{
    cout << longArray[l] << endl;
}

/*
238. Product of Array Except Self
6-23-23
*/
class Solution
{
public:
    vector<int> productExceptSelf(vector<int> &nums)
    {
        int length = nums.size();

        vector<int> leftSide = nums;
        vector<int> rightSide = nums;
        vector<int> ans = nums;

        leftSide[0] = 1;
        for (int i = 1; i < length; i++)
            leftSide[i] = nums[i - 1] * leftSide[i - 1];

        rightSide[length - 1] = 1;
        for (int i = length - 2; i >= 0; i--)
            rightSide[i] = nums[i + 1] * rightSide[i + 1];

        for (int i = 0; i < length; i++)
            ans[i] = rightSide[i] * leftSide[i];

        return ans;
    }
};

/*
36. Valid Sudoku
6-26-23
*/
class Solution
{
public:
    bool isValidSudoku(vector<vector<char>> &board)
    {
        const int count = 9;
        bool rows[count][count] = {false};
        bool columns[count][count] = {false};
        bool boxes[count][count] = {false};
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
            {
                if (board[i][j] != '.')
                {
                    int num = board[i][j] - '1';
                    int box_index = (i / 3) * 3 + (j / 3);
                    if (rows[i][num] || columns[j][num] || boxes[box_index][num])
                        return false;
                    rows[i][num] = true;
                    columns[j][num] = true;
                    boxes[box_index][num] = true;
                }
            }
        }
        return true;
    }
};


//128 consecutive sequence:
// 6-30-23
class Solution {
public:
    int longestConsecutive(vector<int>& nums) {
        set<int> nums_set;
        int streak = 0;
        for(int num : nums)
            nums_set.insert(num);

        for(int num : nums_set){
            if(nums_set.find(num-1) == nums_set.end()){
                int curr = num;
                int curr_streak = 1;
                while(nums_set.find(curr + 1) != nums_set.end()){
                    curr++;
                    curr_streak++;
                }
                streak = max(streak, curr_streak);
            }
        }
        return streak;
    }
};