using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;

using SharpBullet.OAL.Metadata; namespace mymodel
{

    public class OlumBildirimi : BaseEntity
    {

        public int TransferDurumu { get; set; }
        [FieldDefinition(Length = 1000)]
        public string TransferSonuc { get; set; }
        public DateTime TransferTarihi { get; set; }

        private Teshis teshis1;
        public Teshis Teshis1 
        { 
            get 
            {
                return teshis1 == null ? teshis1 = new Teshis() : teshis1; 
            }
            set { teshis1 = value; } 
        }

        private Teshis teshis2;
        public Teshis Teshis2
        {
            get
            {
                return teshis2 == null ? teshis2 = new Teshis() : teshis2;
            }
            set { teshis2 = value; }
        }

        private Teshis teshis3;
        public Teshis Teshis3
        {
            get
            {
                return teshis3 == null ? teshis3 = new Teshis() : teshis3;
            }
            set { teshis3 = value; }
        }

       

        public DateTime OlumTarihi { get; set; }

        [FieldDefinition(Length =50)]
        public string OlumYeri { get; set; }

       

        public override void Validate()
        {
            int i = Transaction.Instance.ExecuteScalarI("Select count(Id)  from OlumBildirimi where Aktif=1 and Hasta_Id="+Hasta.Id);
            if (i == 1 && this.Id == 0)
                throw new ApplicationException("Bir kişiye ait bir tane ölüm bildirimi yapılabilir.");


            if (this.OlumTarihi == System.DateTime.MinValue)
                throw new Exception("Ölüm tarihi alanı boş bırakılamaz.");

            if (this.OlumYeri.Length==0)
                throw new Exception("Ölüm yeri alanı boş bırakılamaz.");

            if (this.Teshis1.Id == 0)
                throw new Exception("Temel Neden alanı boş bırakılamaz.");

             if (this.Teshis3.Id == 0)
                 throw new Exception("Ölüm Nedeni alanı boş bırakılamaz.");
        }
       
    }
}
