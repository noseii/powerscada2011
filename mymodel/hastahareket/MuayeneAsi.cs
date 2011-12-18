using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{

    public class MuayeneAsi : BaseEntity
    {
        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string Kodu //denormalize alan.. gönderde kullanılıyor
        {
            get
            {
                return asitanim == null ? "" : asitanim.Kodu;
            }

        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string Adi //denormalize alan.. gönderde kullanılıyor
        {
            get
            {
                return asitanim == null ? "" : asitanim.Adi;
            }

        }

        private AsiTanim asitanim;
        public AsiTanim AsiTanim 
        { 
            get 
            { 
                return asitanim == null ? asitanim = new AsiTanim() : asitanim;
            } 
            set 
            { 
                asitanim = value;
            } 
        }

        public DateTime PlanlananTarih { get; set; }

        public DateTime RevizeTarih { get; set; }

        public int TransferDurumu { get; set; }
        
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        
        public DateTime TransferTarihi { get; set; }

        /// <summary>
        /// Aşının hangi dozunun vurulacağını bu alandan anlayacağız.
        /// </summary>
        public long AsiOzellikTanimId { get; set; }

        public string AsiSiraSi {get;set;}

        public string LotNumarasi { get; set; }
        //TODO:BildirimTipi ne olduğu sorulacak.
        //public myenum.BildirimTip BildirimTipi { get; set; }
       
       
    }
}
