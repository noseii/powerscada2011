using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.DAL
{
    public interface IConnectionHandle
    {
        ITransactionHandle TransactionHandle { get; }	
    }
}
