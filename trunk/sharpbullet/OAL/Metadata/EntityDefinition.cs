using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBullet.OAL.Metadata
{
    public class EntityDefinition : DefinitionBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
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
        /// Optimistick Locking i�in kullan�lacak alan�n ismi.
        /// E�er b�yle bir alan yoksa null b�rak�lmal�.
        /// </summary>
        public string OptimisticLockField { get; set; }
    }
}
