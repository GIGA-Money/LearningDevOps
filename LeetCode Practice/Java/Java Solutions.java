/* Java solutions
Problems from LeetCode
*/

/*
Running Sum of 1d Array
date: 11/16/22
*/
class Solution {
    public int[] runningSum(int[] nums) {
        int simpleReturn[] = nums;
        if (nums.length == 1)
            return nums;

        for (int i = 0; i < nums.length; ++i) {
            if (i == 0) {
                simpleReturn[i] = nums[i];
            } else {
                simpleReturn[i] = nums[i - 1] + simpleReturn[i];
            }
        }
        return simpleReturn;
    }
}

/*
 * Richest Customer Wealth
 * date: 11/17/22
 */
class Solution {
    public int maximumWealth(int[][] accounts) {
        int[] returnWealth = new int[accounts.length + 1];
        int insertSum = 0;
        int inter = 0;
        for (int[] i : accounts) {
            for (int j : i) {
                insertSum += j;
            }
            returnWealth[inter] = insertSum;
            insertSum = 0;
            inter++;
        }
        Arrays.sort(returnWealth);
        return returnWealth[returnWealth.length - 1];
    }
}

/*
 * Number of Steps to Reduce a Number to Zero
 * date 11/18/22
 */
class Solution {
    public int numberOfSteps(int num) {
        int stepCount = 0;
        while (num != 0) {
            if ((num % 2) == 0) {
                num /= 2;
                ++stepCount;
            } else {
                --num;
                ++stepCount;
            }
        }
        return stepCount;
    }
}

/*
 * FizzBuzz
 * date: 11/18/22
 */
class Solution {
    public List<String> fizzBuzz(int n) {
        List<String> retStr = new ArrayList<String>();
        if (n == 1) {
            retStr.add(Integer.toString(n));
            return retStr;
        }
        int iter = 1;
        while (iter <= n) {
            if ((iter % 3) == 0 && (iter % 5) == 0)
                retStr.add("FizzBuzz");
            else if ((iter % 5) == 0)
                retStr.add("Buzz");
            else if ((iter % 3) == 0)
                retStr.add("Fizz");
            else
                retStr.add(Integer.toString(iter));
            ++iter;
        }
        return retStr;
    }
}

/*
 * Middle of the Linked List
 * 11/21/22
 */
class Solution {
    public ListNode middleNode(ListNode head) {
        ListNode cursor = head;
        ArrayList<ListNode> cursorStore = new ArrayList<ListNode>();
        int iter = 0;
        for (cursor = head; cursor != null; cursor = cursor.next) {
            cursorStore.add(cursor);
            ++iter;
        }
        iter /= 2;
        return cursor = cursorStore.get(iter);
    }
}

/*
 * Max Consecutive Ones
 * 11/21/22
 */
class Solution {
    public int findMaxConsecutiveOnes(int[] nums) {
        List<Integer> retStr = new ArrayList<Integer>();
        int counter = 0;
        for (int j : nums) {
            if (j == 0)
                counter = 0;
            else if (j == 1) {
                retStr.add(counter);
                ++counter;
            }
            retStr.add(counter);
        }
        return Collections.max(retStr);
    }
}

/*
 * Squares of a Sorted Arrayh
 * 11/23/22
 */
class Solution {
    public int[] sortedSquares(int[] nums) {
        int iter = 0;
        for (int num : nums) {
            nums[iter] = (int) Math.pow(num, 2);
            ++iter;
        }
        Arrays.sort(nums);
        return nums;
    }
}

/*
 * 12/1/2022
 * Remove Element
 */
class Solution {
    public int removeElement(int[] nums, int val) {
        int i = 0;
        int j = i;
        for (; j < nums.length; ++j) {
            if (nums[j] != val) {
                nums[i] = nums[j];
                ++i;
            }
        }
        return i;
    }
}

/*
 * 12/02/2022
 * remove duplicates
 */
class Solution {
    public int removeDuplicates(int[] nums) {
        if (nums.length == 0)
            return 0;
        int i = 0;
        if (nums[nums.length - 1] == nums[i])
            return ++i;
        for (int j = 1; j < nums.length; ++j) {
            if (nums[j] == nums[j - 1])
                ++i;
            else
                nums[j - i] = nums[j];
        }
        return nums.length - i;
    }
}

/*
 * 12/06/2022
 * Check If N and Its Double Exist
 */
class Solution {
    public boolean checkIfExist(int[] arr) {
        int i = 0;
        int j = i;
        for (; i < arr.length; ++i) {
            for (; j < arr.length; ++j) {
                if (i != j & arr[i] * 2 == arr[j])
                    return true;
            }
            j = 0;
        }
        return false;
    }
}

/*
 * 12/28/22
 * Valid Mountain Array
 */
class Solution {
    public boolean validMountainArray(int[] arr) {
        int size = arr.length, i = 0, j = size - 1;
        while (i + 1 < size && arr[i] < arr[i + 1])
            ++i;
        while (j > 0 && arr[j - 1] > arr[j])
            --j;
        return i > 0 && i == j && j < size - 1;
    }
}

/*
 * Replace elements with greatest element on right side
 * 12/29/22
 */
class Solution {
    public int[] replaceElements(int[] arr) {
        if (arr.length <= 1) {
            Arrays.fill(arr, -1);
            return arr;
        }
        int actual, max = -1;
        for (int i = arr.length - 1; i >= 0; --i) {
            actual = arr[i];
            arr[i] = max;
            max = Math.max(actual, max);
        }
        return arr;
    }
}

/*
 * Move Zeros
 * 01/06/23
 */
