using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SharpBullet.OAL
{
    public static class Configuration
    {
        private static Hashtable configValues = new Hashtable();

        public static void SetValue(string key, object value)
        {
            configValues[key] = 
                value != null
                ? value.ToString() : null;
        }

        public static object GetValue(string key)
        {
            return configValues[key];
        }

        public static string GetString(string key)
        {
            return configValues[key] as string;
        }

        public static bool GetBool(string key)
        {
            string value;
            bool result;

            value = configValues[key] as string;
            result = bool.Parse(value);

            return result;
        }
    }
}
