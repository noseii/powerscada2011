using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class SaglikIstirahat : BaseEntity
    {
        //TODO:SaglikIstirahat ekranı olmalı mı ? entity tanımı sorulmalı

        private Muayene muayene;
        public Muayene Muayene
        {
            get
            {
                return muayene == null ? muayene = new Muayene() : muayene;
            }
            set
            {
                muayene = value;
            }
        }

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
        public  Doktor VekilDoktor
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

        public DateTime RaporBasTarih { get; set; }
        
        public decimal GunSayisi { get; set; }

        public myenum.RaporTuru RaporTuru { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }


        public SaglikIstirahat()
        {

            GunSayisi = 0;
            
        }
       
    }
}
