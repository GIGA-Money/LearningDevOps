/*
12/1/2022
Remove Element
*/
int removeElement(int* nums, int numsSize, int val){
        int i = 0;
        int j = i;
        for(;j < numsSize; ++j){
            if (nums[j] != val)
            {
                nums[i] = nums[j];
                ++i;
            }
        }
        return i;
}

/*
 * 12/02/2022
 * remove duplicates
*/
int removeDuplicates(int* nums, int numsSize){
    if (numsSize == 0)
        return 0;
    if (nums[numsSize - 1] == nums[0])
        return 1;
    int i = 0;
        for(int j = 1; j < numsSize; ++j){
            if(nums[j] == nums[j-1])
                ++i;
            else
                 nums[j-i] = nums[j];
         }
    return numsSize - i;
}

/*
12/06/2022
Check If N and Its Double Exist
*/
bool checkIfExist(int* arr, int arrSize){
        int i = 0;
        for(; i < arrSize; ++i)
            for(int j = 0; j < arrSize; ++j)
                if (i != j & arr[i] * 2 == arr[j])
                return true;
        return false;
}