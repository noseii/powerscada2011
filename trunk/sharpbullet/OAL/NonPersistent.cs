using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.OAL
{
    /// <summary>
    /// Use to mark properties as nonpersistent.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class NonPersistentAttribute : Attribute
    {
        public NonPersistentAttribute()
        {
        }
    }
}
