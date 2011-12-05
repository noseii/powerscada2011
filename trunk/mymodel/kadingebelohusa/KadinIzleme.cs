using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class KadinIzleme : BaseEntity
    {       
       
        public myenum.KadinKorunmaYontemi KadinKorunmaYontemi { get; set; }

        public byte CanliDogumAdedi { get; set; }
        
        public byte OluDogumAdedi { get; set; }
        
        public byte DusukDogumAdedi { get; set; }
        
        public byte KendiligindenDusukDogumAdedi { get; set; }
        
        public byte isteyerekDusukDogumAdedi { get; set; }
        
        public bool DogumKontrolDanismanligiAldi { get; set; }
        
        public bool ServikalSmear { get; set; }
        
        public bool KonjAnomali { get; set; }

        public int EvlilikYasi { get; set; }

        public int IlkGebelikYasi { get; set; }
        
        public DateTime SonrakiIzlemeTarihi { get; set; }

        public int TransferDurumu { get; set; }
        
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }

        public DateTime TransferTarihi { get; set; }

        public myenum.ApYontemiKullanmamaNedeni ApYontemiKullanmamaNedeni { get; set; }
      

        public KadinIzleme()
        {
            CanliDogumAdedi = 0;
            OluDogumAdedi = 0;
            DusukDogumAdedi = 0;
            KendiligindenDusukDogumAdedi = 0;
            isteyerekDusukDogumAdedi = 0;
            DogumKontrolDanismanligiAldi = false;
            ServikalSmear = false;
            KonjAnomali = false;
            SonrakiIzlemeTarihi = System.DateTime.Now.AddDays(30);
            EvlilikYasi = 0;
            ApYontemiKullanmamaNedeni = myenum.ApYontemiKullanmamaNedeni.Seçiniz;
        }



        public override void Validate()
        {
            base.Validate();
            if (IsAutoImport)
                return;
           if (Hasta.Cinsiyeti != myenum.Cinsiyet.Kadın)
                throw new Exception("Gebelik başlangıç kaydı yanlızca bayanlar için kullanılabilir.");
           if (!(Hasta.Yasi() > 14 && Hasta.Yasi() < 50))
               throw new Exception("Kadın izlemi yanlızca 15 49 yaş aralığındaki hastalara yapılabilir.");
           if(KadinKorunmaYontemi==0)
               throw new Exception("Kullanılan Aile planlaması Yöntemi boş bırakılamaz");

           if (EvlilikYasi == 0) 
               throw new Exception("Evlilik yaşı sıfır olamaz.");

           if (KadinKorunmaYontemi == myenum.KadinKorunmaYontemi.APYöntemiKullanmıyor)
           {
               if (ApYontemiKullanmamaNedeni == myenum.ApYontemiKullanmamaNedeni.Seçiniz)
               {
                   throw new Exception("Aile planlama Yöntemi Kullanmama Nedeni boş bırakılamaz.");
               }
           }
       
        }
    }
}
