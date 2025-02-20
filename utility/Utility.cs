using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ytSound.utility
{
    internal class Utility
    {
        public static bool IsValidUrl(string url)
        {
            string pattern = @"^(http|https|ftp):\/\/([A-Za-z0-9\-]+\.)+[A-Za-z]{2,}(:[0-9]+)?(\/.*)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(url);
        }

    }
}
