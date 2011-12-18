using System;
using System.Collections.Generic;
using System.Text;
using SharpBullet.DAL;
using System.Data;
using SharpBullet.OAL.Metadata;

namespace SharpBullet.OAL
{
    public delegate object GetValueDelegate(string key);
    public delegate void SetValueDelegate(string key, object value);

    public class Persistence
    {
        public static GetValueDelegate GetValue;
        public static SetValueDelegate SetValue;

        #region Single Read
        public static T Read<T>(object primaryKey)
        {
            Type type;
            type = typeof(T);

            return (T)Read(type, primaryKey);
        }

        public static object Read(Type type, object primaryKey)
        {
            object entityObject = Activator.CreateInstance(type);

            return Read(entityObject, primaryKey);
        }

        public static object Read(object entity, object primaryKey)
        {
            Type type = entity.GetType();
            DataTable table;
            PersistenceStrategy strategy;
            string tableName, keyColumn, keyParamName, sql;
            string[] fieldNames;

            strategy = PersistenceStrategyProvider.FindStrategyFor(type);
            tableName = strategy.GetTableNameOf(type);
            keyColumn = strategy.GetKeyColumnOf(type);

            sql = strategy.GetSelectStatementFor(type, new string[] { keyColumn }, new string[] { "@prm0" });

            //For types which are not views, sql is null or empty.
            if (string.IsNullOrEmpty(sql))
            {
                keyParamName = Transaction.SqlHelper().GenerateParamName(0);
                fieldNames = strategy.GetSelectFieldNamesOf(type);

                sql = Transaction.SqlHelper().BuildSelectSqlFor(tableName, fieldNames,
                    new string[] { keyColumn },
                    new string[] { keyParamName }, null, 0);
            }

            table = Transaction.Instance.ExecuteSql(sql, primaryKey);
            if (table.Rows.Count > 0)
            {
                strategy.Fill(entity, table.Rows[0]);
            }
            else
                entity = null;

            return entity;
        }

        /// <summary>
        /// Entity must exist, otherwise exception must be handled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T Read<T>(params Condition[] parameters)
        {
            T entity;
            T[] entityList;

            entityList = ReadList<T>(null, parameters, null, 1);
            if (entityList.Length > 0)
                entity = entityList[0];
            else
                entity = default(T);

            return entity;
        }
        #endregion

        #region Multiple Read
        public static DataTable ReadListTable(Type entityType, string[] fields, Condition[] parameters, string[] orders, int limitNumberOfEntities)
        {
            PersistenceStrategy strategy;
            string tableName, sql, paramPrefix, paramSuffix;
            string[] filterFields, filterParams;
            object[] parameterValues;
            DataTable table;
            Operator[] operators;

            strategy = PersistenceStrategyProvider.FindStrategyFor(entityType);
            tableName = strategy.GetTableNameOf(entityType);
            filterFields = StrHelper.GetPropertyValuesOf(parameters, "Field");
            filterParams = StrHelper.GetNumbers(0, filterFields.Length);
            parameterValues = ArrayHelper.GetPropertyValuesOf(parameters, "Value");
            operators = ArrayHelper.GetPropertyValuesOf<Operator>(parameters, "Operator");

            paramPrefix = Transaction.SqlHelper().ParameterPrefix();
            paramSuffix = Transaction.SqlHelper().ParameterSuffix();
            filterParams = StrHelper.Concat(paramPrefix, filterParams, paramSuffix);

            sql = Transaction.SqlHelper().BuildSelectSqlFor(tableName, fields, filterFields, operators, filterParams, orders, limitNumberOfEntities);

            table = Transaction.Instance.ExecuteSql(sql, parameterValues);

            return table;
        }

