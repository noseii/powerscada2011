using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace System
{
    public static class ArrayHelper
    {
        private static object[] emptyArray = new object[] { };
        public static object[] EmptyArray
        {
            get { return emptyArray; }
        }

        public static void Merge<T>(ref T[] values, params T[] newValues)
        {
            int oldSize;
            int newSize;

            oldSize = values.Length;
            newSize = oldSize + newValues.Length;

            Array.Resize<T>(ref values, newSize);
            newValues.CopyTo(values, oldSize);
        }

        public static object[] GetPropertyValuesOf(Array array, string propertyName)
        {
            object obj;
            Type type;
            PropertyInfo propertyInfo;
            object[] result;

            if (array == null || array.Length == 0) return EmptyArray;

            obj = array.GetValue(0);
            type = obj.GetType();
            propertyInfo = type.GetProperty(propertyName);
            result = new object[array.Length];
            //result = new string[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = propertyInfo.GetValue(array.GetValue(i), null);
            }

            return result;
        }

        public static T[] GetPropertyValuesOf<T>(Array array, string propertyName)
        {
            object obj;
            Type type;
            PropertyInfo propertyInfo;
            T[] result;

            if (array == null || array.Length == 0) return new T[0];

            obj = array.GetValue(0);
            type = obj.GetType();
            propertyInfo = type.GetProperty(propertyName);
            result = new T[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                result[i] = (T)propertyInfo.GetValue(array.GetValue(i), null);
            }

            return result;
        }

        public static bool IsNull(Array array)
        {
            return
                array == null || array.Length == 0;
        }

        public static bool HasValue(Array array)
        {
            return
                array != null && array.Length > 0;
        }

        public static T[] GetArray<T>(T value, int length)
        {
            T[] result = new T[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = value;
            }

            return result;
        }

        public static bool In<T>(T search, params T[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (search.Equals(values[i])) return true;
            }
            return false;
        }
    }
}
