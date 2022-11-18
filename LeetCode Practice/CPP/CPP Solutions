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