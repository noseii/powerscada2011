using System;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.EntityClient;
using System.Configuration;


namespace mycommon
{
    public class myUtil
    {
        public static string RunSqlStr(string str)
        {
            SqlCommand cmd = new SqlCommand(str);
            return RunSql(cmd);
        }
        public static string RunSql(SqlCommand cmd)
        {
            try
            {
                EntityConnectionStringBuilder entcon = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings[mydecs.constrname].ConnectionString);
                SqlConnection cnn = new SqlConnection(entcon.ProviderConnectionString);
                cmd.CommandTimeout = 1000000;
                cmd.Connection = cnn;
                cnn.Open();
                try
                {
                    cmd.CommandText = "set dateformat dmy;\n " + cmd.CommandText;
                    cmd.ExecuteNonQuery();
                }

                finally
                {
                    cnn.Close();
                }
            }
            catch (Exception e)
            {
                return "error:" + e.Message;
            }
            return "ok";
        }
     
        public static string GetMakAdres()
        {
            string makadres = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            makadres = makadres.Replace(@"\", "_");
            makadres = makadres.Insert(0, "_");
            makadres = makadres.Insert(0, System.Environment.MachineName);

            return makadres;
        }
        
        public static bool Insert(Object inentity)
        {
                //TODO:insertte object yada id dönmeli
                SqlCommand cmd = new SqlCommand();
                PropertyInfo[] props = inentity.GetType().GetProperties();
                string sql = @"INSERT INTO {0}
                                    (
                                        Id,Aktif,EklemeTarihi,Islemturu,ekleyenmakadres,
                                        {1}
                                    ) 
                                VALUES 
                                    (
                                         (select isnull(max(Id),0)+1 from {0}),1,getdate()," +
                                          (int)mycommon.enumcommon.Islemturu.Insert + ",'" + GetMakAdres() + @"',
                                         {2}
                                    )
                              ";

                string names = "";
                string values = "";
                SqlParameterCollection parameters = cmd.Parameters;

                foreach (PropertyInfo pi in props)
                {
                    string propName = pi.Name;
                    object value = pi.GetValue(inentity, null);
                    if (value == null || 
                        pi.Name == "Id" ||
                        pi.Name == "Aktif" ||
                        pi.Name == "EklemeTarihi" ||
                        pi.Name == "EkleyenMakAdres" ||
                        pi.Name == "Islemturu" ||
                        (pi.PropertyType ==typeof(DateTime) &&(DateTime)value == DateTime.MinValue)
                        ) 
                        continue;
                    if (pi.PropertyType.BaseType.Name.Contains("basemodel"))
                    {
                        propName = pi.Name + "_Id";
                        if (value != DBNull.Value)
                            value = GetValue(value, "Id");
                    }
                    
                    names = names + propName + ",";
                    values = values + "@" + propName + ",";

                    SqlParameter p = new SqlParameter(propName, value);
                    parameters.Add(p);
                }
                names = names.Substring(0, names.LastIndexOf(','));
                values = values.Substring(0, values.LastIndexOf(','));

                sql = string.Format(sql, inentity.GetType().Name, names, values);
                cmd.CommandText = sql;

                string res = RunSql(cmd);
                if (res=="ok")
                {
                    return true;
                }
                else
                    return false;
        }
        public static bool Update(Object inentity)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                PropertyInfo[] props = inentity.GetType().GetProperties();
                string sql = "UPDATE {0} SET DegistirmeTarihi=getdate(),DegistirenMakAdres='" + GetMakAdres() + 
                                            "',Islemturu=" + (int)mycommon.enumcommon.Islemturu.Update +",{1} WHERE Id={2}";
                string names = "";
                SqlParameterCollection parameters = cmd.Parameters;
                foreach (PropertyInfo pi in props)
                    if (pi.Name!="Id")
                    {
                        string propName = pi.Name;
                        if (pi.Name == "DegistirmeTarihi" ||
                            pi.Name == "DegistirenMakAdres" ||
                            pi.Name == "Islemturu")
                            continue;

                        object value = pi.GetValue(inentity, null);
                        if (value == null)
                            value = DBNull.Value;
                        if (pi.PropertyType == typeof(DateTime) &&
                            (DateTime)value == DateTime.MinValue)
                            value = DBNull.Value;

                        if (pi.PropertyType.BaseType.Name.Contains("basemodel"))
                        {
                            propName = pi.Name + "_Id";
                            if (value != DBNull.Value)
                                value = GetValue(value, "Id"); 
                        }
                        names = names + propName + "=" + "@" + propName + ",";
                        SqlParameter p = new SqlParameter(propName, value);
                        parameters.Add(p);
                    }
                names = names.Substring(0, names.LastIndexOf(','));
                object Id = inentity.GetType().GetProperty("Id").GetValue(inentity, null);
                sql = string.Format(sql, inentity.GetType().Name, names, Id);
                cmd.CommandText = sql;

                string res = RunSql(cmd);
                if (res == "ok")
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool Delete(Object inentity)
        {
            try
            {
                return RunSql(new SqlCommand("DELETE FROM " + inentity.GetType().Name + " WHERE Id = " + inentity.GetType().GetProperty("Id").GetValue(inentity, null))) != "error";
            }
            catch
            {
                return false;
            }
        }


        public static List<T> Select<T>(T inentity, string wherestr, bool withdesc) where T : new()
        {
            string selectstr = CreateSelectStrFromObject(inentity, "", withdesc, false) + "\n" + wherestr;
            DataTable dt = OpenSqlIntoDataTable(selectstr);
            List<T> list = ConvertDataTableToObjectList<T>(inentity, dt, selectstr);
            return list;
        }

       

        public static DataTable OpenSqlIntoDataTable(string str)
        {
            EntityConnectionStringBuilder entcon = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings[mydecs.constrname].ConnectionString);
            SqlConnection cnn = new SqlConnection(entcon.ProviderConnectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(str);
            cmd.Connection = cnn;
            cmd.CommandTimeout = 1000000;
            cnn.Open();
            da.SelectCommand = cmd;
            DataTable dtGet = new DataTable();
            da.Fill(dtGet);
            return dtGet;
        }
        public static string CreateSelectStrFromObject(Object inentity, string entitylable, bool withdesc, bool Isjoined)
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
                if (pi.PropertyType.BaseType.Name.Contains("basemodel"))
                {
                    if (withdesc)
                    {
                        Object joinedentity = (Object)Activator.CreateInstance(pi.PropertyType);
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
        public static List<T> ConvertDataTableToObjectList<T>(T inentity, DataTable entityRows, string str) where T : new()
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
        public static Object FillEntity(int level, string prefix, Type entityType, DataRow entityRow)
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
        private static object GetValue(object obj, string prop)
        {
            if (obj == null) return DBNull.Value;
            object x = obj.GetType().GetProperty(prop).GetValue(obj, null);
            if (x == null) return DBNull.Value;
            return x;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inentity"></param>
        /// <returns></returns>
        public static bool DoScript(Object inentity)
        {
            
            
            try
            {
                string sql = @" drop table {0};
                                Create Table {0}
                                (
                                    {1},
                                    CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED 
                                (
	                                [Id] ASC
                                )
                                WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, 
                                IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                                ) ON [PRIMARY]";

                SqlCommand cmd = new SqlCommand();
                PropertyInfo[] props = inentity.GetType().GetProperties();
                string names = "";
                
                foreach (PropertyInfo pi in props)
                {
                    string propName = pi.Name;
                    if (pi.PropertyType.BaseType.Name.Contains("basemodel"))
                        propName = pi.Name + "_Id bigint";
                    else                        
                        if (pi.PropertyType.BaseType.FullName.Contains("System.Enum"))
                            propName = pi.Name + " tinyint";
                        else
                        {
                            //TODO:otomatik db scriptte string ve charlar için 100 problemi çözülmeli
                            switch (pi.PropertyType.Name)
                            {
                                case "Byte":
                                    propName = pi.Name + " tinyint";
                                    break;
                                case "String":
                                    propName = pi.Name + " varchar(100)";
                                    break;
                                case "Int16":
                                    propName = pi.Name + " smallint";
                                    break;
                                case "Int32":
                                    propName = pi.Name + " int";
                                    break;
                                case "Int64":
                                    propName = pi.Name + " bigint";
                                    break;
                                case "Boolean":
                                    propName = pi.Name + " bit";
                                    break;
                                case "Decimal":
                                    propName = pi.Name + " decimal(18,4)";
                                    break;
                                case "DateTime":
                                    propName = pi.Name + " DateTime";
                                    break;
                                case "Char":
                                    propName = pi.Name + " Char(1)";
                                    break;
                                default:
                                    propName = pi.Name + " "+pi.PropertyType.Name;
                                    break;
                            }
                        }

                    names = names + propName + ",\n";
                }
                names = names.Substring(0, names.LastIndexOf(','));

                sql = string.Format(sql, inentity.GetType().Name, names);
                cmd.CommandText = sql;

                string res = RunSql(cmd);
                return res.Substring(0, 5) != "error";
            }
            catch
            {
                return false;
            }
        }


        }
}
