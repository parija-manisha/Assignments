using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDetailsLayers.Util
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

        public static void SetValue(object entity, string input, System.Reflection.PropertyInfo property)
        {
            Type propertyType = property.PropertyType;

            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (string.IsNullOrEmpty(input))
                {
                    property.SetValue(entity, null);
                }
                else
                {
                    Type underlyingType = Nullable.GetUnderlyingType(propertyType);
                    object convertedValue = Convert.ChangeType(input, underlyingType);
                    property.SetValue(entity, convertedValue);
                }
            }
        }
    }
}
