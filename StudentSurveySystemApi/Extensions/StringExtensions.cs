using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Server.Extensions
{
    public static class StringExtensions
    {
        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }
        public static string RemoveWhiteSpaces(this string str)
        {
            return Regex.Replace(str, @"\s+", "");
        }
        public static string RemoveDiactrics(this string word)
        {
            if (word == null || "".Equals(word))
            {
                return word;
            }
            var charArray = word.ToCharArray();
            var normalizedArray = new char[charArray.Length];
            for (var i = 0; i < normalizedArray.Length; i++)
            {
                normalizedArray[i] = NormalizeChar(charArray[i]);
            }
            return new string(normalizedArray);
        }

        private static char NormalizeChar(char c)
        {
            switch (c)
            {
                case 'ą':
                    return 'a';
                case 'ć':
                    return 'c';
                case 'ę':
                    return 'e';
                case 'ł':
                    return 'l';
                case 'ń':
                    return 'n';
                case 'ó':
                    return 'o';
                case 'ś':
                    return 's';
                case 'ż':
                case 'ź':
                    return 'z';
            }
            return c;
        }

        public static double? ToNullableDouble(this string s)
        {
            if (double.TryParse(s, out var i)) return i;
            return null;
        }
    }
}