using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ilce : Entity
    {
        private il il;

        [FieldDefinition(IsRequired=true)]
        public il Il
        {
            get
            {
                return il == null ? il = new il() : il;
            }
            set
            {
                il = value;
            }

        }

        [FieldDefinition(Length =100)]
        public string Adi { get; set; }

        public string Kodu { get; set; }


        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