        public static T[] ReadList<T>(string[] fields, Condition[] parameters, string[] orders, int limitNumberOfEntities)
        {
            Type type;
            T[] entities;
            PersistenceStrategy strategy;
            DataTable table;

            type = typeof(T);
            strategy = PersistenceStrategyProvider.FindStrategyFor(type);
            table = ReadListTable(type, fields, parameters, orders, limitNumberOfEntities);

            if (table.Rows.Count > 0)
            {
                entities = new T[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    entities[i] = Activator.CreateInstance<T>();
                    strategy.Fill(entities[i], table.Rows[i]);
                }
            }
            else
                entities = new T[0];

            return entities;
        }

        /// <summary>
        /// Tüm kayýtlarý okur. 'Select * from tablo'
        /// </summary>
        /// <typeparam name="T">Okunmasý istenen Entity</typeparam>
        /// <returns>Sonuçlarý dizi olarak döndürür</returns>
        public static T[] ReadList<T>()
        {
            return ReadList<T>(null);
        }

        public static T[] ReadList<T>(string sql, params object[] parameterValues)
        {
            Type type;
            T[] entities;
            PersistenceStrategy strategy;
            DataTable table;

            type = typeof(T);
            strategy = PersistenceStrategyProvider.FindStrategyFor(type);
            if (string.IsNullOrEmpty(sql))
                sql = string.Format("select * from {0}", strategy.GetTableNameOf(type));

            table = Transaction.Instance.ExecuteSql(sql, parameterValues);

            if (table.Rows.Count > 0)
            {
                entities = new T[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    entities[i] = Activator.CreateInstance<T>();
                    strategy.Fill(entities[i], table.Rows[i]);
                }
            }
            else
                entities = new T[0];

            return entities;
        }

        public static T[] ReadDetail<T>(string parent, object id)
        {
            return ReadList<T>(null, new Condition[] { new Condition(parent, Operator.Equal, id) }, null, 0);
        }
        #endregion

        #region Save Methods
        public static object Insert(object entity)
        {
            Type type;
            PersistenceStrategy strategy;
            string tableName, sql, idSql, paramPrefix, paramSuffix;
            string[] fieldNames, parameterNames;
            object[] fieldValues;
            int i;
            IParameterHandle idParam = null;
            IdMethod idMethod;
            object idValue = 0;

            //!!! bu kodun -entity içindeki deðerler alýnmadan- önce çalýþmasý lazým.
            IInsertInfo iInfo = entity as IInsertInfo;
            if (iInfo != null && GetValue != null)
            {
                iInfo.InsertUser = (int)(GetValue("Kullanici_Id") ?? 0);
                iInfo.InsertDate = DateTime.Now;
            }
            
            type = entity.GetType();
            strategy = PersistenceStrategyProvider.FindStrategyFor(type);
            paramPrefix = Transaction.SqlHelper().ParameterPrefix();
            paramSuffix = Transaction.SqlHelper().ParameterSuffix();

            tableName = strategy.GetTableNameOf(type);
            fieldNames = strategy.GetInsertFieldNamesOf(type);
            parameterNames = StrHelper.GetNumbers(0, fieldNames.Length);
            parameterNames = StrHelper.Concat(paramPrefix, parameterNames, paramSuffix);
            fieldValues = strategy.GetFieldValuesOf(entity, fieldNames);
            

            sql = Transaction.SqlHelper().BuildInsertSqlFor(tableName, fieldNames, parameterNames);

            idMethod = strategy.GetIdMethodFor(type);
            switch (idMethod)
            {
                case IdMethod.Identity:
                    idValue = 0;
                    idParam = Transaction.Instance.NewParameter("NewId", idValue, ParameterDirection.Output);
                    sql = Transaction.SqlHelper().
                        BuildInsertSqlWithIdentity(tableName, fieldNames, parameterNames, "NewId");
                    break;
                case IdMethod.BySql:
                    idSql = strategy.GetIdSqlFor(type);
                    idValue = Transaction.Instance.ExecuteScalar(idSql);
                    break;
                case IdMethod.Custom:
                    idValue = strategy.GetIdFor(entity, Transaction.Instance);
                    break;
                case IdMethod.UserSubmitted:
                    idValue = strategy.GetIdFor(entity, Transaction.Instance);
                    ((ActiveRecord.ActiveRecordBase)entity).Id =(long)idValue;
                    fieldValues = strategy.GetFieldValuesOf(entity, fieldNames);
                    break;
            }

