using System;
using mycommon;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class HastalikSonucu :BaseEntity
    {
        //TODO:Sorulacak kullanılmıyor gibi gözüküyor
        [FieldDefinition(Length =16)]
        public string Adi { get; set; }

        public override string ToString() { return this.Adi ?? ""; }
       
    }
}
