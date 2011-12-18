using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using mymodel;

using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class BebekCocukBeslenme : BaseEntity
    {
       

        public BebekCocukBilgi BebekCocukBilgi { get; set; }
        
        public byte SadeceAnneSutuSuresi { get; set; }
        
        public mymodel.myenum.Aylar AnneSutuKesilmeAyi { get; set; }
        
        public mymodel.myenum.Aylar ilkGidaAyi { get; set; }

        public int TransferDurumu { get; 
            set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

       
       
    }
}