            if (Transaction.SqlHelper().GetType() == typeof(MySqlHelper))
            {               
                //MySql için output parametreler ile ilgili sorun var!!!
                idParam.Value = Transaction.Instance.ExecuteScalar(sql, fieldValues);
                i = 1; //*** 
            }
            else
            {
                i = Transaction.Instance.ExecuteNonQuery(sql, fieldValues, idParam);
            }

            if (idParam != null)
                idValue = idParam.Value; //this works when 'idMethod' is '..Identity'

            return idValue;
        }

        public static int Update(object entity)
        {
            Type type;
            PersistenceStrategy strategy;
            string tableName, sql, keyField, keyParameter, paramPrefix, paramSuffix,
                optimisticLockField;
            string[] fieldNames, parameterNames;
            object[] fieldValues;
            object keyValue;
            byte optimisticLockValue;
            int i;

            type = entity.GetType();
            strategy = PersistenceStrategyProvider.FindStrategyFor(type);

            tableName = strategy.GetTableNameOf(type);
            fieldNames = strategy.GetUpdateFieldNamesOf(type);
            parameterNames = StrHelper.GetNumbers(0, fieldNames.Length);

            paramPrefix = Transaction.SqlHelper().ParameterPrefix();
            paramSuffix = Transaction.SqlHelper().ParameterSuffix();
            parameterNames = StrHelper.Concat(paramPrefix, parameterNames, paramSuffix);

            keyField = strategy.GetKeyColumnOf(type);
            keyParameter = paramPrefix + fieldNames.Length;
            keyValue = strategy.GetKeyValueOf(entity);

            optimisticLockField = strategy.GetOptimisticLockField(type);
            optimisticLockValue = 0;
            if (!string.IsNullOrEmpty(optimisticLockField))
                optimisticLockValue = (byte)strategy.GetOptimisticLockValue(entity);

            fieldValues = strategy.GetFieldValuesOf(entity, fieldNames);
            ArrayHelper.Merge<object>(ref fieldValues, keyValue);

            sql = Transaction.SqlHelper().BuildUpdateSqlFor(tableName, keyField, keyParameter, 
                optimisticLockField, optimisticLockValue,
                fieldNames, parameterNames);

            i = Transaction.Instance.ExecuteNonQuery(sql, fieldValues);
            return i;
        }
        #endregion

        #region Delete Methods
        public static void DeleteByKey<T>(object key, bool throwException)
        {
            DeleteByKey(typeof(T), key, throwException);
        }

        public static void DeleteByKey(Type entityType, object key, bool throwException)
        {
            PersistenceStrategy strategy;
            string tableName, keyField, sql, keyParamName, keyParamSql;
            IParameterHandle idParameter;
            int numberOfRows;

            strategy = PersistenceStrategyProvider.FindStrategyFor(entityType);
            tableName = strategy.GetTableNameOf(entityType);
            keyField = strategy.GetKeyColumnOf(entityType);

            keyParamName = "prmId";
            keyParamSql = Transaction.SqlHelper().GenerateParamName(keyParamName);

            sql = Transaction.SqlHelper().BuildDeleteSqlFor(tableName, keyField, keyParamSql);
            idParameter = Transaction.Instance.NewParameter(keyParamName, key, ParameterDirection.Input);

            numberOfRows = Transaction.Instance.ExecuteNonQuery(sql, ArrayHelper.EmptyArray, idParameter);
        }
        #endregion

    }
}

/*

*/