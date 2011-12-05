using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ah30_ilacdozbirimleri : BaseEntity
    {
        public Int64 ilac_form_no { get; set; }
        [FieldDefinition(Length =120)]
        public string form_adi { get; set; }
        [FieldDefinition(Length =30)]
        public string form_kodu { get; set; }
        [FieldDefinition(Length =120)]
        public string arama_adi { get; set; }
        [FieldDefinition(Length =120)]
        public string arama_adi_eng { get; set; }
        public Int32 ilac_form_ust_no { get; set; }
        public Int64 sbrs_referans_no { get; set; }
        public Int32 surum { get; set; }


        public override string ToString() { return this.form_adi ?? ""; }
       
    }
}
