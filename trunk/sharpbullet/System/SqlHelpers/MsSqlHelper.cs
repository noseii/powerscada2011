using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public class MsSqlHelper : System.ISqlHelper
    {
        public string BuildInsertSqlFor(string tableName, string[] fields, string[] values)
        {
            string sql =
                "insert into " +
                tableName +
                " (" + string.Join(", ", fields) + ") values " +
                " (" + string.Join(", ", values) + ")";

            return sql;
        }

        public string BuildInsertSqlWithIdentity(string tableName, string[] fields, string[] values, string identityParamName)
        {
            string sql = BuildInsertSqlFor(tableName, fields, values);
            sql += " SET @" + identityParamName + " = SCOPE_IDENTITY()";

            return sql;
        }

        public string BuildUpdateSqlFor(string tableName, object keyField, object keyValue, string[] fieldNames, string[] values)
        {
            string sql =
                "update " + tableName + " set ";

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (i > 0) sql += ", ";
                sql += fieldNames[i] + " = " + values[i];
            }

            sql += " where " + keyField + " = " + keyValue;

            return sql;       
        }

        public string BuildUpdateSqlFor(string tableName, object keyField, object keyValue, string optimisticLockField, byte optimisticLockValue, string[] fieldNames, string[] values)
        {
            string sql =
                "update " + tableName + " set ";

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (i > 0) sql += ", ";
                sql += fieldNames[i] + " = " + values[i];
            }
            if (!string.IsNullOrEmpty(optimisticLockField))
            {
                if (optimisticLockValue < 255)
                    sql += ", " + optimisticLockField + " = " + optimisticLockField + " +1";
                else
                    sql += ", " + optimisticLockField + " = 0";
            }

            sql += " where " + keyField + " = " + keyValue;
            if (!string.IsNullOrEmpty(optimisticLockField))
            {
                sql += " and (" + optimisticLockField + " = " + optimisticLockValue + " or "
                    + optimisticLockField + " is null)";
            }

            return sql;
        }  

        public string BuildSelectSqlFor(string tableName, string[] fields,
            string[] filterFields, string[] filterValues, string[] orders, int limitResultSet)
        {
            string sql;
            Operator[] operators = null;

            if (ArrayHelper.HasValue(filterFields))
                operators = ArrayHelper.GetArray<Operator>(Operator.Equal, filterFields.Length);

            sql = BuildSelectSqlFor(tableName, fields, filterFields, operators, filterValues, orders, limitResultSet);

            return sql;
        }

        public string BuildSelectSqlFor(string tableName, string[] fields,
            string[] filterFields, Operator[] filterOperators, string[] filterValues,
            string[] orders, int limitResultSet)
        {
            string sql = "select ";
            if (limitResultSet > 0)
                sql += "top " + limitResultSet + " ";

            

            if (ArrayHelper.IsNull(fields))
                sql += "*";
            else
                sql += string.Join(", ", fields);

            sql += " from " + tableName + " ";
            if (!ArrayHelper.IsNull(filterFields))
            {
                sql += "where ";
                for (int i = 0; i < filterFields.Length; i++)
                {
                    if (i > 0) sql += " and ";
                    sql += filterFields[i] + " " + OperatorString(filterOperators[i], filterValues[i]);
                }
            }

            if (!ArrayHelper.IsNull(orders))
            {
                sql += " order by ";
                sql += string.Join(", ", orders);
            }

            return sql;
        }

        public string BuildSelectSqlFor(string tableName, string[] fields,
            string[] filterFields, string[] filterValues, string[] orders)
        {
            string sql;
            Operator[] operators = null;

            if (ArrayHelper.HasValue(filterFields))
                operators = ArrayHelper.GetArray<Operator>(Operator.Equal, filterFields.Length);

            sql = BuildSelectSqlFor(tableName, fields, filterFields, operators, filterValues, orders);

            return sql;
        }

        public string BuildSelectSqlFor(string tableName, string[] fields,
            string[] filterFields, Operator[] filterOperators, string[] filterValues,
            string[] orders)
        {
            string sql = "select ";



            if (ArrayHelper.IsNull(fields))
                sql += "*";
            else
                sql += string.Join(", ", fields);

            sql += " from " + tableName + " ";
            if (!ArrayHelper.IsNull(filterFields))
            {
                sql += "where ";
                for (int i = 0; i < filterFields.Length; i++)
                {
                    if (i > 0) sql += " and ";
                    sql += filterFields[i] + " " + OperatorString(filterOperators[i], filterValues[i]);
                }
            }

            if (!ArrayHelper.IsNull(orders))
            {
                sql += " order by ";
                sql += string.Join(", ", orders);
            }

            return sql;
        }
        /* Is Null Deste�inden �nceki Hali
         * public string BuildSelectSqlFor(string tableName, string[] fields,
            string[] filterFields, Operator[] filterOperators, string[] filterValues, 
            string[] orders, int limitResultSet)
        {
            string sql = "select ";

            if (limitResultSet > 0)
                sql += "top " + limitResultSet + " ";

            if (ArrayHelper.IsNull(fields))
                sql += "*";
            else
                sql += string.Join(", ", fields);

            sql += " from " + tableName + " ";

            if (!ArrayHelper.IsNull(filterFields))
            {
                sql += "where ";
                for (int i = 0; i < filterFields.Length; i++)
                {
                    if (i > 0) sql += " and ";
                    sql += filterFields[i] + " " + OperatorString(filterOperators[i]) + " " + filterValues[i];
                }
            }

            if (!ArrayHelper.IsNull(orders))
            {
                sql += " order by ";
                sql += string.Join(", ", orders);
            }

            return sql;
        }*/

        /// <summary>
        /// Can not delete all rows.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyField"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public string BuildDeleteSqlFor(string tableName, string keyField, string keyValue)
        {
            string sql = "delete from ";

            sql += tableName + " where " + keyField + " = " + keyValue;

            return sql;
        }

        public string GenerateParamName(int index)
        {
            return "@prm" + index;
        }

        public string GenerateParamName(string paramLabel)
        {
            return "@" + paramLabel;
        }

        public string ParameterPrefix()
        {
            return "@prm";
        }
        
        public string ParameterSuffix()
        {
            return "";
        }

        public string OperatorString(Operator op, string value)
        {
            if (op != Operator.IsNull)
                return OperatorString(op) + " " + value;
            else
                return OperatorString(op);
        }

        public string OperatorString(Operator op)
        {
            switch (op)
            {
                case Operator.Equal:
                    return "=";
                case Operator.GreaterThan:
                    return ">";
                case Operator.LessThan:
                    return "<";
                case Operator.GreaterOrEqual:
                    return ">=";
                case Operator.LessOrEqual:
                    return "<=";
                case Operator.Like:
                    return "like";
                case Operator.IsNull:
                    return "Is Null";
            }
            throw new ApplicationException("Unknown Operator Type");
        }

        public string Translate(string msSql)
        {
            return msSql;
        }
    }
}
