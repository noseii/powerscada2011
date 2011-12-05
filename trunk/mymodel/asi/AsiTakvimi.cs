//using System;
//using mycommon;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;


//using SharpBullet.OAL.Metadata; namespace mymodel
//{
//    public class AsiTakvimi : BaseEntity
//    {
//        private Hasta hasta;public Hasta Hasta { get { return hasta == null ? hasta = new Hasta() : hasta;} set { hasta = value;} }
        
//        private AsiTanim asitanim;public AsiTanim AsiTanim { get { return asitanim == null ? asitanim = new AsiTanim() : asitanim;} set { asitanim = value;} }
        
//        public AsiPeriyod AsiPeriyod { get; set; }
        
//        [FieldDefinition(Length =16)]
//        public string casi_sira { get; set; }
        
//        public DateTime dtilk_planlanan_tarih { get; set; }
        
//        public DateTime dtrevize_planlanan_tarih { get; set; }
        
//        public DateTime dtasinin_yapildigi_tarih { get; set; }
        
//        [FieldDefinition(Length =32)]
//        public string clot_numarasi { get; set; }
        
//        public bool luygulandi { get; set; }
        
//        public bool lasi_kendisinden { get; set; }
        
//        private Doktor doktor;public Doktor Doktor { get { return doktor == null ? doktor = new Doktor() : doktor;} set { doktor = value;} }
        
//        private DoktorVekalet doktorvekalet;public DoktorVekalet DoktorVekalet { get { return doktorvekalet == null ? doktorvekalet = new DoktorVekalet() : doktorvekalet;} set { doktorvekalet = value;} }
        
//        public HastaHareket HastaHareket { get; set; }
        
//        public Int32 iah30_transfer { get; set; }
        
//        [FieldDefinition(Length =1000)]
//        public string cah30_ws_result { get; set; }
        
//        public DateTime dtah30_transfer_islem_zamani { get; set; }

//        public override string ToString() { return this.casi_sira ?? ""; }
       
//    }
//}
