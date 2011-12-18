using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mymodel;
using System.Data;
using mycommon;
using System.Reflection;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Metadata
    {
        public static List<T> Select<T>(mymodel.Model inentity, string wherestr, bool withdesc) where T : new()
        {
            string selectstr = CreateSelectStrFromObject(inentity, "", withdesc, false) + "\n" + wherestr;
            DataTable dt =mycommon.myUtil.OpenSqlIntoDataTable(selectstr);
            List<T> list = ConvertDataTableToObjectList<T>(inentity, dt, selectstr);
            return list;
        }

        public static string CreateSelectStrFromObject(mymodel.Model inentity, string entitylable, bool withdesc, bool Isjoined)
        {
            string selectstr = "";
            string entityname = inentity.GetType().Name;
            string fromstr = "";
            int say = 0;
            PropertyInfo[] props = inentity.GetType().GetProperties();
            foreach (PropertyInfo pi in props)
            {
                say++;
                string propName = pi.Name;
                if (pi.PropertyType.BaseType.Name.Contains("Model"))
                {
                    if (withdesc)
                    {
                        mymodel.Model joinedentity = (mymodel.Model)Activator.CreateInstance(pi.PropertyType);
                        string joinedentityname = joinedentity.GetType().Name;
                        propName = CreateSelectStrFromObject(joinedentity, pi.Name, false, true) + " ";

                        //if (pi.GetMetaSaha().Zorunlu)
                        //    fromstr = fromstr + "\nInner Join ";
                        //else
                        fromstr = fromstr + "\nLeft Join ";

                        fromstr = fromstr + joinedentityname + "(nolock) As " + pi.Name + " On " + pi.Name + ".Id=" + entityname + "." + pi.Name + "_Id";

                        if (entitylable.Length == 0)
                            selectstr = selectstr + "\n" + propName + ",";
                        else
                            selectstr = selectstr + "\n" + propName + ",";
                    }
                    else
                    {
                        propName = pi.Name + "_Id";
                        if (entitylable.Length == 0)
                            selectstr = selectstr + entityname + "." + propName + ",";
                        else
                            selectstr = selectstr + entitylable + "." + propName + " as " + entitylable + "_" + propName + ",";
                    }
                }
                else
                    if (Isjoined)
                    {
                        if (entitylable.Length == 0)
                            selectstr = selectstr + entityname + "." + propName + " as " + entityname + "_" + propName + ",";
                        else
                            selectstr = selectstr + entitylable + "." + propName + " as " + entitylable + "_" + propName + ",";
                    }
                    else
                    {
                        if (entitylable.Length == 0)
                            selectstr = selectstr + entityname + "." + propName + ",";
                        else
                            selectstr = selectstr + entitylable + "." + propName + ",";
                    }


                if ((say.ToString().Substring(say.ToString().Length - 1, 1) == "0" || say.ToString().Substring(say.ToString().Length - 1, 1) == "5") &&
                    selectstr.Substring(selectstr.Length - 2, 2) != "\n"
                )
                    selectstr = selectstr + "\n";
            }
            selectstr = selectstr.Substring(0, selectstr.LastIndexOf(','));
            if (Isjoined)
                selectstr = selectstr + " " + fromstr;
            else
                selectstr = "Select " + selectstr + "\nFrom " + entityname + "(nolock)  " + fromstr;
            return selectstr;
        }
        public static List<T> ConvertDataTableToObjectList<T>(mymodel.Model inentity, System.Data.DataTable entityRows, string str) where T : new()
        {
            List<T> entities = new List<T>();
            string entityname = inentity.GetType().Name;
            foreach (DataRow entityRow in entityRows.Rows)
            {
                T newEntity = (T)FillEntity(1, "", inentity.GetType(), entityRow);
                entities.Add(newEntity);
            }
            return entities;
        }
        public static Object FillEntity(int level, string prefix, Type entityType, System.Data.DataRow entityRow)
        {
            if (!string.IsNullOrEmpty(prefix)
                && entityRow[prefix + "Id"] == DBNull.Value) return null;

            PropertyInfo[] properties = entityType.GetProperties();
            Object newEntity = (Object)Activator.CreateInstance(entityType);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.DeclaringType == typeof(Object)) continue;
                if (pi.PropertyType.BaseType.Name.Contains("basemodel")
                    && level < 2)
                {
                    string childPrefix = string.IsNullOrEmpty(prefix) ?
                        pi.Name + "_"
                        : prefix + "_" + pi.Name + "_";

                    pi.SetValue(newEntity, FillEntity(level + 1, childPrefix, pi.PropertyType, entityRow), null);
                }
                else
                {
                    if (!entityRow.Table.Columns.Contains(prefix + pi.Name)
                        || entityRow[prefix + pi.Name] == DBNull.Value)
                        continue;

                    pi.SetValue(newEntity, entityRow[prefix + pi.Name], null);
                }
            }
            return newEntity;
        }
 
    }
}
