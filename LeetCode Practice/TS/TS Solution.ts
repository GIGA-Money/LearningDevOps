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

/*
 * 12/02/2022
 * remove duplicates
*/
function removeDuplicates(nums: number[]): number {
    if (nums.length == 0)
            return 0;
    if (nums[nums.length - 1]  == nums[0])
        return 1;
    var i = 0;
        for(var j = 1; j < nums.length; ++j){
            if(nums[j] == nums[j-1])
                ++i;
            else
                nums[j-i] = nums[j];
         }
        return nums.length - i;
};

/*
12/06/2022
Check If N and Its Double Exist
*/
function checkIfExist(arr: number[]): boolean {
    var i = 0;
    var j = i;
    for(; i < arr.length; ++i){
        for(; j < arr.length; ++j){
            if (i != j && arr[i] * 2 == arr[j])
                return true;
        }
        j = 0;
    }                    
    return false;
};