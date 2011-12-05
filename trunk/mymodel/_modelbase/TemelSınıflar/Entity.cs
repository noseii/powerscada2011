using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBullet.ActiveRecord;
using mymodel;
using SharpBullet.OAL.Metadata;
using SharpBullet.OAL.Metadata; 

namespace mymodel
{

     [Serializable]
    public class Entity : ActiveRecordBase
    {
        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
            {
                return Id == 0; //ID'si sıfır olan birşey null sayılır, dolayısıyla gelen obje dbnull, empty string vb. şeyler için buna eşit sayılabilir.
            }
            Entity otherEntity = (Entity)obj;
            return this.Id == otherEntity.Id;
        }

        public bool     Aktif { get; set; }

        public DateTime EklemeTarihi { get; set; }
       
        [FieldDefinition(Length = 40)]
        public string EkleyenKullanici { get; set; }
      
        [FieldDefinition(Length = 40)]
        public string DegistirenKullanici { get; set; }
       

        public DateTime DegistirmeTarihi { get; set; }

        public string EkleyenMakAdres { get; set; }
        public string DegistirenMakAdres { get; set; }

        

        public Entity()
        {
            Aktif = true;
            EklemeTarihi = DateTime.Now;
            EkleyenKullanici = CurrentModel.User!=null ? CurrentModel.User.ToString() : "";
            EkleyenMakAdres = CurrentModel.GetMakAdres() ?? "";
        }

      

        public override int Update()
        {
            DegistirmeTarihi = System.DateTime.Now;
            DegistirenKullanici = CurrentModel.User.ToString();
            DegistirenMakAdres = CurrentModel.GetMakAdres();
            return base.Update();
        }

        

    }


    public class Sonuc
    {
        public bool HataVarMi { get; set; }

        public string Mesaj { get; set; }

        public object Nesne { get; set; }

        public Sonuc()
        {
            HataVarMi = false;
            Mesaj = string.Empty;
            Nesne = new object();
        }
        public Sonuc(bool hatavarmi,string mesaj)
        {
            HataVarMi = hatavarmi;
            Mesaj = mesaj;
            Nesne = new object();
        }

        public Sonuc(bool hatavarmi, string mesaj,object nesne)
        {
            HataVarMi = hatavarmi;
            Mesaj = mesaj;
            Nesne = nesne;
        }
    }
}
