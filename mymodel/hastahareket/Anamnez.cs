using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;

using SharpBullet.OAL.Metadata; 
namespace mymodel
{
    public class Anamnez : BaseEntity
    {

        //private Hasta hasta;

        //public Hasta Hasta
        //{
        //    get
        //    {
        //        return hasta == null ? hasta = new Hasta() : hasta;
        //    }
        //    set
        //    {
        //        hasta = value;
        //    }
        //}
       
        //private Muayene muayene;

        //public Muayene Muayene
        //{
        //    get
        //    {
        //        return muayene == null ? muayene = new Muayene() : muayene;
        //    }
        //    set
        //    {
        //        muayene = value;
        //    }
        //}

        //private Takvim randevu;

        //public Takvim Randevu
        //{
        //    get
        //    {
        //        return randevu == null ? randevu = new Takvim() : randevu;
        //    }
        //    set
        //    {
        //        randevu = value;
        //    }
        //}

        [FieldDefinition(Length = 1000)]
        public string Sikayet { get; set; }
        
        [FieldDefinition(Length = 1000)]
        public string Hikaye { get; set; }
        
         [FieldDefinition(Length = 1000)]
        public string Ozgecmis { get; set; }
        
         [FieldDefinition(Length = 1000)]
        public string Soygecmis { get; set; }
        
        public byte Nabiz { get; set; }
        
        public byte KucukTansiyon { get; set; }
        
        public byte BuyukTansiyon { get; set; }
        
        public decimal/*(12,2)*/ Ates { get; set; }
        
        public byte Boyu { get; set; }
        
        public byte Agirligi { get; set; }
        
         [FieldDefinition(Length = 1000)]
         public string FizikiMuayene { get; set; }
        
         [FieldDefinition(Length = 1000)]
         public string Tedavi { get; set; }
        
         [FieldDefinition(Length = 1000)]
         public string Tetkik { get; set; }

         public int TransferDurumu { get; set; }
         [FieldDefinition(Length = 1000)]
         public string TransferSonuc { get; set; }
         public DateTime TransferTarihi { get; set; }


       
    }
}
