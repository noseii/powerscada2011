using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ilac : Entity
    {
        [FieldDefinition(Length =220)]
        public string Adi { get; set; }
        [FieldDefinition(Length =32)]
        public string Barkod { get; set; }
        public string AzamiDozaj { get; set; }
        public myenum.ReceteTur Turu { get; set; }
        
        
        //public Int64 sbrs_ilac_no { get; set; }
        //public Int64 sbrs_referans_no { get; set; }
        //public Int32 ilac_ithalatci_no { get; set; }
        //public Int32 ilac_uretici_no { get; set; }
        //public decimal/*(12,2)*/ fiyati { get; set; }
        //[FieldDefinition(Length =16)]
        //public string fiyat_birimi { get; set; }
        //public Int32 ilac_form_no { get; set; }
        //[FieldDefinition(Length =220)]
        //public string ilac_arama_adi { get; set; }
        //[FieldDefinition(Length =16)]
        //public string dozaj_birimi { get; set; }
        //public Int32 surum { get; set; }
        //public Int64 sira_no { get; set; }


        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
