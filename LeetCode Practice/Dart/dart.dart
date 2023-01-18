/*
Move Zeros
01/06/23
*/
class Solution {
  void moveZeroes(List<int> nums) {
        var foot = 0;
        var temp = 0;
        for(var head = 0; head <= nums.length-1; ++head){
            if(nums[head] != 0){
                temp = nums[head];
                nums[head] = nums[foot];
                nums[foot] = temp;
                ++foot;
            }
        }
    }
    bool isPositive(double number){
        return number >= 0;
    }
}