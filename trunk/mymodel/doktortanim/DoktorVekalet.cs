using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL;

using SharpBullet.OAL.Metadata;
namespace mymodel
{
    public class DoktorVekalet : Entity
    {
         [FieldDefinition(IsRequired = true)]
        public Doktor VerenDoktor { get; set; }

        public Doktor AlanDoktor { get; set; }

        public DateTime BaslangicTarihi { get; set; }

        public DateTime BitisTarihi { get; set; }

        public myenum.VekaletNedeni VekaletNedeni { get; set; }

        public string Aciklama { get; set; }



        public DoktorVekalet()
        {
            VerenDoktor = new Doktor();
            AlanDoktor = new Doktor();
            BaslangicTarihi = System.DateTime.Today;
            BitisTarihi = System.DateTime.Today.AddDays(1);
            VekaletNedeni = myenum.VekaletNedeni.Izin;
            Aciklama = string.Empty;
        }


        public override void Validate()
        {
            base.Validate();


            if (VerenDoktor.Id == AlanDoktor.Id)
                throw new Exception("Vekaleti veren doktor ile alan doktor aynı olamaz");

            if (BaslangicTarihi == System.DateTime.MinValue || BitisTarihi == System.DateTime.MinValue)
                throw new Exception("Başlangıç tarihi ya da bitiş tarihi boş bırakılamaz.");

            if (VekaletNedeni == 0)
                throw new Exception("Vekalet nedeni boş bırakılamaz");

            int count = 0; 
            count = Transaction.Instance.ExecuteScalarI(@"Select count(Id) from DoktorVekalet Where VerenDoktor_Id=@prm0 
                and Aktif=1 and BaslangicTarihi<=@prm1 and BitisTarihi>=@prm2 ", new object[] { AlanDoktor.Id, BitisTarihi, BaslangicTarihi });
            if (count > 0)
                throw new Exception("Vekaleti alacak doktor yazılı tarihler arasında izinlidir.");



            //if ((Convert.ToDateTime(AracData.Rows[i]["HakEdisBitTarihi"]) >= ultraDateTimeEditorbastar.DateTime) && (ultraDateTimeEditorbittar.DateTime >= Convert.ToDateTime(AracData.Rows[i]["HakEdisBasTarihi"])))


            if (Id == 0)
            {
                count = 0;
                count = Transaction.Instance.ExecuteScalarI(@"Select count(Id) from DoktorVekalet Where VerenDoktor_Id=@prm0 
                and Aktif=1 and BaslangicTarihi=@prm1 and BitisTarihi=@prm2 ", new object[] { VerenDoktor.Id, BaslangicTarihi, BitisTarihi });
                if (count > 0)
                    throw new Exception("Bu doktor daha önce bu tarihler arasında başka bir doktora vekalet vermiş.");
            }

        }
    }
}