using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using SharpBullet.OAL.Metadata;

namespace SharpBullet.OAL.Schema
{
    public static class SchemaHelper
    {
        public static string Syncronize(Type baseEntityType)
        {
            System.Reflection.Assembly assembly =
                Metadata.DataDictionary.Instance.GetTypeofEntity(0).Assembly;

            StringBuilder str = new StringBuilder();
            if (SharpBullet.OAL.Configuration.GetValue("DbType").ToString().Contains("My"))
                updateMysql(str, assembly, baseEntityType);
            else
                updateMs(str, assembly, baseEntityType);

            return str.ToString();
        }

        private static void updateMs(StringBuilder str, Assembly assembly, Type baseEntityType)
        {
            foreach (Type entityType in assembly.GetTypes())
            {

                bool Table = true;
                if (entityType.GetCustomAttributes(typeof(EntityDefinitionAttribute), false).Length > 0)
                {
                    object obje = (EntityDefinitionAttribute)entityType.GetCustomAttributes(typeof(EntityDefinitionAttribute), false)[0];
                    Table = ((EntityDefinitionAttribute)obje).IsTable;
                }
                
                if (!entityType.IsClass
                    || !entityType.IsSubclassOf(baseEntityType)
                    || entityType.IsGenericType
                    || entityType.Name.Contains("<")  || !Table) continue;

                PropertyInfo[] props =
                    PersistenceStrategyProvider.FindStrategyFor(entityType)
                    .GetPersistentProperties(entityType);
                object[] memberinfo = entityType.GetCustomAttributes(typeof(EntityDefinitionAttribute), true);

                string className = entityType.Name;
                if (Configuration.GetValue("DbType")!= "System.Data.Sqlite"&&TableExistMs(className))
                {
                    DataTable table = executeSql("select * from " + className + " where Id=-1");

                    System.Collections.Hashtable fieldDictionary = new System.Collections.Hashtable();
                    foreach (PropertyInfo property in props)
                    {
                        //Derviş Aygün
                        FieldDefinitionAttribute fielddefinition = ReflectionHelper.GetAttribute<FieldDefinitionAttribute>(property);
                        if (fielddefinition != null && fielddefinition.MappingType == FieldMappingType.No)
                            continue;
                        //Derviş Aygün

                        FieldDefinitionAttribute definition = DataDictionary.Instance.GetFieldDefinition(className + "." + property.Name);
                        string fieldname = (property.PropertyType.IsSubclassOf(baseEntityType)) ?
                            definition.Name + "_Id" : definition.Name;

                        fieldDictionary[fieldname] = 1;
                        if (!table.Columns.Contains(fieldname))
                        {
                            string sql = "alter table " + className + " add " + fieldname + " " + MapType(definition);
                            executeNonQuery(sql);
                            str.Append("New Field: " + fieldname + ", Table: " + className + "<br/>");
                        }
                        else if (!fieldname.EndsWith("_Id") && table.Columns[fieldname].DataType.Name != definition.TypeName)
                        {
                            string sql = "alter table " + className + " alter column " + fieldname + " " + MapType(definition);
                            executeNonQuery(sql);
                            str.Append("Alter Field: " + fieldname + ", Table: " + className + "<br/>");
                        }
                    }
                    foreach (DataColumn column in table.Columns)
                    {
                        if (!fieldDictionary.ContainsKey(column.ColumnName))
                        {
                            string sql = "alter table " + className + " drop column " + column.ColumnName;
                            executeNonQuery(sql);
                            str.Append("Drop Field: " + column.ColumnName + ", Table: " + className + "<br/>");
                        }
                    }
                }
                else
                {
                    //Hiç özelliği olmayan class lar olabilir
                    if (props.Length == 0)
                    {
                        continue;
                    }

                    StringBuilder s = new System.Text.StringBuilder();
                    s.Append("CREATE TABLE " + className + " (");
                    foreach (PropertyInfo property in props)
                    {
                        FieldDefinitionAttribute definition = DataDictionary.Instance.GetFieldDefinition(className + "." + property.Name);
                        string fieldname = (property.PropertyType.IsSubclassOf(baseEntityType)) ?
                            definition.Name + "_Id" : definition.Name;
                        s.Append(fieldname + " " + MapType(definition));
                        if (definition.Name == "Id")
                        {
                            if (memberinfo != null && memberinfo.Length > 0)
                            {
                                EntityDefinitionAttribute attirebute = (EntityDefinitionAttribute)memberinfo[0];
                                if (attirebute.IdMethod == IdMethod.UserSubmitted)
                                    if (Configuration.GetValue("DbType")!= "System.Data.Sqlite")
                                        s.Append("  CONSTRAINT PK_" + className + "_Id PRIMARY KEY CLUSTERED");
                                    else
                                        s.Append("  PRIMARY KEY");
                            }
                            else
                                s.Append(" IDENTITY(1,1) CONSTRAINT PK_" + className + "_Id PRIMARY KEY CLUSTERED");
                        }
                        s.Append(", ");
                    }
                    s.Remove(s.Length - 2, 2);
                    s.Append(")");

                    string sql = s.ToString();
                    executeNonQuery(sql);
                    str.Append("New Table: " + className + "<br/>");
                }

                System.Collections.Hashtable fields = new System.Collections.Hashtable();
                Dictionary<string, List<string>> uniqueGroup = new Dictionary<string, List<string>>();
                Dictionary<string, List<string>> nonUniqueGroup = new Dictionary<string, List<string>>();
                foreach (PropertyInfo property in props)
                {
                    FieldDefinitionAttribute definition = DataDictionary.Instance.GetFieldDefinition(className + "." + property.Name);
                    string fieldname = (property.PropertyType.IsSubclassOf(baseEntityType)) ?
                        definition.Name + "_Id" : definition.Name;

                    if (!string.IsNullOrEmpty(definition.UniqueIndexGroup))
                    {
                        if (!uniqueGroup.ContainsKey(definition.UniqueIndexGroup))
                            uniqueGroup[definition.UniqueIndexGroup] = new List<string>();
                        
                        uniqueGroup[definition.UniqueIndexGroup].Add(fieldname);                        
                    }
                    if (!string.IsNullOrEmpty(definition.NonUniqueIndexGroup))
                    {
                        if (!nonUniqueGroup.ContainsKey(definition.NonUniqueIndexGroup))
                            nonUniqueGroup[definition.NonUniqueIndexGroup] = new List<string>();

                        nonUniqueGroup[definition.NonUniqueIndexGroup].Add(fieldname);
                    }
                }
                foreach (string indexGroup in uniqueGroup.Keys)
                {
                    executeNonQuery(CreateIndex(className, true, indexGroup, uniqueGroup[indexGroup]));
                }
                foreach (string indexGroup in nonUniqueGroup.Keys)
                {
                    executeNonQuery(CreateIndex(className, false, indexGroup, nonUniqueGroup[indexGroup]));
                }
            }
        }

