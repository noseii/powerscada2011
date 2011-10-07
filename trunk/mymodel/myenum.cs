using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpBullet.OAL.Metadata; 

namespace mymodel
{
    public class myenum
    {

        public enum EkranGetir
        {
            BirinciEkran = 1,
            İkinciEkran = 2

        }

        public enum IletisimTip
        {
            Telefon = 65,
            EPosta = 66,
            Faks = 67,
            CepTelefonu = 68,
            WebAdresi = 69

        }

        public enum AdresTip
        {
            EvAdresi = 71,
            IsAdresi = 72
        }



        public enum AramaSekli
        {
            Degistikce=1,
            GetirButonunaBasina=2

        }
        

      

        public enum GorevTuru
        {
            Sekreter=1,
            SaglikMemuru = 2,
            AileHekimi=3,
            Hemsire = 4,
            Ebe = 5,
            Admin=6
        }

       

       
        public enum MedeniHali
        {

            Belirsiz = -10,
            Belirtilmemis = 11,
            Bekar = 6,
            Evli = 7,
            Bosanmis = 8,
            Dul = 9,
            AyriYasiyor = 10
        }

       public enum TransferDurumu
        {
            //bakanlıktan gelen 0:başarılı,2:hata alındı,1:başarısız ilk deger sıfır olamayacağı için her değere 10 eklendi
            Gonderildi = 10,
            HataAlindi = 12,
            Basarisiz = 11,
            Alindi=20
        }
      

        public enum Cinsiyet
        {
            Erkek = 1,
            Kadın = 2,
            Belirsiz = 3
        }

     

        public enum Islemturu
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public enum EditMode
        {
            emYeni = 1,
            emDuzenle = 2,
            emIncele = 3,
            emKaydet = 4,
            emIptal = 5,
            emVazgec = 6,
            emBos = 7,
            emWizard = 8,
            emWizardSon = 9
        }

        public enum Aylar
        {
            Ocak = 1,
            Subat = 2,
            Mart = 3,
            Nisan = 4,
            Mayis = 5,
            Haziran = 6,
            Temmuz = 7,
            Agustos = 8,
            Eylul = 9,
            Ekim = 10,
            Kasim = 11,
            Aralik = 12
        }

        public enum ParametreTipi
        {
            Cihaz=1,
            ResimTuru=2,
            Klima=3
        }

       

      

     
        

     
      

        public enum Hak
        {
            Undefined = 0,
            IzlemAcabilmeHakki = 1,
            MuayeneAcabilmeHakki = 2

        }

        public enum Davranis
        {
            Oku=1,
            Yaz=2,
            OkuveYaz=3
        }

       
       
    }
}
