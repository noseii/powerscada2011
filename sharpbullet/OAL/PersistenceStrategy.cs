using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SharpBullet.OAL
{
    public abstract class PersistenceStrategy
    {
        public abstract string GetTableNameOf(Type type);

        public abstract string GetKeyColumnOf(Type type);

        /// <summary>
        /// Must return a sql for 'view' types, otherwise return empty or null.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract string GetSelectStatementFor(Type type, string[] filterFields, string[] filterValues);

        public abstract void Fill(object entity, System.Data.DataRow dataRow);

        public abstract bool CanPersist(Type type);

        public abstract string[] GetPersistentFieldNamesOf(Type type);

        public abstract PropertyInfo[] GetPersistentProperties(Type type);

        public abstract object[] GetFieldValuesOf(object entity, string[] fieldNames);

        public abstract string[] GetInsertFieldNamesOf(Type type);

        public abstract string[] GetUpdateFieldNamesOf(Type type);

        public abstract string[] GetSelectFieldNamesOf(Type type);

        public abstract object GetKeyValueOf(object entity);

        public abstract IdMethod GetIdMethodFor(Type type);

        public abstract string GetIdSqlFor(Type type);

        public abstract object GetIdFor(object entity, Transaction transaction);

        public abstract string GetOptimisticLockField(Type type);

        public abstract object GetOptimisticLockValue(object entity);
    }
}
