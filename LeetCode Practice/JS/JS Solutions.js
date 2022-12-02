/*
* JS, won't often provide a JS solution 
* as its not a priority to learn.
*/

/*
Number of Steps to Reduce a Number to Zero
date 11/18/22
*/

/**
 * @param {number} num
 * @return {number}
 */
var numberOfSteps = function(num) {
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
* alerternative, version, 
* uses the recommended comparison tools, in the while loop.
*/
/**
 * @param {number} num
 * @return {number}
 */
var numberOfSteps = function(num) {
        var stepCount = 0;
        while (num !== 0){
            if ((num % 2 ) === 0){
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
/**
 * @param {number[]} nums
 * @param {number} val
 * @return {number}
 */
 var removeElement = function(nums, val) {
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