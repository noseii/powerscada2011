using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class BebekIzleme : BaseEntity
    {
      
        public int Agirligi { get; set; }
        
        public int Boyu { get; set; }
        
        public int BasCevresi { get; set; }
        
        public int GogusCevresi { get; set; }
        
        public int KolCevresi { get; set; }
        
        public bool FontenalAcikmi { get; set; }
        public bool FenilKetonuriIcinKanAlindimi { get; set; }
        public int DogumAgirligi { get; set; }
        public int DogumBoyu { get; set; }
        public int DogumBasCevresi { get; set; }
        public byte EkGidaBaslamaAy { get; set; }
        public bool BebekDogumKomplikasyonVarmi { get; set; }


        [FieldDefinition(Length =1000)]
        public string BuyumeGelismeNotu { get; set; }
        
        [FieldDefinition(Length =1000)]
        public string BeslenmeNotu { get; set; }
        
        [FieldDefinition(Length =510)]
        public string FizikselMuayeneNotu { get; set; }
        
        [FieldDefinition(Length =510)]
        public string GenelNotlar { get; set; }

        public BebekCocukBilgi BebekCocukBilgi { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

        public BebekIzleme()
        {
            this.Agirligi = 0;
            this.BasCevresi = 0;
            this.DogumBasCevresi = 0;
            this.DogumAgirligi = 0;
            this.EkGidaBaslamaAy = 0;
            this.Boyu = 0;
            this.DogumBoyu = 0;
        }


        public override void Validate()
        {
            base.Validate();
            if (IsAutoImport)
                return;
        }
       
    }
}
