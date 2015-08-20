using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Util
{
    class LetterHandller
    {
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        //public static string UppercaseFirstSentence(string s)
        //{
        //    string us = "", uw, nus=" ";
        //    int index;

        //    while (nus.IndexOf(' ') != null)
        //    {
        //        index = nus.IndexOf(' ');
        //        uw = nus.Substring(0, index);
        //        nus = s.Substring(index + 1, s.Length);
        //        us = us + " " + UppercaseFirst(uw);
        //    }
        //    return us.Trim();
        //}
    }
}
