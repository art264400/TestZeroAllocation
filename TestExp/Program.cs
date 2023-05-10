using System;
using System.Collections.Generic;
using System.Linq;

namespace TestExp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Guid? guid = new Guid();
            var tr = false;
            if(!guid.HasValue)
                tr = true;
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            var dictonary = new Dictionary<int, int>(nums.Length);
            for (int i = 0; i < nums.Length; i++)
            {
                dictonary.Add(i, nums[i]);
            }

            foreach(var dict1  in dictonary)
                foreach(var dict2 in dictonary)
                    if (dict1.Key != dict2.Key && dict1.Value + dict2.Value == target)
                        return new int[2] { dict1.Key, dict2.Key };
            return default;
        }
    }
}
