using System;
         

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

using SharpBullet.OAL.Metadata; 

namespace mymodel
{
    public class BaseEntity : Entity
    {
        private string kodu;

        [FieldDefinition(Length = 50)]
        public string Kodu
        {
            get { return kodu; }
            set { kodu = value; }
        }

        private string adi;

        [FieldDefinition(Length = 150, IsFiltered = false)]
        public string Adi
        {
            get { return adi; }
            set { adi = value; }
        }

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

        //private Doktor doktor;
        //public Doktor Doktor
        //{
        //    get
        //    {
        //        return doktor == null ? doktor = new Doktor() : doktor;
        //    }
        //    set
        //    {
        //        doktor = value;
        //    }
        //}

        //private Doktor vekildoktor;
        //public Doktor VekilDoktor
        //{
        //    get
        //    {
        //        return vekildoktor == null ? vekildoktor = new Doktor() : vekildoktor;
        //    }
        //    set
        //    {
        //        vekildoktor = value;
        //    }
        //}

        //public DateTime IzlemTarihi { get; set; }

        public BaseEntity()
        {
            //IzlemTarihi = System.DateTime.Today;

        }

        public override void  Validate()
        {
 	        base.Validate();

            //if (Muayene != null && Muayene.Id > 0)
            //{
            //    if (Muayene.MuayeneTarihi == System.DateTime.MinValue)
            //        Muayene.Read();
            //    if (IzlemTarihi < Muayene.MuayeneTarihi)
            //        throw new Exception(" izlem tarihi Muayene Tarihinden küçük olamaz.");
            //}

            //if(IzlemTarihi>DateTime.Now)
            //    throw new Exception("İleriye Yönelik işlem yapılamaz.");

        }
    }
}
