using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.OAL.Metadata
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class EntityDefinitionAttribute : Attribute
    {
        public EntityDefinitionAttribute()
        {
        }

        private IdMethod idMethod = IdMethod.Identity;
        public IdMethod IdMethod
        {
            get { return idMethod; }
            set { idMethod = value; }
        }

        private string stringField;
        public string StringField
        {
            get { return stringField; }
            set { stringField = value; }
        }

        /// <summary>
        /// Optimistick Locking için kullanýlacak alanýn ismi.
        /// Eðer böyle bir alan yoksa null býrakýlmalý.
        /// </summary>
        public string OptimisticLockField { get; set; }


        private bool istable;

        public bool IsTable
        {
            get { return istable; }
            set { istable = value; }
        }
    }     
}