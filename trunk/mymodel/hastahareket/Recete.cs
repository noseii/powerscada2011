using System;
         
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class Recete : BaseEntity
    {

        
        //TODO:raporlu reçete mevzusu öğren
        public string RaporNo { get; set; }
        
        public myenum.SosyalGuvenlikKurumTipi RaporKurumu { get; set; }
        
        public myenum.ReceteTur Tipi { get; set; }
        
        public string No { get; set; }
        
        public string Aciklama { get; set; }

        public override string ToString() { return this.No ?? ""; }
        
        public override Type GetChildType()
        {
            return typeof(Receteilac);
        }

        public override string GetFKName()
        {
            return "Recete_Id";
        }

        public Recete()
        {
            Muayene = new Muayene();
            RaporNo = string.Empty;
            No = string.Empty;
            Aciklama = string.Empty;

                

        }
    }
}
