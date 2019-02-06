using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace DotNetCommonLibrary.Text
{
    public static class Converter
    {
        static readonly Regex SpacesRegex = new Regex(@"\s+"); // any spaces
        static readonly Regex TimeRegex = new Regex(@"(-?)(\d+)h:(\d+)m", RegexOptions.IgnoreCase);

        /// <summary>
        /// Replace wildcard to regex
        ///   ex.) ? => ^.$ / * => ^.*$
        /// </summary>
        /// <param name="wildCardText"></param>
        /// <returns></returns>
        public static Regex ToWildCardRegex(string wildCardText)
        {
            var regexText = Regex.Replace(wildCardText, ".", m =>
            {
                // m is should be found
                var s = m.Value;
                switch (s)
                {
                    case "?":
                        return "."; // ? => .
                    case "*":
                        return ".*"; // * => .*
                    default:
                        return Regex.Escape(s);
                }
            });
            return new Regex("^" + regexText + "$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Zenkaku to Hankaku
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ZenkakuToHankaku(string text)
        {
            return Strings.StrConv(text, VbStrConv.Narrow, 0x411);
        }

        /// <summary>
        /// remove all spaces
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string TrimSpaces(string text)
        {
            return SpacesRegex.Replace(text, "");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lastIndex"></param>
        /// <param name="trimText"></param>
        /// <returns></returns>
        public static string TrimLastString(string text, int lastIndex, string trimText)
        {
            try
            {
                int resultIndex = text.Length;
                for (int i = 0; i <= lastIndex; ++i)
                {
                    var index = text.LastIndexOf(trimText, resultIndex - 1);
                    if (index == -1)
                    {
                        return text; // return original text if doesn't match
                    }
                    resultIndex = index;
                }
                return text.Substring(0, resultIndex);
            }
            catch
            {
                return text; // return original text if doesn't match
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static System.TimeSpan ToTimeSpan(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var m = TimeRegex.Match(text);
                if (m.Success && 2 < m.Groups.Count)
                {
                    // positive or negative
                    var sign = m.Groups[1].Value;
                    var hourText = m.Groups[2].Value;
                    var minText = m.Groups[3].Value;

                    if (int.TryParse(hourText, out int hour) && int.TryParse(minText, out int min))
                    {
                        var coefficient = (sign == "") ? 1 : -1;
                        return new System.TimeSpan(hour * coefficient, min * coefficient, 0);
                    }
                }
            }
            return new System.TimeSpan();
        }
    }
}
