using App3.Models;
using App3.Resources.DataHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace App3.Resources
{
    public static class FileFactory
    {

        public static string CheckTypeOfFile(string path)
        {
            var file = new FileInfo(path);
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                if (file.Extension.Equals(".xls"))
                {
                    return "xls";
                }
                    
                else if (file.Extension.Equals(".xlsx"))
                    return "xlsx";
                else if (file.Extension.Equals(".csv"))
                    return "csv";
                else
                    return "Nie obsługuje takiego formatu :/";
            }
        }
        public static string Strip(string text)
        {
            //usuwanie komentarzy 
            text = Regex.Replace(text, @"([<>\?\*\\\""/\|])+", string.Empty);
            //usuwanie skryptów oraz arkuszy styli
            text = Regex.Replace(text, @"(<script[^<]*</script>)|(<style[^<]*</style>)|(&[^;]*;)", string.Empty);
            text = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            return text;
        }
    }
}