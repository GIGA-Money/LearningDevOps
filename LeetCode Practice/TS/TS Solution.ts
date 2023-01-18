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

/*
12/28/22
Valid Mountain Array
*/
function validMountainArray(arr: number[]): boolean {
    var size = arr.length, i = 0, j = size - 1;
    while(i + 1 < size && arr[i] < arr[i+1]) ++i;
    while(j > 0 && arr[j-1] > arr[j]) --j;
    return i > 0 && i == j && j < size - 1;
};

/*
Replace elements with greatest element on right side
12/29/22
*/
function replaceElements(arr: number[]): number[] {
    if(arr.length <= 1){
        arr[0] = -1;
        return arr;
        }
    var actual, max = -1;
    for(var i =  arr.length - 1; i >= 0; --i){
        actual = arr[i];
        arr[i] = max;
        max = Math.max(actual, max);
    }
    return arr;
};

/*
Move Zeros
01/06/23
*/
/**
 Do not return anything, modify nums in-place instead.
 */
 function moveZeroes(nums: number[]): void {
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
};

var isPositive = function(numbers){
    if((numbers % 2) == 0){
        return true;
    }
    else{
        return false;
    }
};

var isPositive = function(number){
    return number >= 0;
};

/*
1/10/2023
Sort By Parity
*/
function sortArrayByParity(nums: number[]): number[] {
    var fast = 0;
    for(var slow = 0; slow <= nums.length - 1; ++slow){
        if ((nums[slow] % 2) == 0){
            nums[slow] = (nums[slow] + nums[fast]) - (nums[fast] = nums[slow]);
            ++fast;
        }
    }
    return nums;
};