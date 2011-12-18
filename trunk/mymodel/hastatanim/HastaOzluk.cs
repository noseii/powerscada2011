using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class HastaOzluk : Entity
    {
        //TODO: henüz kullanımı düşünülmedi sorulabilir
        private Hasta hasta;public Hasta Hasta { get { return hasta == null ? hasta = new Hasta() : hasta;} set { hasta = value;} }
        //public Kurum Kurum { get; set; }
        private Doktor doktor;public Doktor Doktor { get { return doktor == null ? doktor = new Doktor() : doktor;} set { doktor = value;} }
        public Int32 imeslek { get; set; }
        public Int32 iis_durumu { get; set; }
        public Int32 isosyal_guvence_durum { get; set; }
        public Int32 iogrenim_durum { get; set; }
        public Int32 iadres_tip { get; set; }
        [FieldDefinition(Length =30)] 
        public string cadres_kodu { get; set; }
        public Int32 igezici_hizmet_durum { get; set; }
        public Int32 iozurdurum { get; set; }
        public Int32 isigarakullanim { get; set; }
        public Int32 ialkolkullanim { get; set; }
        public Int32 imaddekullanim { get; set; }
        public Int32 iameliyat_gecmis { get; set; }
        public Int32 iyaralanma_gecmis { get; set; }
        public Int32 iah30_transfer { get; set; }

       
    }
}
