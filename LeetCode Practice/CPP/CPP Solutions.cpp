/* C++ solutions
Problems from LeetCode
*/

/*
Running Sum of 1d Array
date: 11/16/22
*/

class Solution {
public:
    vector<int> runningSum(vector<int>& nums) {
       vector<int> simpleReturn {};
	   /* this code being here does increas the run time.
	   but was helpful for edge caes.
	   if (nums.size() == 1)
            return nums;
	   */
       for (auto i = nums.begin(); i != nums.end(); ++i){
           if (i == nums.begin()){
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

class Solution {
public:
    int maximumWealth(vector<vector<int>>& accounts) {
        vector<int> maxList = {};
        int insertSum = 0;
        for (auto& i: accounts){
            for (auto& j: i){
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

class Solution {
public:
    int numberOfSteps(int num) {
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
class Solution {
public:
    vector<string> fizzBuzz(int n) {
        vector<string> retStr = {};
        if (n == 1){
            retStr.push_back(std::to_string(n));
            return retStr;
        }
        int iter = 1;
        while (iter <= n){
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
class Solution {
public:
    ListNode* middleNode(ListNode* head) {
        int iter = 0;
        ListNode* cursor;
        vector<ListNode*> cursorStore;
        // length of LL;
        for(cursor = head; cursor != NULL; cursor = cursor->next){
            cursorStore.push_back(cursor);
            ++iter;
        }
        //divide by 2 to get middle
        iter /= 2;
        return cursor = cursorStore[iter];
    }
};

/*
Max Consecutive Ones
11/21/22
*/
class Solution {
public:
    int findMaxConsecutiveOnes(vector<int>& nums) {
        vector<int> maxCount = {};
        int counter = 0;
        for (auto& j: nums){
               if(j == 0)
                   counter = 0;
                else if(j == 1){
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
class Solution {
public:
    int findNumbers(vector<int>& nums) {
        int count = 0;
        string tempStr = " ";
        for(auto& num : nums){
            tempStr = std::to_string(num);
            if((tempStr.length() % 2 ) == 0)
                ++count;
        }
        return count;
    }
};

/*
Squares of a Sorted Arrayh
11/23/22
*/
class Solution {
public:
    vector<int> sortedSquares(vector<int>& nums) {
        int iter = 0;
        for(auto& num : nums){
            nums[iter] = pow(num,2);
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
class Solution {
public:
    int removeElement(vector<int>& nums, int val) {
        int i = 0;
        for(int j = 0; j < nums.size(); ++j){
            if(nums[j] != val){
                nums[i] = nums[j];
                ++i;
            }
        }
        return i;
    }
};