using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ah30_malzemekodlari : BaseEntity
    {
        [FieldDefinition(Length =50)]
        public string malzeme_kodu { get; set; }
        public Int64 malzeme_kayit_id { get; set; }
        [FieldDefinition(Length =120)]
        public string malzeme_tanimi { get; set; }
        [FieldDefinition(Length =30)]
        public string malzeme_turu { get; set; }
        public Int32 surum { get; set; }
        public Int32 sbrs_referans_no { get; set; }

        public override string ToString() { return this.malzeme_kodu ?? ""; }
       
    }
}
