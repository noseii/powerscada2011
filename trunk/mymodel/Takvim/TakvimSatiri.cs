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
    public class TakvimSatiri : Entity
    {

        private Takvim takvim;

        public Takvim Takvim
        {
            get
            {
                return takvim == null ? takvim = new Takvim() : takvim;
            }
            set
            { takvim = value; }
        }
        
        public DateTime PlanlananTarih { get; set; }

        private Doktor doktor;

        public Doktor Doktor
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

        public myenum.IslemTuru IslemTuru { get; set; }

        public myenum.IzlemTuru Izlemturu { get; set; }

        public myenum.TakvimSatirDurumu Durum { get; set; }

        public DateTime YapildigiTarih { get; set; }

        private AsiTanim asi;

        public AsiTanim Asi
        {
            get
            {
                return asi == null ? asi = new AsiTanim() : asi;
            }
            set
            { asi = value; }
        }

        /// <summary>
        /// Aşının hangi dozunun vurulacağını bu alandan anlayacağız.
        /// </summary>
        public long AsiOzellikTanimId { get; set; }

        public string Aciklama { get; set; }

        public TakvimSatiri()
        {

            IslemTuru = myenum.IslemTuru.Muayene;
            Durum = myenum.TakvimSatirDurumu.Yapılmadı;
            Doktor = new Doktor();
            Hasta = new Hasta();
            PlanlananTarih = DateTime.Today;
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
        public string AsiAdi
        {
            get
            {
                return this.Asi.Id > 0 ? this.Asi.Adi : "";
            }

        }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string AsiKodu
        {
            get
            {
                return this.Asi.Id > 0 ? this.Asi.Kodu : "";
            }

        }

        public override void Validate()
        {
            base.Validate();
         
        }




    }
}