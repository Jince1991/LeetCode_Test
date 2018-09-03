using System;
using System.Collections.Generic;

namespace LeetCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolution solution = new FindKthLargest();
            solution.Test();
        }
    }

    public interface ISolution
    {
        void Test();
    }

    #region 从排序数组中删除重复项
    /// <summary>
    /// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-array/description/
    /// 2018/8/27
    /// </summary>
    public class RemoveDuplicates : ISolution
    {
        public void Test()
        {
            int[] nums = new int[5] { 0, 1, 1, 2, 3 };
            int count = Solution(nums);
            for (int i = 0; i < count; ++i)
            {
                Console.WriteLine(nums[i]);
            }
            Console.ReadLine();
        }

        public int Solution(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            int count = 0;
            for (int i = 1; i < nums.Length; ++i)
            {
                if (nums[count] != nums[i])
                    nums[++count] = nums[i];
            }
            return count + 1;
        }
    }
    #endregion

    #region 从数组中查找两个元素的目标和
    /// <summary>
    /// https://leetcode-cn.com/problems/two-sum/description/
    /// 2018.8.27
    /// </summary>
    public class TwoSum : ISolution
    {
        /// <summary>
        /// 暴力解决，两两相加，时间复杂度O(n^2)，空间复杂度O(1)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int[] Solution1(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new Exception("array error");
            for (int i = 0; i < nums.Length; ++i)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    if (nums[i] + nums[j] == target)
                        return new int[2] { i, j };
                }
            }
            throw new Exception("No solution");
        }

        /// <summary>
        /// 以空间换时间，时间复杂度O(n)，空间复杂度O(n)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int[] Solution2(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new Exception("Array error");
            Dictionary<int, int> numDic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; ++i)
            {
                numDic.Add(nums[i], i);
            }
            for (int i = 0; i < nums.Length; ++i)
            {
                int complement = target - nums[i];
                if (numDic.ContainsKey(complement) && numDic[complement] != i)
                    return new int[2] { i, numDic[complement] };
            }
            throw new Exception("No solution");
        }

        /// <summary>
        /// 时间复杂度O(n)，空间复杂度O(n)
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int[] Solution3(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
                throw new Exception("Array error");
            Dictionary<int, int> numDic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; ++i)
            {
                int complement = target - nums[i];
                if (numDic.ContainsKey(complement))
                    return new int[2] { numDic[complement], i };
                numDic.Add(nums[i], i);
            }
            throw new Exception("No solution");
        }

        public void Test()
        {
            int[] nums = new int[5] { -2, 0, 1, -3, 5 };
            int target = 6;
            int[] result = Solution1(nums, target);
            Console.Write("1:");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            result = Solution2(nums, target);
            Console.WriteLine("");
            Console.Write("2:");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            result = Solution3(nums, target);
            Console.WriteLine("");
            Console.Write("3:");
            foreach (var item in result)
            {
                Console.Write(item + " ");
            }
            Console.ReadLine();
        }
    }
    #endregion

    #region 两数相加
    /// <summary>
    /// https://leetcode-cn.com/problems/add-two-numbers/description/
    /// 2018.2.28
    /// </summary>
    public class AddTwoNumber : ISolution
    {
        private class ListNode
        {
            public ListNode(int _val)
            {
                val = _val;
            }
            public int val;
            public ListNode next;
        }

        private ListNode Solution(ListNode l1, ListNode l2)
        {
            ListNode head = new ListNode((l1.val + l2.val) % 10);
            ListNode current = head;
            ListNode node1 = l1;
            ListNode node2 = l2;
            int carry = (l1.val + l2.val) / 10;
            while (node1.next != null || node2.next != null || carry > 0)
            {
                int l1val = node1.next == null ? 0 : node1.next.val;
                int l2val = node2.next == null ? 0 : node2.next.val;
                current.next = new ListNode((l1val + l2val + carry) % 10);
                carry = (l1val + l2val + carry) / 10;
                current = current.next;
                node1 = node1.next ?? node1;
                node2 = node2.next ?? node2;
            }
            current.next = null;
            return head;
        }

        public void Test()
        {
            //             ListNode l1 = new ListNode(2)
            //             {
            //                 next = new ListNode(4)
            //                 {
            //                     next = new ListNode(3)
            //                 }
            //             };
            ListNode l1 = new ListNode(1)
            {
                next = new ListNode(8)
            };

            ListNode l2 = new ListNode(0);

            var result = Solution(l1, l2);
            var node = result;
            Console.WriteLine(node.val);
            while (node.next != null)
            {
                node = node.next;
                Console.WriteLine(node.val);
            }
            Console.ReadLine();
        }
    }
    #endregion

    #region 无重复字符的最长子串
    /// <summary>
    /// https://leetcode-cn.com/problems/longest-substring-without-repeating-characters/description/
    /// 2018.8.28
    /// </summary>
    public class LengthOfLongestSubstring : ISolution
    {
        private int Solution(string s)
        {
            int[] chars = new int[128];
            int ans = 0;
            for (int i = 0, j = 0; i < s.Length; ++i)
            {
                j = Math.Max(chars[s[i]], j);
                ans = Math.Max(ans, i - j + 1);
                chars[s[i]] = i + 1;
            }
            return ans;
        }

        public void Test()
        {
            Console.WriteLine(Solution("abcdef"));
            Console.ReadLine();
        }
    }
    #endregion

    #region 反转整数
    /// <summary>
    /// https://leetcode-cn.com/problems/reverse-integer/description/
    /// 2018.8.30
    /// </summary>
    public class RevertInteger : ISolution
    {
        private int Solution(int x)
        {
            int MAX = int.MaxValue / 10;
            int MIN = int.MinValue / 10;
            int rev = 0;
            while (x != 0)
            {
                int pop = x % 10;
                x /= 10;
                if (rev > MAX || (rev == MAX && pop > 7))
                    return 0;
                if (rev < MIN || (rev == MIN && pop < -8))
                    return 0;
                rev = rev * 10 + pop;
            }
            return rev;
        }

        public void Test()
        {
            Console.WriteLine(Solution(-2113847412));
            Console.ReadLine();
        }
    }
    #endregion

    #region 回文数
    /// <summary>
    /// https://leetcode-cn.com/problems/palindrome-number/description/
    /// 2018.8.30
    /// </summary>
    public class IsPalindrome : ISolution
    {
        private bool Solution(int x)
        {
            if (x < 0)
                return false;
            if (x == 0)
                return true;
            if (x % 10 == 0)
                return false;

            int rev = 0;
            while (x > rev)
            {
                rev = rev * 10 + x % 10;
                x /= 10;
            }
            return x == rev || x == rev / 10;
        }

        public void Test()
        {
            Console.WriteLine(Solution(12321));
            Console.ReadLine();
        }
    }
    #endregion

    #region 最长公共前缀
    /// <summary>
    /// https://leetcode-cn.com/problems/longest-common-prefix/description/
    /// 2018.8.31
    /// </summary>
    public class LongestCommonPrefix : ISolution
    {
        private string Solution(string[] strs)
        {
            if (strs == null || strs.Length == 0)
                return "";
            if (strs[0] == null || strs[0].Length == 0)
                return "";
            if (strs.Length == 1)
                return strs[0];
            string pre = "";
            char current = strs[0][0];
            int j = 0;
            while (j < strs[0].Length)
            {
                current = strs[0][j];
                for (int i = 1; i < strs.Length; ++i)
                {
                    if (strs[i] == null || j >= strs[i].Length)
                        return pre;
                    if (strs[i][j] != current)
                        return pre;
                    if (i == strs.Length - 1)
                        pre += current.ToString();
                }
                ++j;
            }
            return pre;
        }

        public void Test()
        {
            //string[] strs = new string[3] { "flower","flow","flight" };
            //string[] strs = new string[1] { "a" };
            string[] strs = new string[2] { "aca", "cba" };
            Console.WriteLine(Solution(strs));
            Console.ReadLine();
        }
    }
    #endregion

    #region 数组中的第K个最大元素
    /// <summary>
    /// https://leetcode-cn.com/problems/kth-largest-element-in-an-array/description/
    /// 2018.9.3
    /// </summary>
    public class FindKthLargest : ISolution
    {
        //冒泡
        private int Solution1(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                throw new Exception("Array error");
            for (int i = 0; i < k; ++i)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    if (nums[i] < nums[j])
                    {
                        int temp = nums[i];
                        nums[i] = nums[j];
                        nums[j] = temp;
                    }
                }
            }
            return nums[k - 1];
        }
        
        //快排
        private int Solution2(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
                throw new Exception("Array error");

            return QucikSort(nums, 0, nums.Length - 1, k - 1);
        }

        private int Separate(int[] nums, int min, int max)
        {
            int temp = nums[min];
            while (min < max)
            {
                while (min < max && nums[max] <= temp)
                    --max;
                nums[min] = nums[max];
                while (min < max && nums[min] >= temp)
                    ++min;
                nums[max] = nums[min];
            }
            nums[min] = temp;
            return min;
        }

        private int QucikSort(int[] nums, int left, int right, int k)
        {
            int middle = Separate(nums, left, right);
            if (middle > k)
                return QucikSort(nums, left, middle, k);
            else if (middle < k)
                return QucikSort(nums, middle + 1, right, k);
            else
                return nums[k];
        }

        public void Test()
        {
            int[] nums = new int[6] { 3, 2, 1, 5, 6, 4 };
            Console.WriteLine(Solution2(nums, 2));
            Console.ReadLine();
        }
    }
    #endregion
}