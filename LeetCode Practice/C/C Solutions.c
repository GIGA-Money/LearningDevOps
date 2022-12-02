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