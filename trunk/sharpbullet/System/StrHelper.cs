using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace System
{
    public static class StrHelper
    {
        private static string[] emptyArray = new string[] { };
        public static string[] EmptyArray
        {
            get { return emptyArray; }
        }

        /// <summary>
        /// Concats every item in values array with prefix and suffix.
        /// And returns the results in a new array.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="values"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string[] Concat(string prefix, string[] values, string postfix)
        {
            string[] result = new string[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                result[i] = prefix + values[i] + postfix;
            }

            return result;
        }

        public static string[] GetNumbers(int from, int count)
        {
            string[] result = new string[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = (from + i).ToString();
            }

            return result;
        }

        public static string[] GetPropertyValuesOf(Array array, string propertyName)
        {
            object obj;
            Type type;
            PropertyInfo propertyInfo;
            string[] result;

            if (array == null || array.Length == 0) return EmptyArray;

            obj = array.GetValue(0);
            type = obj.GetType();
            propertyInfo = type.GetProperty(propertyName);
            result = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = propertyInfo.GetValue(array.GetValue(i), null).ToString();
            }

            return result;
        }

        public static string[] Remove(string removeThis, string[] values)
        {
            ArrayList result = new ArrayList();

            foreach (string s in values)
            {
                if (removeThis != s) result.Add(s);
            }

            return (string[])result.ToArray(typeof(string));
        }

        public static string TruncateFromRight(string s, string delimiter, int count)
        {
            int index;
            string result;

            result = s;
            for (int i = 0; i < count; i++)
            {
                index = result.LastIndexOf(delimiter);
                result = result.Substring(0, index);
            }

            return result;
        }

        /// <summary>
        /// Makes a new string array containing the 'value', 'count' times.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string[] GetArrayOf(string value, int count)
        {
            string[] result;

            result = new string[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = value;
            }

            return result;
        }

        public static string GetLastWordAfter(string delimeter, string content)
        {
            int i;
            string result;

            if (string.IsNullOrEmpty(content)) return "";

            i = content.LastIndexOf(delimeter);
            if (i < 0) return "";

            result = content.Substring(i + 1);

            return result;
        }
    }
}
