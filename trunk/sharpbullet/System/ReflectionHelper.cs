using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading;

namespace System
{
    public static class ReflectionHelper
    {
        public static PropertyInfo[] GetPropertiesExceptMarkedWithAttr(Type type, Type attribute)
        {
            List<PropertyInfo> list;
            PropertyInfo[] properties;

            list = new List<PropertyInfo>();
            properties = type.GetProperties();
            
            for (int i = 0; i < properties.Length; i++)
            {
                if (ArrayHelper.IsNull(properties[i].GetCustomAttributes(attribute, false)))
                {
                    SharpBullet.OAL.Metadata.FieldDefinitionAttribute fielddefinition = ReflectionHelper.GetAttribute<SharpBullet.OAL.Metadata.FieldDefinitionAttribute>(properties[i]);
                    if (fielddefinition != null && fielddefinition.MappingType == SharpBullet.OAL.FieldMappingType.No)
                    {
                        continue;
                    }

                    list.Add(properties[i]);
                }
            }

            return list.ToArray();
        }

        public static T GetAttribute<T>(Type type)
        {
            object[] attrs = type.GetCustomAttributes(typeof(T), true);
            if (attrs != null && attrs.Length == 1)
                return (T)attrs[0];

            return default(T);
        }

        public static T GetAttribute<T>(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(typeof(T), true);
            if (attrs != null && attrs.Length == 1)
                return (T)attrs[0];

            return default(T);
        }

        public static PropertyInfo GetPropertyInfo(Type type, string field)
        {
            if (!field.Contains("."))
                return type.GetProperty(field);
            else
            {
                string[] fieldNames = field.Split('.');
                PropertyInfo pi = null;
                foreach (string fieldName in fieldNames)
                {
                    pi = type.GetProperty(fieldName);
                    type = pi.PropertyType;
                }
                return pi;
            }
        }

        public static PropertyInfo GetFirstProperty(Type type, Type propertyType)
        {
            foreach (PropertyInfo pi in type.GetProperties())
                if (pi.PropertyType == propertyType)
                    return pi;
            return null;
        }

        public static object GetValue(object instance, string field)
        {
            if (!field.Contains("."))
                return instance.GetType().GetProperty(field).GetValue(instance, null);
            else
            {
                string[] fieldNames = field.Split('.');
                PropertyInfo pi = null;
                foreach (string fieldName in fieldNames)
                {
                    pi = instance.GetType().GetProperty(fieldName);
                    instance = pi.GetValue(instance, null);
                }
                return instance;
            }
        }

        public static void SetValue(object instance, string field, object value)
        {
            if (!field.Contains("."))
            {
                PropertyInfo pi = instance.GetType().GetProperty(field);
                
                //Bir valuetype için deðer gelmemiþse set etmeye gerek olmamalý. 
                if ((value == null || (string.IsNullOrEmpty(value.ToString())))
                    && pi.PropertyType.IsValueType)
                {
                    return;
                }

                if(pi.PropertyType == typeof(DateTime) && value!=null && value.GetType()==typeof(string))
                    value = DateTime.Parse((string)value, new System.Globalization.CultureInfo("Tr-tr"));
                    //value = DateTime.ParseExact((string)value, "dd.MM.yyyy hh:mm", new System.Globalization.CultureInfo("Tr-tr"));
                else if (!pi.PropertyType.IsEnum)
                    value = Convert.ChangeType(value, pi.PropertyType, Thread.CurrentThread.CurrentCulture);
                else
                    value = Enum.Parse(pi.PropertyType, value.ToString());

                instance.GetType().GetProperty(field).SetValue(instance, value, null);
            }
            else
            {
                string[] fieldNames = field.Split('.');
                PropertyInfo pi = null;
                string fieldName = null;
                for (int i = 0; i < fieldNames.Length; i++) 
                {
                    fieldName = fieldNames[i];
                    pi = instance.GetType().GetProperty(fieldName);
                    if(i <(fieldNames.Length-1))
                        instance = pi.GetValue(instance, null);
                }

                //Bir valuetype için deðer gelmemiþse set etmeye gerek olmamalý. 
                if (pi.PropertyType.IsValueType &&
                    (value == null || string.IsNullOrEmpty((string)value))) return;

                if (pi.PropertyType == typeof(DateTime) && value != null && value.GetType() == typeof(string))
                    value = DateTime.Parse((string)value, new System.Globalization.CultureInfo("Tr-tr"));
                else if (!pi.PropertyType.IsEnum)
                    value = Convert.ChangeType(value, pi.PropertyType);
                else
                    value = Enum.Parse(pi.PropertyType, value.ToString());

                pi.SetValue(instance, value, null);
            }
        }
    }
}
