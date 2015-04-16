using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSDC.App_Code
{
    public static class ExtensionHelper
    {
        public static string TrimString(this string inputString, int length)
        {
            try
            {
                if (inputString.Length > length)
                    return inputString.Substring(0, length - 5) + "...";
                else
                    return inputString;
            }
            catch
            {
                return inputString;
            }
        }
        public static string ToDateFormat(this string inputDateTime)
        {
            return String.Format("{0:MM/dd/yyyy hh:mm tt}", inputDateTime);
        }
        public static string ToTitleCase(this string str)
        {
            if (String.IsNullOrEmpty(str))
                return str;
            if (str.Length == 1)
                return str.ToUpper();
            return str.Remove(1).ToUpper() + str.Substring(1).ToLower();
        }
        public static string ToTitleCase(this string str, TitleCase tcase)
        {
            str = str.ToLower();
            switch (tcase)
            {
                
                case TitleCase.Words:
                    return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
                default:
                    break;
            }
            return str;
        }


        public enum TitleCase
        {
            First,
            All,
            Words
        }
    }
}