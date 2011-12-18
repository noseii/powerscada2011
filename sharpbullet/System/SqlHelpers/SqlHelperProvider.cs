using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace System
{
    public class SqlHelperProvider
    {
        private static Hashtable helperList = new Hashtable();

        static SqlHelperProvider()
        {
            AddHelper("System.Data.SqlClient", new MsSqlHelper());
            AddHelper("MySql.Data.MySqlClient", new MySqlHelper());
            AddHelper("System.Data.Sqlite", new SqliteHelper());
        }

        public static void AddHelper(string dbType, ISqlHelper helper)
        {
            helperList[dbType] = helper;
        }

        public static ISqlHelper FindHelperFor(string dbType)
        {
            ISqlHelper helper;

            helper = (ISqlHelper)helperList[dbType];

            return helper;
        }
    }
}
