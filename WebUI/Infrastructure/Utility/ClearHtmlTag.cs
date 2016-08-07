using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace WebUI.Infrastructure.Utility
{
    public class ClearHtmlTag
    {
        public static string ClearHtmlTag1(string str)
        {
            // Remove new lines since they are not visible in HTML
            str = str.Replace("\n", " ");

            // Remove tab spaces
            str = str.Replace("\t", " ");            

            // Remove HEAD tag
            str = Regex.Replace(str, "<head.*?</head>", "" , RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Remove any JavaScript
            str = Regex.Replace(str, "<script.*?</script>", "" , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // Remove any Style
            str = Regex.Replace(str, "<style[\\W\\w]*?</style>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            str = str.Replace("<br>", " <br>");
            str = str.Replace("<br ", " <br ");
            str = str.Replace("<p ", " <p ");

            str = Regex.Replace(str, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);                  //HTML    
            str = Regex.Replace(str, @"-->", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"<!--.*", "", RegexOptions.IgnoreCase);
            
            // Remove multiple white spaces from HTML
            str = Regex.Replace(str, "\\s+", " ").Trim();            

            return str; 
        }

    }

}