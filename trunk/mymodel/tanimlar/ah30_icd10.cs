using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ah30_icd10 : BaseEntity
    {
        public Int32 icd_no { get; set; }
        public Int32 sbrs_referans_no { get; set; }
        [FieldDefinition(Length =210)]
        public string icd_adi { get; set; }
        [FieldDefinition(Length =210)]
        public string icd_adi_eng { get; set; }
        [FieldDefinition(Length =20)]
        public string icd_kodu { get; set; }
        public Int32 seviye { get; set; }
        public Int32 surum { get; set; }
        public Int32 bildirimi_zorunlu { get; set; }
        public Int32 olum_nedeni { get; set; }
        [FieldDefinition(Length =20)]
        public string icd_ust_kodu { get; set; }
        public Int32 icd_ust_no { get; set; }
        public Int32 anne_olumu { get; set; }


        public override string ToString() { return this.icd_adi ?? ""; }
       
    }
}
