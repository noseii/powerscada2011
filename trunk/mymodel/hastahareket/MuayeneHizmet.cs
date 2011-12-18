using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;

using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class MuayeneHizmet : BaseEntity
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


        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string HizmetKodu
        {
            get
            {
                return hizmet == null ? "" : hizmet.Kodu;
            }

        }

        private Hizmet hizmet;

        public Hizmet Hizmet
        {
            get
            {
                return hizmet == null ? hizmet = new Hizmet() : hizmet;
            }
            set
            {
                hizmet = value;
            }
        }


       

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string HizmetTuru        {
            get
            {
                return hizmet == null ? "" : hizmet.HizmetTur.Adi;
            }

        }
        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

    }
}
