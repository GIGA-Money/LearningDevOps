/*
* TS, won't often provide a TS solution as its 
* not a priority to learn.
*/

/*
Number of Steps to Reduce a Number to Zero
date 11/18/22
*/

function numberOfSteps(num: number): number {
        var stepCount = 0;
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
};

/*
12/1/2022
Remove Element
*/
function removeElement(nums: number[], val: number): number {
    var i = 0;
    var j = i;
    for(;j < nums.length; ++j){
        if (nums[j] != val)
        {
            nums[i] = nums[j];
            ++i;
        }
    }
    return i;
};