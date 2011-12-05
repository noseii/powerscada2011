using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.OAL.Metadata
{
    public abstract class Validation
    {
        public abstract void Validate(string field, object value);
    }
}
