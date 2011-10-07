using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public class MySqlHelper : System.ISqlHelper
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
            sql += "; select LAST_INSERT_ID()";
            //Mysql için (þuan) output parametreler sadece storedproc lar için çalýþýyormuþ,
            //bu yüzden execscalar yapýp selct ile deðeri almak gerekiyor.
            //sql += "; SET ?" + identityParamName + " = LAST_INSERT_ID()";

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

            if (limitResultSet > 0)
                sql += " limit " + limitResultSet + " ";

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
                    sql += filterFields[i] + " " + OperatorString(filterOperators[i]) + " " + filterValues[i];
                }
            }

            if (!ArrayHelper.IsNull(orders))
            {
                sql += " order by ";
                sql += string.Join(", ", orders);
            }


            return sql;
        }
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
            return "?prm" + index;
        }

        public string GenerateParamName(string paramLabel)
        {
            return "?" + paramLabel;
        }

        public string ParameterPrefix()
        {
            return "?prm";
        }
        
        public string ParameterSuffix()
        {
            return "";
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
            }
            throw new ApplicationException("Unknown Operator Type");
        }

        public string Translate(string msSql)
        {
            msSql = msSql
                .Replace(" @prm", " " + ParameterPrefix())
                .Replace("(@prm", "(" + ParameterPrefix())
                .Replace(",@prm", "," + ParameterPrefix())
                .Replace("=@prm", "=" + ParameterPrefix());

            while (msSql.Contains(" top "))
            {
                int i = msSql.IndexOf(" top ");
                int j = i + " top ".Length;
                int l = j;
                while (msSql[l] != ' ') l++;

                int p = 0;
                int index = l;
                while (index < msSql.Length && p >= 0)
                {
                    if (msSql[index] == '(') p++;
                    if (msSql[index] == ')') p--;
                    index++;
                }
                
                string limitNumber = msSql.Substring(j, l - j + 1);
                msSql = msSql.Remove(i + 1, l - i);

                if (p < 0)
                    msSql = msSql.Insert(index -1 -(l-i), " limit " + limitNumber);
                else
                    msSql += " limit " + limitNumber;
            }

            return msSql;
        }

    }
}