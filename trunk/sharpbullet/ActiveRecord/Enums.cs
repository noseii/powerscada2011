using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.ActiveRecord
{
    public enum ActiveRecordState
    {
        New = 0,
        Modified = 1,
        UnModified = 2,
        Deleted = 3,
    }
}
