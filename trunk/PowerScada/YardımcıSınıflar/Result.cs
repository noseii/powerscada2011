using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PowerScada
{

    public class Result
    {
        private bool isError;
        private object returnValue;
        private string message;
        private ResultCode resultCode;
        /// <summary>
        /// Result da birden fazla veriyi döndürebilmek için eklendi
        /// </summary>
        private ArrayList outParameters;
        public ArrayList OutParameters
        {
            get
            {
                if (outParameters == null) outParameters = new ArrayList();
                return outParameters;
            }
            set { outParameters = value; }
        }

        public Result() { isError = true; message = "Result is null!"; resultCode = ResultCode.GeneralFailure; }

        public Result(bool isError, string message)
        {
            this.isError = isError;
            this.returnValue = null;
            this.message = message;
            if (IsError)
            {
                this.resultCode = ResultCode.GeneralFailure;
            }
            else
                this.resultCode = ResultCode.Success;
        }

        public Result(bool isError)
        {
            this.isError = isError;
            this.returnValue = null;
            this.message = "";
            if (IsError)
            {
                this.resultCode = ResultCode.GeneralFailure;
            }
            else
                this.resultCode = ResultCode.Success;
        }

        public Result(bool isError, object returnValue, string message, ResultCode resultCode)
        {
            this.isError = isError;
            this.returnValue = returnValue;
            this.message = message;
            this.resultCode = resultCode;
        }

        public Result(bool isError, object returnValue, ArrayList outParameters, string message, ResultCode resultCode)
        {
            this.isError = isError;
            this.returnValue = returnValue;
            this.message = message;
            this.resultCode = resultCode;
            this.outParameters = outParameters;
        }

        public bool IsError { get { return isError; } set { isError = value; } }
        public object Value { get { return returnValue; } set { returnValue = value; } }
        // public object Value {get {return returnValue;}}
        public string Message { get { return message; } set { message = value; } }
        public ResultCode Code { get { return resultCode; } set { resultCode = value; } }

        public Result PrecedeMessageWith(string precedingMessage)
        {
            this.Message = precedingMessage + " Sebep : " + this.Message;
            return this;
        }
        //Burası optimize edilmeli. Eski yapıları bozmamak için uğraşmadım. ByEE
        public Result PrecedeMessageWith(string precedingMessage, string midMessage)
        {
            this.Message = precedingMessage + " " + this.Message;
            return this;
        }

        public static Result Success()
        {
            return new Result(false);
        }

        public static Result Failure(string message)
        {
            return new Result(true, message);
        }
    }

    public enum ResultCode : long
    {
        Success = 0,
        FailedForInteraction, // Rule failed because it needs more input(interaction) from client
        EntityPessimisticLocked,
        MalzemeHareketiSeviyeValidation,
        FailedForMalzemeHareketiSeviyeValidation,
        GeneralFailure,
        ObjIdShouldBeZero,
        FailedForDBConstraint,
        ObjIdShouldBePositive,
        EntityWasModifiedOrDeleted,
        OrtakInsertFailure,
        OrtakUpdateFailure,
        OrtakReadFailure,
        OrtakReadEntityNotFound,
        OrtakObjIdNull,
        OrtakInvalidated,
        OrtakHakYok,
        OrtakActionFailure,
        CariRiskLimiti,
        CariBorcRiskLimiti,
        CariAlacakRiskLimiti,
        CariRiskLimitUyari,
        CariRiskLimitiYok,
        ValidationFailure,
        TanimliStyleDosyasiYok,

        //Feedback
        BaglantiKurulmayaCalisilmadi,
        BaglantiKurulamadi,

        //Muhasebe
        Muhasebe,
        MuhasebeHesabiValidateFailure,
        MuhasebeFisiValidateFailure,
        MuhasebelestirmeFailure,

        //Satis
        Satis,
        SatisFaturasiReadFailure,
        SatisIndirimiReadFailure,
        SatisSiparisiReadFailure,

        //Satinalma
        Satinalma,
        RequiredFieldEvrakNo,
        RequiredFieldFISCari,

        //siparis

        //Ortak
        Ortak,
        GrupReadFailure,
        DayShouldNotBeSunday,
        EvrakNoSablonuBitti,
        EvrakNoSablonuTanimlanmamis,


        //Stok
        Stok,
        FailedForLevelControl, //Stok seviyeleri kontrolünden geçilmediği zaman
        BoyutBirimiKumesiReadFailure,
        BoyutBirimiReadFailure,
        MalzemeHareketiDepoValidateError,
        MalzemeFisiDepoFilterFailure,
        FailedForDBConstraintForBarkod,
        BarkodluMalzemeBulunamadi,
        KriterFiyatiOlusmadi,
        //Depo Yönetimi
        InsertedAyniKodluLokasyon,
        KritereUygunFiyatYok,
        FiyatOlusumuIcınEvrakTuruBirSablonaBaglanmamis,

        BirimSepetiReadFailure,

        HizmetHesabiReadFailure,
        OTVReadFailure,
        BirimReadFailure,
        OzellikReadFailure,
        MalzemeHesabiReadFailure,
        MalzemeHesabininMalzemeBirimleriReadFailure,
        MalzemeBirimininBarkodlariReadFaiure,
        MalzemeHesabininMalzemeAlternatifleriReadFailure,
        MalzemeHesabininMalzemeBelgeleriReadFailure,
        MalzemeHesabininMalzemeCarileriReadFailure,
        OzellikDegeriReadFailure,
        MalzemeHesabiCannotBeSelected,

        // DepoYönetimi
        LotGetirirkenHata,
        WmsIslemBitti,
        OutBoundtaKayitYok,
        WMSHataVarDevam,
        SystemHatasi,
        ToplamaFisiIptal,
        KamyonlastirmaBitti,



        //Cek-Senet
        CekSenetHesabiInsertFailure,
        CekSenetBordrosuInsertFailure,
        CekSenetHesabiUpdateFailure,
        // Kargo 
        KolibasiHesaplandi,
        ToplamDesiHesaplandi,
        DesiKgHesaplandi,
        KFaturaBakiyedenTahsilatYapildi,
        BakiyeLimitiAsildiTahsilatOlmadi,
        GondericiSubeyeyanlizcaIadeYapilabilir,
        KendineIadeYadaYonlendirmeYapilamaz,
        Fatura,
        Tesellum,
        VarolanBirAdres,
        VarisSubesiBulunamadi,
        BulunamayanInterlandlarVar,
        KargoOdemePlanı,
        KargoOdemePlanıUyarı,
        UyarHizmetVer,
        UyarHizmetVerRiskLimitiGuncelle,
        HizliDagitimExenTanimli,
        HizliDagitimBelirlenenOran,


        DepoUygIrsaliyeKayitBasarili,

        //depo yonetimi
        OnerilenCrossDockSatiriSilinmis,
        OnerilenCrossDockSatirMiktariAsıldı,
        IsEmriSonlandirmaBekleyenSatirlarVar,
        KayitBulunamadi,
        SablonDeposundaHataAlindi,
        // Inka
        PersonelCikisYapti,

        EvrakTransferEdilmis,

        DepodaTanimliOlmayanMalzeme

    }
   
}
