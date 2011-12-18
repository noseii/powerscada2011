using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class test_hizmet : Entity
    {
        [FieldDefinition(Length =50)]
        public string test_no { get; set; }
        [FieldDefinition(Length =36)]
        public string dresmi_kodu { get; set; }

        public override string ToString() { return this.test_no ?? ""; }
       
    }
}
