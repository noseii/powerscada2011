using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ah30_ilactedarikcileri : BaseEntity
    {
        public Int64 ilac_tedarikci_no { get; set; }
        public Int64 sbrs_referans_no { get; set; }
        [FieldDefinition(Length =120)]
        public string tedarikci_adi { get; set; }
        public Int32 uretici { get; set; }
        public Int32 ithalatci { get; set; }
        public Int32 surum { get; set; }


        public override string ToString() { return this.tedarikci_adi ?? ""; }
       
    }
}
