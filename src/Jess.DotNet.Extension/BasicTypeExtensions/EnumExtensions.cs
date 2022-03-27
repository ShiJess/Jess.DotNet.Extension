using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jess.DotNet.Extension
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举Display特性Name信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum en)
        {
            Type type = en.GetType();
            FieldInfo fd = type.GetField(en.ToString());
            if (fd == null)
                return en?.ToString();

            var attrs = fd.GetCustomAttributes(typeof(DisplayAttribute), false);
            string name = string.Empty;
            foreach (DisplayAttribute attribute in attrs)
            {
                name = attribute.Name;
                break;
            }
            if (string.IsNullOrWhiteSpace(name))
                name = fd.Name;
            return name;
        }

        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();
            FieldInfo fd = type.GetField(en.ToString());
            if (fd == null)
                return en?.ToString();

            var attrs = fd.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string name = string.Empty;
            foreach (DescriptionAttribute attribute in attrs)
            {
                name = attribute.Description;
                break;
            }
            if (string.IsNullOrWhiteSpace(name))
                name = fd.Name;
            return name;
        }

        public static string GetDescription(Type enumType, object value)
        {
            foreach (MemberInfo memberInfo in enumType.GetMembers())
            {
                object enumvalue = value;
                if (value == null)
                    value = 0;
                else if (value.GetType() == typeof(string))
                {
                    var strvalue = value.ToString();
                    if (strvalue.Length == 1)
                        enumvalue = strvalue.First();
                    else
                    {
                        int temp = 0;
                        if (Int32.TryParse(strvalue, out temp))
                            enumvalue = temp;
                    }
                }
                else
                    enumvalue = value;


                if(memberInfo.Name==enumType.GetEnumName(enumvalue))
                {
                    foreach (Attribute attribute in Attribute.GetCustomAttributes(memberInfo))
                    {
                        if(attribute.GetType()==typeof(DescriptionAttribute))
                        {
                            return ((DescriptionAttribute)attribute).Description;
                        }
                    }
                }


            }

            return string.Empty;


        }


    }
}
