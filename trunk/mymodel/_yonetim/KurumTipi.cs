using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class KurumTipi : Entity
    {
        [FieldDefinition(Length =100)]
        public string Adi { get; set; }
        [FieldDefinition(Length =20)]
        public string KisaAdi { get; set; }
        [FieldDefinition(Length =36)]
        public string MuhasebeGelirHesapKodu { get; set; }
        public Int32 Provizyon { get; set; }
        public Int32 Status { get; set; }
        public Int32 HataDurumu { get; set; }
        [FieldDefinition(Length =60)]
        public string FaturaBasligi { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