        private static string CreateIndex(string className, bool unique, string indexGroup, List<string> fields)
        {
            string sqlScript = "";
            sqlScript += System.Environment.NewLine;
            sqlScript += " IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[" + className + "]') AND name = N'" + indexGroup + "')";
            sqlScript += System.Environment.NewLine;
            sqlScript += " BEGIN";
            sqlScript += System.Environment.NewLine;
            sqlScript += " CREATE ";
            if (unique)
            {
                sqlScript += " UNIQUE ";
            }
            sqlScript += " NONCLUSTERED ";
            
            sqlScript += " INDEX [" + indexGroup + "] ON [dbo].[" + className + "] ";

            sqlScript += " (";
            sqlScript += System.Environment.NewLine;
            
            foreach (var field in fields)
            {  
                sqlScript += "     [" + field.Trim() + "] ASC,";
                sqlScript += System.Environment.NewLine;
            }           

            sqlScript = sqlScript.Substring(0, sqlScript.Length - ("," + System.Environment.NewLine).Length);
            sqlScript += System.Environment.NewLine;
            sqlScript += " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]";
            sqlScript += System.Environment.NewLine;
            sqlScript += " END";

            return sqlScript;
        }

        private static void updateMysql(StringBuilder str, Assembly assembly, Type baseEntityType)
        {
            foreach (Type entityType in assembly.GetTypes())
            {
                bool Table = true;
                if (entityType.GetCustomAttributes(typeof(EntityDefinitionAttribute), false).Length > 0)
                {
                    object obje = (EntityDefinitionAttribute)entityType.GetCustomAttributes(typeof(EntityDefinitionAttribute), false)[0];
                    Table = ((EntityDefinitionAttribute)obje).IsTable;
                }

                if (!entityType.IsClass
                    || !entityType.IsSubclassOf(baseEntityType)
                    || entityType.IsGenericType
                    || entityType.Name.Contains("<") || !Table) continue;

                PropertyInfo[] props =
                    PersistenceStrategyProvider.FindStrategyFor(entityType)
                    .GetPersistentProperties(entityType);

                object[] memberinfo=entityType.GetCustomAttributes(typeof(EntityDefinitionAttribute), true);

                string className = entityType.Name;
                if (TableExistMy(className, Transaction.Instance.GetSchema()))
                {
                    DataTable table = executeSql("select * from " + className + " where Id=-1");
                    DataTable metaTable = Transaction.Instance.MetaTableColumns(className);
                    Dictionary<string, int> metaLengths = new Dictionary<string, int>();
                    foreach (DataRow row in metaTable.Rows)
                    {
                        if ((string)row["DATA_TYPE"] != "varchar") continue;
                        metaLengths[(string)row["COLUMN_NAME"]] = Convert.ToInt32(row["CHARACTER_MAXIMUM_LENGTH"]);
                    }

                    System.Collections.Hashtable fieldDictionary = new System.Collections.Hashtable();
                    foreach (PropertyInfo property in props)
                    {
                        //Derviş Aygün
                        FieldDefinitionAttribute fielddefinition = ReflectionHelper.GetAttribute<FieldDefinitionAttribute>(property);
                        if (fielddefinition != null && fielddefinition.MappingType == FieldMappingType.No)
                            continue;
                        //Derviş aygün
                        FieldDefinitionAttribute definition = DataDictionary.Instance.GetFieldDefinition(className + "." + property.Name);
                        string fieldname = (property.PropertyType.IsSubclassOf(baseEntityType)) ?
                            definition.Name + "_Id" : definition.Name;

                        fieldDictionary[fieldname] = 1;
                        if (!table.Columns.Contains(fieldname))
                        {
                            string sql = "alter table " + className + " add " + fieldname + " " + MapTypeMysql(definition);
                            executeNonQuery(sql);
                            str.Append("New Field: " + fieldname + ", Table: " + className + "<br/>");
                        }
                        else if (!fieldname.EndsWith("_Id")
                            && definition.TypeName != "Boolean"
                            && table.Columns[fieldname].DataType.Name != definition.TypeName)
                        {
                            string sql = "alter table " + className + " modify column " + fieldname + " " + MapTypeMysql(definition);
                            executeNonQuery(sql);
                            str.Append("Alter Field: " + fieldname + ", Table: " + className + "<br/>");
                        }
                        else if (!fieldname.EndsWith("_Id")
                            && definition.TypeName == "Boolean"
                            && table.Columns[fieldname].DataType.Name != "SByte")
                        {
                            string sql = "alter table " + className + " modify column " + fieldname + " " + MapTypeMysql(definition);
                            executeNonQuery(sql);
                            str.Append("Alter Field: " + fieldname + ", Table: " + className + "<br/>");
                        }
                        else if (metaLengths.ContainsKey(fieldname)
                            && definition.Length != metaLengths[fieldname])
                        {
                            string sql = "alter table " + className + " modify column " + fieldname + " " + MapTypeMysql(definition);
                            executeNonQuery(sql);
                            str.Append("Resize Field: " + fieldname + ", Table: " + className + ", Length: " + definition.Length + "<br/>");
                        }
                    }
                    foreach (DataColumn column in table.Columns)
                    {
                        if (!fieldDictionary.ContainsKey(column.ColumnName))
                        {
                            string sql = "alter table " + className + " drop column " + column.ColumnName;
                            executeNonQuery(sql);
                            str.Append("Drop Field: " + column.ColumnName + ", Table: " + className + "<br/>");
                        }
                    }
                }
                else
                {
                    //Hiç özelliği olmayan class lar olabilir
                    if (props.Length == 0)
                    {
                        continue;
                    }

                    StringBuilder s = new System.Text.StringBuilder();
                    s.Append("CREATE TABLE " + className + " (");
                    string pkstr = "";
                    foreach (PropertyInfo property in props)
                    {
                        FieldDefinitionAttribute definition = DataDictionary.Instance.GetFieldDefinition(className + "." + property.Name);
                        string fieldname = (property.PropertyType.IsSubclassOf(baseEntityType)) ?
                            definition.Name + "_Id" : definition.Name;
                        s.Append("`" + fieldname + "` " + MapTypeMysql(definition));
                        if (definition.Name == "Id")
                        {
                            if (memberinfo != null && memberinfo.Length > 0)
                            {
                                EntityDefinitionAttribute attirebute = (EntityDefinitionAttribute)memberinfo[0];
                                if(attirebute.IdMethod==IdMethod.UserSubmitted)
                                    s.Append(" NOT NULL ");
                            }
                            else
                                s.Append(" NOT NULL AUTO_INCREMENT");
                            
                            pkstr = " ,PRIMARY KEY (`Id`)";
                        }

                        s.Append(", ");
                    }
                    s.Remove(s.Length - 2, 2);
                    s.Append(pkstr);
                    s.Append(")");

                    string sql = s.ToString();
                    executeNonQuery(sql);
                    str.Append("New Table: " + className + "<br/>");
                }
            }
        }

