using System;
namespace System
{
    public interface ISqlHelper
    {
        string BuildDeleteSqlFor(string tableName, string keyField, string keyValue);
        string BuildInsertSqlFor(string tableName, string[] fields, string[] values);
        string BuildInsertSqlWithIdentity(string tableName, string[] fields, string[] values, string identityParamName);
        string BuildSelectSqlFor(string tableName, string[] fields, string[] filterFields, string[] filterValues, string[] orders, int limitResultSet);
        string BuildSelectSqlFor(string tableName, string[] fields, string[] filterFields, Operator[]filterOperators, string[] filterValues, string[] orders, int limitResultSet);
        string BuildSelectSqlFor(string tableName, string[] fields, string[] filterFields, string[] filterValues, string[] orders);
        string BuildSelectSqlFor(string tableName, string[] fields, string[] filterFields, Operator[] filterOperators, string[] filterValues, string[] orders);
        string BuildUpdateSqlFor(string tableName, object keyField, object keyValue, string[] fieldNames, string[] values);
        string BuildUpdateSqlFor(string tableName, object keyField, object keyValue, 
            string optimisticLockField, byte optimisticLockValue,
            string[] fieldNames, string[] values);

        string GenerateParamName(int index);
        string GenerateParamName(string paramLabel);
        string ParameterPrefix();
        string ParameterSuffix();

        /// <summary>
        /// Parametre karakterini ve top ifadelerini, db ye göre değiştirir.
        /// </summary>
        /// <param name="msSql"></param>
        /// <returns></returns>
        string Translate(string msSql);
    }
}
