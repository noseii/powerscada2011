using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class ProgramDegisiklikleri : Entity
    {
        [FieldDefinition(Length =500)]
        public string Konu { get; set; }
        [FieldDefinition(Length =20)]
        public string Tur { get; set; }
        [FieldDefinition(Length =7999)]
        public string Aciklama { get; set; }
        public Int32 Deger { get; set; }
        public DateTime Tarih { get; set; }
        [FieldDefinition(Length = 500)]
        public string TalepAciklama { get; set; }
        [FieldDefinition(Length = 100)]
        public string Versiyon { get; set; }

        public override string ToString() { return this.Konu ?? ""; }
       
    }
}