        private static DataTable executeSql(string sql)
        {
            return Transaction.Instance.ExecuteSql(sql);
        }

        private static void executeNonQuery(string sql)
        {
            Transaction.Instance.ExecuteNonQuery(sql);
        }

        private static string MapType(FieldDefinitionAttribute definition)
        {
            switch (definition.TypeName)
            {
                case "Byte":
                    return "tinyint";
                case "String":
                    return "varchar(" + definition.Length + ")";
                case "Int32":
                    return "int";
                case "Int64":
                    return "bigint";
                case "Boolean":
                    return "bit";
                case "Decimal":
                    return "decimal(18,4)";
                case "DateTime":
                    return "DateTime";
                case "Text":
                    return "ntext";
                case "Byte[]":
                 
                    if(definition.Length==8000)
                        return "varbinary(MAX)";
                 return "varbinary(" + definition.Length + ")";
                case "nvarchar":
                    return "nvarchar(" + definition.Length + ")";
                default:
                    return "bigint"; //id field of foregin keys.
            }
        }

        private static string MapTypeMysql(FieldDefinitionAttribute definition)
        {
            switch (definition.TypeName)
            {
                case "String":
                    return "nvarchar(" + definition.Length + ")";
                case "Int32":
                    return "int";
                case "Boolean":
                    return "tinyint(1)";
                case "Decimal":
                    return "decimal(18,4)";
                case "DateTime":
                    return "DateTime";
                default:
                    return "bigint"; //id field of foregin keys.
            }
        }

        private static bool TableExistMs(string tableName)
        {
            return
                1 == Transaction.Instance.ExecuteScalarI(
                        @"SELECT COUNT(*) AS tablecount
                        FROM INFORMATION_SCHEMA.TABLES
                        WHERE (TABLE_TYPE = 'BASE TABLE') AND (TABLE_NAME = '" + tableName + "')",
                        null);
        }

        private static bool TableExistMy(string tableName, string schema)
        {
            return
                1 == Transaction.Instance.ExecuteScalarI(
                        @"SELECT count(*) 
                        FROM information_schema.tables 
                        WHERE                                                     
                            table_name = '" + tableName + "' and TABLE_SCHEMA='" + schema + "'");
        }
        private static bool TableExistLite(string tableName)
        {
            return
                1 == Transaction.Instance.ExecuteScalarI(
                        @"SELECT count(*) FROM '" + tableName + "'");
        }
    }
}
