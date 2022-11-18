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