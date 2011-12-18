using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace SharpBullet.DAL
{
    class ConnectionHandle : IConnectionHandle
    {
        public ConnectionHandle(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;
        }

        private DbConnection connection;
        private DbProviderFactory factory;

        private TransactionHandle transactionHandle;

        public ITransactionHandle TransactionHandle
        {
            get { return transactionHandle; }
        }

        public DbConnection Connection
        {
            get { return connection; }
        }

        public DbProviderFactory Factory
        {
            get { return factory; }
        }

        public ITransactionHandle BeginTransaction()
        {
            DbTransaction transaction;

            transaction = connection.BeginTransaction();
            transactionHandle = new TransactionHandle(transaction);

            return transactionHandle;
        }

        internal void Open()
        {
            connection.Open();
        }

        internal void Close()
        {
            connection.Close();
        }
    }
}
