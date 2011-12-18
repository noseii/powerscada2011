using System;


using SharpBullet.OAL.Metadata; namespace mymodel
{
    public class DoktorSablon : Entity
    {
        //TODO:no uniq olmalı   kullanım amacı tekrar sorulmalı
        public Int16 No { get; set; }
                private Doktor doktor;public Doktor Doktor { get { return doktor == null ? doktor = new Doktor() : doktor;} set { doktor = value;} }
    }
}
