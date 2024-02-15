﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentDetailsMultipleLayers.Util
{
    public class Utility
    {
        public static bool TryParseInt(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        public static bool OnlyCharacters(string input)
        {
            Regex regex = new Regex("^[A-Za-z]+$");

            return regex.IsMatch(input);
        }

        public static bool TryParseEnum<TEnum>(string input, out TEnum result) where TEnum : struct
        {
            return System.Enum.TryParse(input, true, out result);
        }

        public static bool TryParseDate(string input, out DateTime result)
        {
            string[] formats = { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yyyy", "M/d/yyyy", "yyyy-MM-ddTHH:mm:ss" };

            return DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}