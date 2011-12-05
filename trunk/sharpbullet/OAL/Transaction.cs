using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using SharpBullet.DAL;
using System.Data;

namespace SharpBullet.OAL
{
    public delegate Transaction GetTransaction();

    public class Transaction
    {
        /// <summary>
        /// Web ve Application server gibi ortamlardaki session mantýðýný desteklemek için kullanýlýr.
        /// Yani set edilen T.Provider, session bazýnda bir nesne verebilmelidir.
        /// </summary>
        public static GetTransaction TransactionProvider;

        private static ISqlHelper sqlHelper;
        private static string httpContextKey = "SharpBullet.Transaction";

        private static Transaction instance;
        public static Transaction Instance
        {
            get
            {
                //Asp.Net içinde SharpBullet kullanuýldýðýnda, her sayfa request'inin kendi transaction kuyruðu olmalý.
                if (System.Web.HttpContext.Current != null)
                {
                    if (!System.Web.HttpContext.Current.Items.Contains(httpContextKey))
                    {
                        System.Web.HttpContext.Current.Items.Add(httpContextKey, new Transaction());
                    }
                    return (Transaction)System.Web.HttpContext.Current.Items[httpContextKey];
                }
                
                if (TransactionProvider != null) 
                    return TransactionProvider();

                if (instance == null) instance = new Transaction();
                return instance;
            }
        }

        private Stack<IConnectionHandle> stack = new Stack<IConnectionHandle>();

        public delegate void D();        

        public void Join(D d)
        {
            object result = null;
            bool owner;
            IConnectionHandle connection;
            
            if (stack.Count == 0) //If there is no transaction start a new one.
            {
                string connectionString, dbType;

                dbType = Configuration.GetString("DbType");
                connectionString = Configuration.GetString("Connection");

                connection = DataAccess.GetConnection(connectionString, dbType);
                
                DataAccess.Open(connection);
                DataAccess.BeginTransaction(connection);
                stack.Push(connection);
                owner = true; //If this method call starts a new transaction, mark it as owner.
            }
            else 
                owner = false; 

            try
            {
                d(); //Execute the anonymous method
            }
            catch (Exception exp)
            {
                if (owner)
                {
                    connection = stack.Pop();
                    DataAccess.Rollback(connection.TransactionHandle);
                    DataAccess.Close(connection);
                }
                //TODO: Convert exception to a user friendly message. Conversion can take place in provider specific sql helper.
                throw;
            }

            if (owner)
            {
                connection = stack.Pop();
                DataAccess.Commit(connection.TransactionHandle);
                DataAccess.Close(connection);
            }
        }

        public DataTable ExecuteSql(string query, params object[] parameterValues)
        {
            IConnectionHandle connection;
            DataTable table = null;

            if (!((string)Configuration.GetValue("DbType")).Contains(".SqlClient"))
                query = SqlHelper().Translate(query);


            Join(delegate()
            {
                connection = stack.Peek();
                table = DataAccess.ExecuteSql(connection, query, parameterValues);
            });

            return table;
        }

        public int ExecuteNonQuery(string query, params object[] parameterValues)
        {
            return ExecuteNonQuery(query, parameterValues, null);
        }

        public int ExecuteNonQuery(string query, object[] parameterValues, params IParameterHandle[] extraParameters)
        {
            IConnectionHandle connection;
            int i = 0;

            if (!((string)Configuration.GetValue("DbType")).Contains(".SqlClient"))
                query = SqlHelper().Translate(query);

            Join(delegate()
            {
                connection = stack.Peek();
                i = DataAccess.ExecuteNonQuery(connection, query, parameterValues, extraParameters);
            });

            return i;
        }
 
        public object ExecuteScalar(string query, params object[] parameterValues)
        {
            IConnectionHandle connection;
            object result = null;

            if (!((string)Configuration.GetValue("DbType")).Contains(".SqlClient"))
                query = SqlHelper().Translate(query);

            Join(delegate()
            {
                connection = stack.Peek();
                result = DataAccess.ExecuteScalar(connection, query, parameterValues);
            });

            return result;
        }

        public DateTime ExecuteScalarDT(string query, params object[] parameterValues)
        {
            DateTime result;

            result = Convert.ToDateTime(
                ExecuteScalar(query, parameterValues));

            return result;
        }

        public decimal ExecuteScalarD(string query, params object[] parameterValues)
        {
            decimal result = 0;

            object value = ExecuteScalar(query, parameterValues);
            if (value != DBNull.Value)
                result = Convert.ToDecimal(value);

            return result;
        }

        public int ExecuteScalarI(string query, params object[] parameterValues)
        {
            int result = 0;


            object value = ExecuteScalar(query, parameterValues);
            if (value != DBNull.Value)
                result = Convert.ToInt32(value);

            return result;
        }

        public long ExecuteScalarL(string query, params object[] parameterValues)
        {
            long result = 0;

            object value = ExecuteScalar(query, parameterValues);
            if (value != DBNull.Value)
                result = Convert.ToInt64(value);

            return result;
        }

        public IParameterHandle NewParameter(string name, object value, ParameterDirection direction)
        {
            IParameterHandle handle;
            string dbType;

            dbType = Configuration.GetString("DbType");
            handle = DataAccess.NewParameter(dbType, name, value, direction);

            return handle;
        }

        public string GetSchema()
        {
            string dbType = Configuration.GetString("DbType");
            string connectionString = Configuration.GetString("Connection");

            return DataAccess.GetSchema(connectionString, dbType);
        }

        public static ISqlHelper SqlHelper()
        {
            if (sqlHelper == null)
            {
                string dbType;

                dbType = Configuration.GetString("DbType");
                sqlHelper = SqlHelperProvider.FindHelperFor(dbType);
            }

            return sqlHelper;
        }
        
        public DataTable MetaTableColumns(string tablename)
        {
            DataTable result = null;
            IConnectionHandle connection;
            
            Join(delegate()
            {
                connection = stack.Peek();
                result = DataAccess.MetaTableColumns(connection, tablename);
            });
            return result;
        }
    }
}
