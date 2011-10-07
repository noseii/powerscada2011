using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SharpBullet.OAL.Metadata;

using mymodel;

namespace mymodel
{
    public class RolEkranHakki : Entity
    {
        public bool Izle{ get; set; }

        public bool Degistir{ get; set; }

        public bool Ekle{ get; set; }

        public bool Sil{ get; set; }

        public myenum.GorevTuru  Rol { get; set; }

        [FieldDefinition(Length = 100)]
        public string EkranAdi{ get; set; }

        public override string ToString() { return this.Rol.ToString() ?? ""; }

    }
}
