using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBullet.OAL;
using SharpBullet;
using SharpBullet.ActiveRecord;
using SharpBullet.OAL.Metadata;
using mymodel;
namespace mymodel
{
    public class Takvim : Entity
    {
        public short SiraNo { get; set; }

       
        public DateTime BasTarih { get; set; }

       
        private Doktor doktor;
       
        public  Doktor Doktor
        {
            get
            {
                return doktor == null ? doktor = new Doktor() : doktor;
            }
            set
            { doktor = value; }
        }

        private Hasta hasta;
        public Hasta Hasta
        {
            get
            {
                return hasta == null ? hasta = new Hasta() : hasta;
            }
            set
            { hasta = value; }
        }

     
        public myenum.RandevuDurumu RandevuDurumu { get; set; }

        public string Aciklama { get; set; }

        //public string Lokasyon { get; set; }

        public string Konu { get; set; }

       
        public string Saat { get; set; }


        /// <summary>
        /// Aşının hangi dozunun vurulacağını bu alandan anlayacağız.
        /// </summary>
        //public long AsiOzellikTanimId { get; set; }
          [FieldDefinition(MappingType = FieldMappingType.No)]
        public List<TakvimSatiri> TakvimSatirlari { get; set; }

        public Takvim()
        {

            //IslemTuru = myenum.IslemTuru.Muayene;
            RandevuDurumu = myenum.RandevuDurumu.Verildi;
            Doktor = new Doktor();
            Hasta = new Hasta();
            BasTarih = DateTime.Now;
           
            TakvimSatirlari = new List<TakvimSatiri>();
            this.Konu = string.Empty;
            this.Aciklama = string.Empty;
        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public long TakvimId
        {
            get
            {
                return this.Id;
            }

        }

   

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public bool ValidateYapma { get; set; }
       

        public override void Validate()
        {
            base.Validate();

            if (!ValidateYapma)
            {
                if (TakvimSatirlari.Count == 0)
                {
                    throw new Exception("Satır olmadan Kayıt yapılamaz");
                }

                if (this.RandevuDurumu != myenum.RandevuDurumu.Verildi)
                {
                    throw new Exception("Sadece durumu verildi olan randevularda düzenleme yapılabilir.");
                }
            }
            
           
        }

        public static void UpdateTakvimDurumu(long takvimId, myenum.RandevuDurumu durum,bool asiyapildi,DateTime asininyapildigitarih)
        {
            Takvim takvim = Persistence.Read<Takvim>(takvimId);
            takvim.RandevuDurumu = durum;
            if (asiyapildi)
            {
                //if (takvim.Asi.Id > 0)
                //    takvim.AsiYapildi = true;
                //takvim.AsininYapildigiTarih = asininyapildigitarih;
            }
            takvim.ValidateYapma = true;
            takvim.Update();
        }
     
        public static void UpdateTakvimDurumu(long takvimId, myenum.RandevuDurumu durum)
        {
            Takvim takvim = Persistence.Read<Takvim>(takvimId);
            takvim.RandevuDurumu = durum;
            takvim.ValidateYapma = true;
            takvim.Update();
          
        }
    }

    public class Randevu
    {
        public Doktor Doktor { get; set; }

        public DateTime BasTarih { get; set; }

        public short SiraNo { get; set; }

        public TimeSpan Saat { get; set; }

        public myenum.RandevuDurumu RandevuDurumu { get; set; }

        public Randevu()
        {
            BasTarih = new DateTime();
            SiraNo = 0;
            Saat = new TimeSpan();
            Doktor = new Doktor();
            RandevuDurumu = myenum.RandevuDurumu.Verildi;
        }
    }


}


   


