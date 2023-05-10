//https://leetcode.com/problems/longest-substring-without-repeating-characters/description/

var sol = new Solution();
Console.WriteLine(sol.LengthOfLongestSubstring("dasdassdadsa dsafasa 1232312 1"));

public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {

        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        HashSet<char> hSet = new HashSet<char>();
        int max = 0;
        int i = 0;
        int j = 0;

        while (i < s.Length)
        {
            if (!hSet.Contains(s[i]))
            {
                hSet.Add(s[i]);
                i++;

            }
            else
            {
                max = Math.Max(max, hSet.Count);
                hSet.Remove(s[j]);
                j++;
            }
        }
        max = Math.Max(max, hSet.Count);
        return max;

    }
}