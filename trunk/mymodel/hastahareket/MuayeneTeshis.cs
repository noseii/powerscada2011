using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;
using SharpBullet.OAL.Metadata; 

using SharpBullet.OAL.Metadata;
using SharpBullet.OAL;
namespace mymodel
{
    public class MuayeneTeshis : BaseEntity
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


        private Teshis teshis;

        public Teshis Teshis
        {
            get
            {
                return teshis == null ? teshis = new Teshis() : teshis;
            }
            set
            {
                teshis = value;
            }
        }

        public bool Kronikmi { get; set; }

        public bool Alerjikmi { get; set; }

        //public Int32 itani_tipi { get; set; }
        //public DateTime dkayit_tarihi { get; set; }
        //public Int32 ibirincil_tani { get; set; }
        //[FieldDefinition(Length =100)]
        //public string ckesin_tani_konulan_yer { get; set; }
        //[FieldDefinition(Length =400)]
        //public string ckesin_tani_konulan_adres { get; set; }
        //public Int32 ikesin_tani_sekli { get; set; }
        //public DateTime dkesin_tani_tarihi { get; set; }
        //[FieldDefinition(Length =20)]
        //public string ckayit_saati { get; set; }

        [FieldDefinition(MappingType = FieldMappingType.No)]
        public string TeshisKodu
        {
            get
            {
                return Teshis.Id > 0 ? Teshis.Kodu : "";
            }

        }
        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }


        public override void Validate()
        {
            base.Validate();
            int kayitvarmi=0;

           
                kayitvarmi = Transaction.Instance.ExecuteNonQuery("Select count(Id) from MuayeneTeshis where hasta_Id=@prm0 and teshis_Id=@prm1 and Kronikmi=@prm2 and Alerjikmi=@prm3 and aktif=1 ",new object[]{this.Hasta.Id,this.Teshis.Id,this.Kronikmi,this.Alerjikmi});
                if (kayitvarmi > 0)
                    throw new Exception("Hastaya bir hastalığın teşhisi iki defa atanamaz");                    
         
        }
    }
}
