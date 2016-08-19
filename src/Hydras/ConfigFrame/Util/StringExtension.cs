using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConfigFrame.Util
{
    public static class StringExtension
    {/// <summary>
        /// String 扩充方法（用正则表达式分割字符串）
        /// </summary>
        /// <param name="target">目标字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="isIncludeMatch">是否包括匹配结果</param>
        /// <returns>数组</returns>
        public static string[] Split(this string target, Regex regex, bool isIncludeMatch = true)
        {
            List<string> list = new List<string>();
            MatchCollection mc = regex.Matches(target);
            int curPostion = 0;
            foreach (Match match in mc)
            {
                if (match.Index != curPostion)
                {
                    list.Add(target.Substring(curPostion, match.Index - curPostion));
                }
                curPostion = match.Index + match.Length;
                if (isIncludeMatch)
                {
                    list.Add(match.Value);
                }
            }
            if (target.Length > curPostion)
            {
                list.Add(target.Substring(curPostion));
            }
            return list.ToArray();
        }
    }
}