class Solution {
    public void moveZeroes(int[] nums) {
        int foot = 0;
        int temp = 0;
        for (int head = 0; head <= nums.length - 1; ++head) {
            if (nums[head] != 0) {
                temp = nums[head];
                nums[head] = nums[foot];
                nums[foot] = temp;
                ++foot;
            }
        }
    }

    public boolean isEvenOdd(double numbers) {
        if ((numbers % 2) == 0) {
            return true;
        } else {
            return false;
        }
    }

    public boolean isPositive(double number) {
        return number >= 0;
    }
}

/*
 * 1/10/2023
 * Sort By Parity
 */
class Solution {
    protected boolean isPositive(int number) {
        return (number % 2) == 0;/**
                                  * XOR swap, but doesn't work on conditions such as [0,2], might be a result of
                                  * the isPositive helper'
                                  * nums[slow] ^= nums[fast]; nums[fast] ^= nums[slow]; nums[slow] ^= nums[fast];
                                  */
    }

    public int[] sortArrayByParity(int[] nums) {
        int fast = 0;
        for (int slow = 0; slow <= nums.length - 1; ++slow) {
            if (isPositive(nums[slow])) {
                nums[slow] = (nums[slow] + nums[fast]) - (nums[fast] = nums[slow]);
                ++fast;
            }
        }
        return nums;
    }
}

/*
 * 1/13/23
 * Height Checker
 */
class Solution {
    public int heightChecker(int[] heights) {
        int[] expected;
        expected = heights.clone();
        Arrays.sort(expected);
        int missmatch = 0;
        for (var i = 0; i <= heights.length - 1; ++i) {
            if (heights[i] != expected[i]) {
                ++missmatch;
            }
        }
        return (missmatch);
    }}

    /*
     * Dr. Adams' class
     * 
     */
    for(

    long l = 0;l<longArray.length-1;++l)
    {
        System.out.println(longArray[l]);
    }

/**
 * 6-13-23 Converted the Py solution for isAnagram
 */
class Solution {
    public boolean isAnagram(String s, String t) {
        if (s.length() != t.length()){
            return false;
        }
        char sArr[] = s.toCharArray();
        char tArr[] = t.toCharArray();
        Arrays.sort(sArr);
        Arrays.sort(tArr);
        for(int i = 0; i < s.length(); i++){
            if(sArr[i] != tArr[i]){
                return false;
            }
        }
        return true;
    }
}

/*
 * 6-13-23 - Two sum, failed brute force.
 */
// // dont forget to declare variables, Py is great, but java will have to do.
// int j = nums.length-1;
// for(int i = 0; i < nums.length; i++){
// //forget how to compare things with ==?
// if((nums[i] + nums[j]) == target){
// return new int[]{i, j};
// }
// else if((nums[i] + nums[j]) > target){
// j -= 1;
// }
// }
// //dont forget array dimensions when making new.
// //well this brute force approach won't work it can't handle all of the
// conditions.
// return new int[]{0,1};

/**
 * 6-13-23 two sum, hashmap.
 */
class Solution {
    public int[] twoSum(int[] nums, int target) {
        Map<Integer, Integer> seen = new HashMap<>();
        for(int i = 0; i < nums.length; i++){
            int complement = target - nums[i];
            if(seen.containsKey(complement)){
                return new int[]{seen.get(complement), i};
            }
            seen.put(nums[i],i);
        }
        return new int[]{};
    }
}

/**
 * 6-16-23
 */
class Solution {
    public List<List<String>> groupAnagrams(String[] strs) {
        if(strs.length <= 0){
            List<List<String>> emptyReturn = new ArrayList<>();
            emptyReturn.add(new ArrayList<>());
            return emptyReturn;
        }
        Map<String, List<String>> outList = new HashMap<>();
        //Using a ranged for loop.
        for(String str : strs){
            char[] sortStr = str.toCharArray();
            Arrays.sort(sortStr);
            String sorted = new String(sortStr);
            if(!outList.containsKey(sorted)){
                outList.put(sorted, new ArrayList<>());
            }
            outList.get(sorted).add(str);
        }
        return new ArrayList<>(outList.values());
    }
}

/**
 * 6-20-23
 * 347. Top K Frequent Elements.
 * Using the bucket sort method.
 */
class Solution {
    // Performing with the Bucket Sort solution.
    public int[] topKFrequent(int[] nums, int k) {
        /*
        Count the frequency of each element. 
        A hashmap counts is created to count the frequency of each element in nums.
        for each number in nums, if the number is already a key, in counts, its count ins incremented.
        if not a key in counts, its added with a count of 1.
        */ 
        Map<Integer, Integer> counts = new HashMap<>();
        for(int num : nums)
            counts.put(num, counts.getOrDefault(num, 0) + 1);

        /*
        Create a list of buckets.
        An array of list buckets, is created where the index of each bucket corresponds to a frequency.
        Each bucket will hold the elements with that frequency. 
        For each number in counts, the number is added to the bucket that corresponds to its frequency.
        */ 
        List<Integer>[] buckets = new List[nums.length + 1];
        for(int num : counts.keySet()){
            int frequency = counts.get(num);
            if(buckets[frequency] == null)
                buckets[frequency] = new ArrayList<>();
            buckets[frequency].add(num);
        }

        // collect the elemetns from the buckets
        List<Integer> topK = new ArrayList<>();
        for(int i = buckets.length - 1; i >= 0 && topK.size() < k; i--){
            if(buckets[i] != null)
                topK.addAll(buckets[i]);
        }

        /*
        Convert the list to an array.
        The elements in the 'topk' list are copied into the new array, results, which is then returned.
        */ 
        int[] result = new int[k];
        for(int i = 0; i < k; i++)
            result[i] = topK.get(i);

        return result;
    }
}