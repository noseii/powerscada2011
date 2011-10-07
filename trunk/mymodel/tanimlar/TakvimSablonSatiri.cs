using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class TakvimSablonSatiri : Entity
    {
        
        public string Adi { get; set; }

        /// <summary>
        /// Izlem Sıra No Hangi sırada yapılacağı 
        /// </summary>
        [FieldDefinition(IsRequired = true)]
        public int IzlemSıraNo { get; set; }

        [FieldDefinition(IsRequired = true)]
        public int IlkIzlemdenSonrakiSure { get; set; }

        public TakvimSablonu TakvimSablonu { get; set; }


        public override string ToString() { return this.Adi ?? ""; }

        public override void Validate()
        {
            base.Validate();

            if (IlkIzlemdenSonrakiSure == 0)
            {
                throw new Exception("Gün değeri boş bırakılamaz");
            }

            if (IzlemSıraNo == 0)
            {
                throw new Exception("İzlem Sıra No boş bırakılamaz");
            }


          int siranosuaynikayitvarmi= 
              SharpBullet.OAL.Transaction.Instance.ExecuteScalarI("Select count(Id) from TakvimSablonSatiri where TakvimSablonu_Id=@prm0 and Aktif=1 and IzlemSıraNo=@prm1", new object[] {TakvimSablonu.Id,IzlemSıraNo });
          if (siranosuaynikayitvarmi != 0)
              throw new Exception("Bu Şablona ait aynı izlem numarasına sahip izlem var.\nİzlem sıra numaraları aynı olamaz.");
        }


        public TakvimSablonSatiri()
        {
            TakvimSablonu = new TakvimSablonu();
            Adi = string.Empty;
            IzlemSıraNo = 0;
            IlkIzlemdenSonrakiSure = 0;
        }
    }
}
