using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace SharpBullet.DAL
{
    class TransactionHandle : ITransactionHandle
    {
        public TransactionHandle(DbTransaction transaction)
        {
            this.transaction = transaction;
        }

        private DbTransaction transaction;

        public DbTransaction Transaction
        {
            get { return transaction; }
            set { transaction = value; }
        }

        internal void RollBack()
        {
            transaction.Rollback();
        }

        internal void Commit()
        {
            transaction.Commit();
        }
    }
}
