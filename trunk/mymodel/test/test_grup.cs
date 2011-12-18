using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class test_grup : Entity
    {
        [FieldDefinition(Length =40)]
        public string grp_adi { get; set; }
        [FieldDefinition(Length =50)]
        public string grp_chz_kod { get; set; }
        [FieldDefinition(Length =50)]
        public string grp_tst_kod { get; set; }
        [FieldDefinition(Length =2)]
        public string grp_aktif { get; set; }
        [FieldDefinition(Length =20)]
        public string lab_kodu { get; set; }

        public override string ToString() { return this.grp_adi ?? ""; }
       
    }
}
