using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class Muayene : Entity
    {

        private Hasta hasta;

        public Hasta Hasta
        {
            get
            {
                return hasta == null ? hasta = new Hasta() : hasta;
            }
            set
            {
                hasta = value;
            }
        }

        private Doktor doktor;

        public Doktor Doktor
        {
            get
            {
                return doktor == null ? doktor = new Doktor() : doktor;
            }
            set
            {
                doktor = value;
            }
        }

        private Doktor vekildoktor;
        public Doktor VekilDoktor
        {
            get
            {
                return vekildoktor == null ? vekildoktor = new Doktor() : vekildoktor;
            }
            set
            {
                vekildoktor = value;
            }
        }


        private Takvim randevu;

        public Takvim Randevu
        {
            get
            {
                return randevu == null ? randevu = new Takvim() : randevu;
            }
            set
            {
                randevu = value;
            }
        }

        public DateTime MuayeneTarihi { get; set; }

        //[FieldDefinition(IsRequired=true)]
        //public myenum.MuayeneTuru MuayeneTuru { get; set; }

        //TODO:ProtokolNo Formulune balıacak uniq bir değer olacak..
        [FieldDefinition(IsRequired = true)]
        public string ProtokolNo { get; set; }

        /// <summary>
        /// Her gün için 1 den başlayıp artarak devam eden bir alan olacak.
        /// </summary>
        ///  [FieldDefinition(IsRequired=true)]
        public Int16 SiraNo { get; set; }

        [FieldDefinition(IsRequired = true)]
        public myenum.MuayeneDurumu MuayeneDurumu { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

        public bool MuayeneKapalimi { get; set; }

        public DateTime MuayeneKapamaTarihi { get; set; }

        public static void UpdateMuayenedurumu(long muayeneid, myenum.MuayeneDurumu durum)
        {
            Muayene muayene = Persistence.Read<Muayene>(muayeneid);
            muayene.MuayeneDurumu = durum;
            muayene.Update();
        }
    }
}
