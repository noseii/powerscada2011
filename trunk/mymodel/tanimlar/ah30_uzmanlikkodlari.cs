using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ah30_uzmanlikkodlari : BaseEntity
    {
        public Int32 uzmanlik_kodu { get; set; }
        public Int32 uzmanlik_no { get; set; }
        public Int32 sbrs_referans_no { get; set; }
        [FieldDefinition(Length =120)]
        public string uzmanlik_adi { get; set; }
        public Int32 surum { get; set; }
        public Int32 uzmanlik_ust_no { get; set; }

        public override string ToString() { return this.uzmanlik_adi ?? ""; }
       
    }
}
