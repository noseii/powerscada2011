using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata; 

namespace mymodel
{
    public class ButKodlari : BaseEntity
    {
        public Int32 but_tur_no { get; set; }

        public Int32 sbrs_but_no { get; set; }

        public Int32 sbrs_referans_no { get; set; }

        [FieldDefinition(Length =30)]
        public string but_kodu { get; set; }

        [FieldDefinition(Length =120)]
        public string but_adi { get; set; }

        public decimal/*(12,2)*/ ucreti { get; set; }

        public Int32 puani { get; set; }

        [FieldDefinition(Length =120)]
        public string aciklama { get; set; }

        public Int32 surum { get; set; }
        
        public Int32 sbrs_but_ust_no { get; set; }

        public override string ToString() { return this.but_kodu ?? ""; }
       
    }
}
