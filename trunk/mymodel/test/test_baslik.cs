using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class test_baslik : Entity
    {
        [FieldDefinition(Length =50)]
        public string tst_kod { get; set; }
        [FieldDefinition(Length =60)]
        public string tst_adi { get; set; }
        [FieldDefinition(Length =20)]
        public string lab_kodu { get; set; }
        [FieldDefinition(Length =12)]
        public string tst_bakanlik_kodu { get; set; }
        [FieldDefinition(Length =20)]
        public string tst_chz_kod { get; set; }

        public override string ToString() { return this.tst_adi ?? ""; }
       
    }
}
