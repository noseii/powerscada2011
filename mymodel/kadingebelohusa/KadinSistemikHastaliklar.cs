using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class KadinSistemikHastaliklar : BaseEntity
    {
       
        [FieldDefinition(IsRequired=true)]
        private Teshis sistemikhastalik;
        public Teshis SistemikHastalik
        {
            get
            {
                return sistemikhastalik == null ? sistemikhastalik = new Teshis() : sistemikhastalik;
            }
            set
            {
                sistemikhastalik = value;
            }
        }

        [FieldDefinition(IsRequired = true)]
        public DateTime Tarih { get; set; }

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (Hasta.Cinsiyeti != myenum.Cinsiyet.Kadın)
                throw new Exception("Kayıt yanlızca bayanlar için kullanılabilir.");
            if (SistemikHastalik.Id > 0)
            {
                int hastalikdahaoncevarmi = SharpBullet.OAL.Transaction.Instance.ExecuteScalarI(
                @"Select count(Id) from KadinSistemikHastaliklar where Hasta_Id=" + Hasta.Id + " and SistemikHastalik_Id=" + SistemikHastalik.Id + " and Aktif=1");

                if (hastalikdahaoncevarmi > 0)
                    throw new Exception("Bu hastalık daha önceden atanmış");

            }
        }
    }
}
