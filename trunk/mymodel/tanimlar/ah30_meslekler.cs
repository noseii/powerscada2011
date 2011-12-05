using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ah30_meslekler : BaseEntity
    {
        public Int32 meslek_kodu { get; set; }
        public Int32 sbrs_meslek_no { get; set; }
        [FieldDefinition(Length =200)]
        public string meslek_adi { get; set; }
        [FieldDefinition(Length =200)]
        public string aciklama { get; set; }
        public Int32 sbrs_referans_no { get; set; }
        public Int32 surum { get; set; }
        public Int32 sbrs_meslek_ust_no { get; set; }

        public override string ToString() { return this.meslek_adi ?? ""; }
       
    }
}
