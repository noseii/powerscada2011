using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class BebekCocukBilgi : BaseEntity
    {
        
        private GebeBaslangic gebebaslangic;
        
        public GebeBaslangic GebeBaslangic 
        { 
            get 
            {
                    return gebebaslangic == null ? gebebaslangic = new GebeBaslangic() : gebebaslangic;
            } 
            set 
            { 
                gebebaslangic = value;
            } 
        }
        
        public myenum.DogumSirasi DogumSirasi { get; set; }
        
        public byte GebelikNo { get; set; }

        public bool BebekDogumKomplikasyonVarmi { get; set; }
        
        public int Agirligi { get; set; }
        
        public int Boyu { get; set; }
        
        public int BasCevresi { get; set; }
        public int DogumAgirligi { get; set; }
        public int DogumBoyu { get; set; }
        public int DogumBasCevresi { get; set; }
        public byte EkGidaBaslamaAy { get; set; }
        
        public int GogusCevresi { get; set; }
        
        public int KolCevresi { get; set; }
        
        public string RiskliDurumlar { get; set; }
        
        public bool KanUyusmazligiVarmi { get; set; }
        
        public DateTime DogumTarihi { get; set; }
        
        public DateTime DogumTarihiBeyan { get; set; }
        
        public myenum.DogumYontemi DogumYontemi { get; set; }
        
        public myenum.Cinsiyet CinsiyetiResmi { get; set; }
        
        public myenum.Cinsiyet CinsiyetiBeyan { get; set; }
        
        public Int64 TckNoAnne { get; set; }
        
        public Int64 TckNoBaba { get; set; }
        
        public bool FenilKetonuriIcinKanAlindimi { get; set; }
        
        public bool FenilKetonuriPozitifmi { get; set; }
        
        public DateTime FenilKetonuriKanAlmaTarihi { get; set; }

        public bool DogustanSekilBozukluguVarmi { get; set; }

        [FieldDefinition(Length = 200)]
        public string DogumSekilBozukluguAciklama { get; set; }
        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

     

        public BebekCocukBilgi()
        {
            this.Agirligi = 0;
            this.BasCevresi = 0;
            this.DogumBasCevresi = 0;
            this.DogumAgirligi = 0;
            this.EkGidaBaslamaAy = 0;
            this.Boyu = 0;
            this.DogumBoyu = 0;
            this.DogumTarihi = System.DateTime.Today;
            this.DogumTarihiBeyan = System.DateTime.Today;
            this.GebelikNo = 0;
            this.FenilKetonuriKanAlmaTarihi = System.DateTime.Today;
        }


        public override void Validate()
        {
            base.Validate();
            if (IsAutoImport)
                return;

            int i = Transaction.Instance.ExecuteScalarI("Select count(Id)  from BebekCocukBilgi where Aktif=1 and Hasta_Id="+this.Hasta.Id);
            if (i == 1 && this.Id == 0)
                throw new ApplicationException("Bir kişiye ait bir tane doğum bildirimi yapılabilir.");
            if(Agirligi==0)
                throw new ApplicationException("Ağırlık alanı boş bırakılamaz.");

            if(Boyu==0)
              throw new ApplicationException("Boyu alanı boş bırakılamaz.");

            if(BasCevresi==0)
              throw new ApplicationException("Baş çevresi alanı boş bırakılamaz.");
          
        }
    }
}
