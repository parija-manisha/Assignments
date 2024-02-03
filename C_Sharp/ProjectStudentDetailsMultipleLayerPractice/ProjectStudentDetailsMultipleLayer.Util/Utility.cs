using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStudentDetailsMultipleLayer.Util
{
    public class Utility
    {

        public static bool TryParseInt(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        public static bool TryParseEnum<TEnum>(string input, out TEnum result) where TEnum : struct
        {
            return System.Enum.TryParse(input, true, out result);
        }

        public static bool TryParseDate(string input, out DateTime result)
        {
            return DateTime.TryParse(input, out result);
        }

        public static bool IsNullOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static void SetValue(object student, string input, System.Reflection.PropertyInfo property)
        {
            var convertedValue = Convert.ChangeType(input, property.PropertyType);

            property.SetValue(student, convertedValue);
        }
    }
}
