using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpBullet.ActiveRecord;
using SharpBullet.DAL;
using SharpBullet.UI;
using SharpBullet.OAL;
using mymodel;
using System.IO;
using System.Data;
using System.Windows.Forms;
         
namespace PowerScada
{
    public class IlkKurulum
    {
     
        public static string createdb(string path)
        {
            return sqlclientUtil.RunSqlStr(@"
                    CREATE DATABASE [PowerScada] ON  PRIMARY 
                    ( NAME = N'PowerScada', FILENAME = N'" +path+@"\PowerScada.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
                     LOG ON 
                    ( NAME = N'AHBS2010_log', FILENAME = N'" + path + @"\AHBS2010_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
                ",Current.masterconstr);
        }

        public static string attachdb(string path)
        {
            return sqlclientUtil.RunSqlStr(@"
                    CREATE DATABASE [PowerScada] ON 
                    ( FILENAME = N'" + path + @"\PowerScada.mdf' ),
                    ( FILENAME = N'" + path + @"\AHBS2010_log.ldf' )
                     FOR ATTACH  ", Current.masterconstr);
        }

        public static string createsqluser()
        {
            string result = "ok";
            try
            {
                result = sqlclientUtil.RunSqlStr(@"CREATE LOGIN [ahbspass2010] WITH PASSWORD=N'1122', DEFAULT_DATABASE=[PowerScada], DEFAULT_LANGUAGE=[Türkçe], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF", Current.masterconstr);
            }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'bulkadmin'", Current.masterconstr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'dbcreator'", Current.masterconstr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'diskadmin'", Current.masterconstr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'processadmin'", Current.masterconstr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'securityadmin'", Current.masterconstr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'serveradmin'", Current.masterconstr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'setupadmin'", Current.masterconstr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC master..sp_addsrvrolemember @loginame = N'ahbspass2010', @rolename = N'sysadmin'", Current.masterconstr); }
            catch { }

            try
            {
                result = sqlclientUtil.RunSqlStr(@"CREATE USER [ahbspass2010] FOR LOGIN [ahbspass2010]", Current.constr);
            }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC sp_addrolemember N'db_accessadmin', N'ahbspass2010'", Current.constr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC sp_addrolemember N'db_backupoperator', N'ahbspass2010'", Current.constr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC sp_addrolemember N'db_datareader', N'ahbspass2010'", Current.constr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC sp_addrolemember N'db_datawriter', N'ahbspass2010'", Current.constr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC sp_addrolemember N'db_ddladmin', N'ahbspass2010'", Current.constr); }
            catch { }
            try { result = sqlclientUtil.RunSqlStr(@"EXEC sp_addrolemember N'db_securityadmin', N'ahbspass2010'", Current.constr); }
            catch { }

            return result;
        }

        public static void varsayilandoktor()
        {
            try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"INSERT INTO Doktor
                       ([Adi]
                       ,[Soyadi]
                       ,[TckNo]
                       ,[Diplomano]
                       ,[WebServisSifre]
                       ,[WebServisKullaniciNo]
                       ,[Aktif]
                       ,[Id]
                       )
                 VALUES
                       (
            			'Adı giriniz',
            			'Soyadı giriniz',
            			'11111111111',
            			'11111111111',
            			'1111',
            			'11111111111',
            			1,1
                       )"
                           );
            }
            catch { }
        }
        public static void ProgramAyarlari()
        {
            try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"INSERT INTO ProgramAyarlari
                       (ID
                        ,Adi  
                        ,MesaiBaslangicSaati 
                        ,RandevuAraligi 
                        ,DoktorPanelAyar 
                        ,AramaYontemiEntermi 
                        ,GridGorunumuStandartmi 
                        ,LabLocalmi
                        ,Aktif 
                       )
                 VALUES
                       (1,
            			'Genel Program Ayarlari',
            			'09:00:00',
            			'00:10:00',
            			1,
            			0,
            			1,
                        1,
            			1
                       )"
                           );
            }
            catch { }
        }
        public static void varsayilankullanici()
        { 
           

            try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"INSERT INTO Kullanici
                       ([Login]
                       ,[Sifre]
                       ,[Adi]
                       ,[SoyAdi]
                       ,[DogumTarihi]
                       ,[EvTel]
                       ,[Gsm]
                       ,[EvAdresi]
                       ,[email]
                       ,[GorevTuru]
                       ,[Tipi]
                       ,[TckNo]
                       ,[Bolumu]
                       ,[Aktif]
                       ,[EklemeTarihi]
                       ,[DegistirmeTarihi]
                       ,[EkleyenKullanici]
                       ,[DegistirenKullanici]
                       ,[EkleyenMakAdres]
                       ,[DegistirenMakAdres]
                       ,[Id]
                       ,[RowVersion]
                        ,[IsAdmin]
                        ,[IsDoktorDegistirebilir]

            )
                 VALUES
                       ('admin'
                       ,'0cc175b9c0f1b6a831c399e269772661'
                       ,'admin'
                       ,'admin'
                       ,''
                       ,''
                       ,''
                       ,''
                       ,''
                       ,'Admin'
                       ,0
                       ,0
                       ,''
                       ,1
                       ," + Current.getdate() + @"
                       ,null
                       ,null
                       ,null
                       ,''
                       ,''
                       ,1
                       ,1,1,1)");

            }
            catch { }
        }

        public static void setdb()
        {
            try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"
create FUNCTION [dbo].[FN_GETILAC](@MUAYENEID bigint) 
RETURNS varchar(max) AS
BEGIN
DECLARE @result varchar(max)
Set @result=''
		SELECT 
				@result = coalesce(@result + '','') +
				(I.Adi+' '+case when len(RI.ilacDozAciklama)>0 then RI.ilacDozAciklama else RI.KullanimPeriyot end)+' , ' 
		FROM Receteilac RI 
		INNER JOIN iLAC I on I.Id=RI.ILAC_ID AND I.Aktif=1
		WHERE
			RI.MuayeneId=@MUAYENEID AND RI.Aktif=1 
SET @result=SUBSTRING(@result,0,LEN(@result))
RETURN  @result
END");

            }
            catch { } try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"
create FUNCTION [dbo].[FN_GETRAPOR](@MUAYENEID bigint) 
RETURNS varchar(max) AS
BEGIN
DECLARE @result varchar(max)
Set @result=''
		SELECT 
				@result = coalesce(@result + '','') +case when isnull(SI.GunSayisi,0)>0 
				then STR((SI.GunSayisi))+' GÜN '+SI.RaporTuru+' , '
				else SI.RaporTuru+',' end
		FROM SaglikIstirahat SI 
		WHERE
			SI.Muayene_Id=@MUAYENEID AND SI.Aktif=1 
SET @result=SUBSTRING(@result,0,LEN(@result))
RETURN  @result
END");

            }
            catch { } try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"
create FUNCTION [dbo].[FN_GETSEVK](@MUAYENEID bigint) 
RETURNS varchar(max) AS
BEGIN

DECLARE @result varchar(max)

Set @result=''

		SELECT 
				@result = coalesce(@result + '','') +
				isnull((SELECT Adi FROM SevkKurum SK WHERE SK.ID=MS.SevkKurum_Id),0)+' , '+
				isnull((SELECT Adi FROM SevkBolum SB WHERE SB.ID=MS.SevkBolum_Id),0)+' , '
				 
		FROM dbo.MuayeneSevk MS
		--INNER JOIN SevkKurum SK ON SK.Id=MS.SevkKurum_Id 
		--INNER JOIN SevkBolum SB ON SB.Id=MS.SevkBolum_Id 
		WHERE
			MS.Muayene_Id=@MUAYENEID AND MS.Aktif=1 
 
SET @result=SUBSTRING(@result,0,LEN(@result))



RETURN  @result

END");

            }
            catch { } try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"create FUNCTION [dbo].[FN_GETTANI](@MUAYENEID bigint) 
RETURNS varchar(max) AS
BEGIN

DECLARE @result varchar(max)

Set @result=''

		SELECT 
				@result = coalesce(@result + '','') +
				T.Adi+' , '
		FROM MuayeneTeshis MT 
		INNER JOIN Teshis T on T.Id=MT.teshis_Id AND T.Aktif=1
		WHERE
			MT.Muayene_Id=@MUAYENEID AND MT.Aktif=1 

RETURN SUBSTRING(@result,0,LEN(@result)) 

END");

            }
            catch { } try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"create function [dbo].[fn_MuayeneSonucu](@MuayeneID bigint) returns varchar(4000) as
begin
    
    DECLARE @COUNT int
    DECLARE @Sonuc varchar(max)
    SET @COUNT=0;
	SET @Sonuc='';
	
	Select @COUNT=count(Id) from MuayeneTeshis where MuayeneTeshis.muayene_Id=@MuayeneID and Aktif=1
	if(@COUNT>0)
		Set	@Sonuc='Tanı,'
	
	SET @COUNT=0;	
		
	Select @COUNT=count(Id) from Receteilac where Receteilac.muayeneId=@MuayeneID and Aktif=1
	if(@COUNT>0)
	  set @Sonuc=@Sonuc+'İlaç,'
	  
	SET @COUNT=0;	
		
	Select @COUNT=count(Id) from MuayeneSevk where MuayeneSevk.muayene_Id=@MuayeneID and Aktif=1
	if(@COUNT>0)
	  set @Sonuc=@Sonuc+'Sevk,'
	  
	Select @COUNT=count(Id) from SaglikIstirahat where SaglikIstirahat.muayene_Id=@MuayeneID and Aktif=1
	if(@COUNT>0)
	  set @Sonuc=@Sonuc+'Rapor,'
    
    
  
    return SUBSTRING(@Sonuc,0,LEN(@Sonuc))
end");
            }
            catch { } try
            {

                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"create function [dbo].[fn_Tasiyicimi](@InID int,@tablo varchar(50)) returns bit as
begin
    declare    
    @Container bit, @adet int
    set @adet=0
    if (@tablo='Teshis')
        select @adet=count(UstTeshis_Id) from Teshis where Aktif=1 and UstTeshis_Id=@InID
        
    if (@tablo='Hizmet')
        select @adet=count(UstHizmet_Id) from Hizmet where Aktif=1 and UstHizmet_Id=@InID

    if (@adet>0) set @container=1
    else set @container=0

    return @Container
end");

            }
            catch { } 
            
            try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"drop  function [dbo].[fn_YasGrubu]");
             }
            catch { } 
            try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"
create    function [dbo].[fn_YasGrubu](@Yas decimal,@RaporTuru varchar(15)) returns varchar(150) as
begin
    declare    
    @YasGrubu   varchar(150)
   set @YasGrubu=''
	
	   if(@RaporTuru='Form17')
	   begin
				   if(@Yas=0)
				   begin
						set @YasGrubu='0 - 11 Ay'
				   end
				   else
					if(@Yas>0 and @Yas<5)
					 begin
						set @YasGrubu='1 - 4 Yaş'
					 end
					 else
					 if(@Yas>4 and @Yas<10)
					 begin
						set @YasGrubu='5 - 9 Yaş'
					 end
					 else
					 if(@Yas>9 and @Yas<15)
					 begin
						set @YasGrubu='10 - 14 Yaş'
					 end
					 else
					 if(@Yas>14 and @Yas<20)
					 begin
						set @YasGrubu='15 - 19 Yaş'
					 end
					  else
					 if(@Yas>19 and @Yas<30)
					 begin
						set @YasGrubu='20 - 29 Yaş'
					 end
					  else
					 if(@Yas>29 and @Yas<45)
					 begin
						set @YasGrubu='30 - 44 Yaş'
					 end
					   else
					 if(@Yas>44 and @Yas<65)
					 begin
						set @YasGrubu='45 - 64 Yaş'
					 end
					  else
					 if(@Yas>64 )
					 begin
						set @YasGrubu='65 + Yaş'
					 end
        
		end
		else
		if(@RaporTuru='Form18A')
	    begin
			 if(@Yas=0)
				   begin
						set @YasGrubu='0 - 11 Ay'
				   end
				   else
					if(@Yas>0 and @Yas<5)
					 begin
						set @YasGrubu='1 - 4 Yaş'
					 end
					 else
					 if(@Yas>4 and @Yas<10)
					 begin
						set @YasGrubu='5 - 9 Yaş'
					 end
					 else
					 if(@Yas>9 and @Yas<15)
					 begin
						set @YasGrubu='10 - 14 Yaş'
					 end
					 else
					 if(@Yas>14 and @Yas<25)
					 begin
						set @YasGrubu='15 - 24 Yaş'
					 end
					  else
					 if(@Yas>24 and @Yas<45)
					 begin
						set @YasGrubu='25 - 44 Yaş'
					 end
					  else
					 if(@Yas>44 and @Yas<65)
					 begin
						set @YasGrubu='45 - 64 Yaş'
					 end
			         else
					 if(@Yas>64 )
					 begin
						set @YasGrubu='65 + Yaş'
					 end
			
		end
    return @YasGrubu
end
");

            }
            catch { } try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"create FUNCTION [dbo].[iszero](@Value bigint,@Replace bigint)
RETURNS int
AS
BEGIN
DECLARE @Result bigint
IF @Value = 0
SET @Result = @Replace
ELSE
SET @Result = @Value
RETURN @Result
END
");

            }
            catch { } try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@" CREATE FUNCTION [dbo].[FN_GETISLEMTURU](@TAKVIMID bigint) 
RETURNS varchar(max) AS
BEGIN

DECLARE @result varchar(max)

Set @result=''

		SELECT 
				@result = coalesce(@result + '','') +
				CASE WHEN TS.IzlemTuru='0' THEN 'Muayene'+' , ' ELSE TS.IzlemTuru+' , ' END
		FROM TakvimSatiri TS 
		WHERE
			TS.Takvim_Id=@TAKVIMID 
		Group by IzlemTuru

RETURN SUBSTRING(@result,0,LEN(@result)) 

END ");

            }
            catch { }
            try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@" drop procedure SpForm17Raporu");
            }
            catch { }

try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@" 
                              create procedure [dbo].[SpForm17Raporu] (@BasTarih datetime,@BitTarih datetime,@doktor bigint) as
begin
set dateformat ymd;

Select
dbo.fn_YasGrubu((DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365,'Form17') as YasGrubu,
Case h.Cinsiyeti
when 'Erkek' then 'E'
when 'Kadın' then 'K' else '' end as Cinsiyeti,
0  as 'AkutKanlıİshalvaka',
sum(case when kesinteshis.Kodu in ('A09') then 1 else 0 end) as 'AkutKanlıİshalKesin',
sum(case when olumteshisi.Kodu in ('A09') then 1 else 0 end) as 'AkutKanlıİshalOlum',
0  as 'Brucellavaka',
sum(case when kesinteshis.Kodu in  ('A23', 'A23.0', 'A23.1', 'A23.2', 'A23.3', 'A23.3', 'A23.8', 'A23.9') then 1 else 0 end) as 'BrucellaKesin',
sum(case when olumteshisi.Kodu in  ('A23', 'A23.0', 'A23.1', 'A23.2', 'A23.3', 'A23.3', 'A23.8', 'A23.9') then 1 else 0 end) as 'BrucellaOlum',
0  as 'Boğmacavaka',
sum(case when kesinteshis.Kodu in   ('A37', 'A37.0', 'A37.1',  'A37.8', 'A37.9' ) then 1 else 0 end) as 'BoğmacaKesin',
sum(case when olumteshisi.Kodu in   ('A37', 'A37.0', 'A37.1',  'A37.8', 'A37.9' ) then 1 else 0 end) as 'BoğmacaOlum',
0  as 'Difterivaka',
sum(case when kesinteshis.Kodu in   ('A36', 'A36.0', 'A36.1', 'A36.2', 'A36.3', 'A36.8', 'A36.9' ) then 1 else 0 end) as 'DifteriKesin',
sum(case when olumteshisi.Kodu in   ('A36', 'A36.0', 'A36.1', 'A36.2', 'A36.3', 'A36.8', 'A36.9' ) then 1 else 0 end) as 'DifteriOlum',
0  as 'Gonorevaka',
sum(case when kesinteshis.Kodu in ('A54.0') then 1 else 0 end) as 'GonoreKesin',
sum(case when olumteshisi.Kodu in ('A54.0') then 1 else 0 end) as 'GonoreOlum',
0  as 'HepatitAvaka',
sum(case when kesinteshis.Kodu in ('B15', 'B15.0', 'B15.9') then 1 else 0 end) as 'HepatitAKesin',
sum(case when olumteshisi.Kodu in ('B15', 'B15.0', 'B15.9') then 1 else 0 end) as 'HepatitAOlum',
0  as 'HepatitBvaka',
sum(case when kesinteshis.Kodu in  ('B16', 'B16.0', 'B16.1', 'B16.2', 'B16.9') then 1 else 0 end) as 'HepatitBKesin',
sum(case when olumteshisi.Kodu in  ('B16', 'B16.0', 'B16.1', 'B16.2', 'B16.9') then 1 else 0 end) as 'HepatitBOlum',
0  as 'HepatitCvaka',
sum(case when kesinteshis.Kodu ='B17.1' then 1 else 0 end) as 'HepatitCKesin',
sum(case when olumteshisi.Kodu ='B17.1' then 1 else 0 end) as 'HepatitCOlum',
0  as 'HepatitEVaka',
sum(case when kesinteshis.Kodu ='B17.2' then 1 else 0 end) as 'HepatitEKesin',
sum(case when olumteshisi.Kodu ='B17.2' then 1 else 0 end) as 'HepatitEOlum',
0  as 'KabakulakVaka',
sum(case when kesinteshis.Kodu in ('B26', 'B26.0', 'B26.1', 'B26.2', 'B26.3', 'B26.8', 'B26.9')  then 1 else 0 end) as 'KabakulakKesin',
sum(case when olumteshisi.Kodu in ('B26', 'B26.0', 'B26.1', 'B26.2', 'B26.3', 'B26.8', 'B26.9')  then 1 else 0 end) as 'KabakulakOlum',
0  as 'KızamıkVaka',
sum(case when kesinteshis.Kodu in ('B05', 'B05.0', 'B05.1', 'B05.2', 'B05.3', 'B05.4', 'B05.8', 'B05.9')  then 1 else 0 end) as 'KızamıkKesin',
sum(case when olumteshisi.Kodu in ('B05', 'B05.0', 'B05.1', 'B05.2', 'B05.3', 'B05.4', 'B05.8', 'B05.9')  then 1 else 0 end) as 'KızamıkOlum',
0  as 'KızamıkçıkVaka',
sum(case when kesinteshis.Kodu in ('B06', 'B06.0', 'B06.8', 'B06.9')  then 1 else 0 end) as 'KızamıkçıkKesin',
sum(case when olumteshisi.Kodu in ('B06', 'B06.0', 'B06.8', 'B06.9')  then 1 else 0 end) as 'KızamıkçıkOlum',
0  as 'KoleraVaka',
sum(case when kesinteshis.Kodu in ('A00', 'A00.0', 'A00.9' )  then 1 else 0 end) as 'KoleraKesin',
sum(case when olumteshisi.Kodu in ('A00', 'A00.0', 'A00.9' )  then 1 else 0 end) as 'KoleraOlum',
0  as 'KuduzVaka',
sum(case when kesinteshis.Kodu in  ('A82', 'A82.0', 'A82.9')   then 1 else 0 end) as 'KuduzKesin',
sum(case when olumteshisi.Kodu in  ('A82', 'A82.0', 'A82.9')   then 1 else 0 end) as 'KuduzOlum',
0  as 'KuduzRiskliTemasVaka',
sum(case when kesinteshis.Kodu ='Z20.3'   then 1 else 0 end) as 'KuduzRiskliTemasKesin',
sum(case when olumteshisi.Kodu ='Z20.3'   then 1 else 0 end) as 'KuduzRiskliTemasOlum',
0  as 'MeningokokkalHastalıkVaka',
sum(case when kesinteshis.Kodu in ('A39', 'A39.0', 'A39.1', 'A39.2', 'A39.3', 'A39.4', 'A39.5', 'A39.8', 'A39.9')   then 1 else 0 end) as 'MeningokokkalHastalıkKesin',
sum(case when olumteshisi.Kodu in ('A39', 'A39.0', 'A39.1', 'A39.2', 'A39.3', 'A39.4', 'A39.5', 'A39.8', 'A39.9')   then 1 else 0 end) as 'MeningokokkalHastalıkOlum',
0  as 'NeonatalTetenozVaka',
sum(case when kesinteshis.Kodu='A33'   then 1 else 0 end) as 'NeonatalTetenozKesin',
sum(case when olumteshisi.Kodu='A33'   then 1 else 0 end) as 'NeonatalTetenozOlum',
0  as 'PoliomiyelitVaka',
sum(case when kesinteshis.Kodu in ('A802', 'A80.0', 'A80.1', 'A80.2', 'A80.3', 'A80.4', 'A80.9')   then 1 else 0 end) as 'PoliomiyelitKesin',
sum(case when olumteshisi.Kodu in ('A802', 'A80.0', 'A80.1', 'A80.2', 'A80.3', 'A80.4', 'A80.9')   then 1 else 0 end) as 'PoliomiyelitOlum',
0  as 'SıtmaVaka',
sum(case when kesinteshis.Kodu in ('B53', 'B53.0', 'B53.1', 'B53.8', 'B54')   then 1 else 0 end) as 'SıtmaKesin',
sum(case when olumteshisi.Kodu in ('B53', 'B53.0', 'B53.1', 'B53.8', 'B54')   then 1 else 0 end) as 'SıtmaOlum',
0  as 'SifilizVaka',
sum(case when kesinteshis.Kodu in ('A53', 'A53.0', 'A53.9')   then 1 else 0 end) as 'SifilizKesin',
sum(case when olumteshisi.Kodu in ('A53', 'A53.0', 'A53.9')   then 1 else 0 end) as 'SifilizOlum',
0  as 'ŞarbonVaka',
sum(case when kesinteshis.Kodu in ('A22', 'A22.0', 'A22.1', 'A22.2', 'A22.7', 'A22.8', 'A22.9')   then 1 else 0 end) as 'ŞarbonKesin',
sum(case when olumteshisi.Kodu in ('A22', 'A22.0', 'A22.1', 'A22.2', 'A22.7', 'A22.8', 'A22.9')   then 1 else 0 end) as 'ŞarbonOlum',
0  as 'ŞarkçıbanıVaka',
sum(case when kesinteshis.Kodu in ('B55', 'B55.0', 'B55.1', 'B55.2', 'B55.9')   then 1 else 0 end) as 'ŞarkçıbanıKesin',
sum(case when olumteshisi.Kodu in ('B55', 'B55.0', 'B55.1', 'B55.2', 'B55.9')   then 1 else 0 end) as 'ŞarkçıbanıOlum',
0  as 'TetanozVaka',
sum(case when kesinteshis.Kodu='A35'   then 1 else 0 end) as 'TetanozKesin',
sum(case when olumteshisi.Kodu='A35'   then 1 else 0 end) as 'TetanozOlum',
0  as 'TifoVaka',
sum(case when kesinteshis.Kodu='A01.0'   then 1 else 0 end) as 'TifoKesin',
sum(case when olumteshisi.Kodu='A01.0'   then 1 else 0 end) as 'TifoOlum',
0  as 'TüberkülozVaka',
sum(case when kesinteshis.Kodu in ('A15','A19')   then 1 else 0 end) as 'TüberkülozKesin',
sum(case when olumteshisi.Kodu in ('A15','A19')   then 1 else 0 end) as 'TüberkülozOlum'

 into  #temptable
From Hasta h
join Muayene on Muayene.Hasta_Id=h.Id and Muayene.MuayeneKapalimi=1
join MuayeneTeshis on MuayeneTeshis.Hasta_Id=h.Id and MuayeneTeshis.Muayene_Id=Muayene.Id and MuayeneTeshis.Alerjikmi=0 and MuayeneTeshis.Kronikmi=0
join Teshis kesinteshis on kesinteshis.Id=MuayeneTeshis.Teshis_Id
left join OlumBildirimi on OlumBildirimi.Hasta_Id=h.Id and Muayene.Id=OlumBildirimi.Muayene_Id
left join Teshis olumteshisi on olumteshisi.Id=OlumBildirimi.Teshis3_Id 
Where 
Muayene.MuayeneTarihi between @BasTarih and @BitTarih
and Muayene.IsAutoImport=0 and MuayeneTeshis.IsAutoImport=0 
and h.Doktor_Id=@doktor
and h.Aktif=1 
group by  dbo.fn_YasGrubu((DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365,'Form17'),h.Cinsiyeti--,kesinteshis.Kodu,olumteshisi.Kodu  

--order by  h.YasGrubu ASC




select
* from 
(

select * from #temptable

union all

select 
#temptable.YasGrubu,
'T' as Cinsiyeti,
sum(AkutKanlıİshalvaka) as AkutKanlıİshalvaka,
sum(AkutKanlıİshalKesin)as AkutKanlıİshalKesin,
sum(AkutKanlıİshalOlum) as AkutKanlıİshalOlum,
sum(Brucellavaka) as Brucellavaka,
sum(BrucellaKesin) as BrucellaKesin,
sum(BrucellaOlum) as BrucellaOlum,
sum(Boğmacavaka)  as Boğmacavaka,
sum(BoğmacaKesin) as BoğmacaKesin,
sum(BoğmacaOlum)  as BoğmacaOlum,
sum(Difterivaka)  as Difterivaka,
sum(DifteriKesin) as DifteriKesin,
sum(DifteriOlum)  as DifteriOlum,
sum(Gonorevaka)   as Gonorevaka,
sum(GonoreKesin)  as GonoreKesin,
sum(GonoreOlum)   as GonoreOlum,
sum(HepatitAvaka) as HepatitAvaka,
sum(HepatitAKesin)as HepatitAKesin,
sum(HepatitAOlum) as HepatitAOlum,
sum(HepatitBvaka) as HepatitBvaka,
sum(HepatitBKesin)as HepatitBKesin,
sum(HepatitBOlum) as HepatitBOlum,
sum(HepatitCvaka) as HepatitCvaka,
sum(HepatitCKesin)as HepatitCKesin,
sum(HepatitCOlum) as HepatitCOlum,
sum(HepatitEVaka) as HepatitEVaka,
sum(HepatitEKesin)as HepatitEKesin,
sum(HepatitEOlum) as HepatitEOlum,
sum(KabakulakVaka)as KabakulakVaka,
sum(KabakulakKesin)as KabakulakKesin,
sum(KabakulakOlum) as KabakulakOlum,
sum(KızamıkVaka)   as KızamıkVaka,
sum(KızamıkKesin)  as KızamıkKesin,
sum(KızamıkOlum)   as KızamıkOlum,
sum(KızamıkçıkVaka) as KızamıkçıkVaka,
sum(KızamıkçıkKesin) as KızamıkçıkKesin,
sum(KızamıkçıkOlum)  as KızamıkçıkOlum,
sum(KoleraVaka)      as KoleraVaka,
sum(KoleraKesin)     as KoleraKesin,
sum(KoleraOlum)      as KoleraOlum,
sum(KuduzVaka)       as KuduzVaka,
sum(KuduzKesin)      as KuduzKesin,
sum(KuduzOlum)       as KuduzOlum,
sum(KuduzRiskliTemasVaka) as KuduzRiskliTemasVaka,
sum(KuduzRiskliTemasKesin) as KuduzRiskliTemasKesin,
sum(KuduzRiskliTemasOlum)  as KuduzRiskliTemasOlum,
sum(MeningokokkalHastalıkVaka) as MeningokokkalHastalıkVaka,
sum(MeningokokkalHastalıkKesin) as MeningokokkalHastalıkKesin,
sum(MeningokokkalHastalıkOlum)  as MeningokokkalHastalıkOlum,
sum(NeonatalTetenozVaka)        as NeonatalTetenozVaka,
sum(NeonatalTetenozKesin)       as NeonatalTetenozKesin,
sum(NeonatalTetenozOlum)        as NeonatalTetenozOlum,
sum(PoliomiyelitVaka)           as PoliomiyelitVaka,
sum(PoliomiyelitKesin)          as PoliomiyelitKesin,
sum(PoliomiyelitOlum)           as PoliomiyelitOlum,
sum(SıtmaVaka)                  as SıtmaVaka,
sum(SıtmaKesin)                 as SıtmaKesin,
sum(SıtmaOlum)                 as SıtmaOlum,
sum(SifilizVaka)                as SifilizVaka,
sum(SifilizKesin)               as SifilizKesin,
sum(SifilizOlum)                as SifilizOlum,
sum(ŞarbonVaka)                 as ŞarbonVaka,
sum(ŞarbonKesin)                as ŞarbonKesin,
sum(ŞarbonOlum)                as ŞarbonOlum,
sum(ŞarkçıbanıVaka)             as ŞarkçıbanıVaka,
sum(ŞarkçıbanıKesin)            as ŞarkçıbanıKesin,
sum(ŞarkçıbanıOlum)            as ŞarkçıbanıOlum,
sum(TetanozVaka)                as TetanozVaka,
sum(TetanozKesin)               as TetanozKesin,
sum(TetanozOlum)                as TetanozOlum,
sum(TifoVaka)                   as TifoVaka,
sum(TifoKesin)                  as TifoKesin,
sum(TifoOlum)                   as TifoOlum,
sum(TüberkülozVaka)                   as TüberkülozVaka,
sum(TüberkülozKesin)                  as TüberkülozKesin,
sum(TüberkülozOlum)                   as TüberkülozOlum
from #temptable 
Group by #temptable.YasGrubu



union all
--------------Tüm Toplamları veriyor----

select 

'TOPLAM' as YasGrubu,
Cinsiyeti,
sum(AkutKanlıİshalvaka) as AkutKanlıİshalvaka,
sum(AkutKanlıİshalKesin)as AkutKanlıİshalKesin,
sum(AkutKanlıİshalOlum) as AkutKanlıİshalOlum,
sum(Brucellavaka) as Brucellavaka,
sum(BrucellaKesin) as BrucellaKesin,
sum(BrucellaOlum) as BrucellaOlum,
sum(Boğmacavaka)  as Boğmacavaka,
sum(BoğmacaKesin) as BoğmacaKesin,
sum(BoğmacaOlum)  as BoğmacaOlum,
sum(Difterivaka)  as Difterivaka,
sum(DifteriKesin) as DifteriKesin,
sum(DifteriOlum)  as DifteriOlum,
sum(Gonorevaka)   as Gonorevaka,
sum(GonoreKesin)  as GonoreKesin,
sum(GonoreOlum)   as GonoreOlum,
sum(HepatitAvaka) as HepatitAvaka,
sum(HepatitAKesin)as HepatitAKesin,
sum(HepatitAOlum) as HepatitAOlum,
sum(HepatitBvaka) as HepatitBvaka,
sum(HepatitBKesin)as HepatitBKesin,
sum(HepatitBOlum) as HepatitBOlum,
sum(HepatitCvaka) as HepatitCvaka,
sum(HepatitCKesin)as HepatitCKesin,
sum(HepatitCOlum) as HepatitCOlum,
sum(HepatitEVaka) as HepatitEVaka,
sum(HepatitEKesin)as HepatitEKesin,
sum(HepatitEOlum) as HepatitEOlum,
sum(KabakulakVaka)as KabakulakVaka,
sum(KabakulakKesin)as KabakulakKesin,
sum(KabakulakOlum) as KabakulakOlum,
sum(KızamıkVaka)   as KızamıkVaka,
sum(KızamıkKesin)  as KızamıkKesin,
sum(KızamıkOlum)   as KızamıkOlum,
sum(KızamıkçıkVaka) as KızamıkçıkVaka,
sum(KızamıkçıkKesin) as KızamıkçıkKesin,
sum(KızamıkçıkOlum)  as KızamıkçıkOlum,
sum(KoleraVaka)      as KoleraVaka,
sum(KoleraKesin)     as KoleraKesin,
sum(KoleraOlum)      as KoleraOlum,
sum(KuduzVaka)       as KuduzVaka,
sum(KuduzKesin)      as KuduzKesin,
sum(KuduzOlum)       as KuduzOlum,
sum(KuduzRiskliTemasVaka) as KuduzRiskliTemasVaka,
sum(KuduzRiskliTemasKesin) as KuduzRiskliTemasKesin,
sum(KuduzRiskliTemasOlum)  as KuduzRiskliTemasOlum,
sum(MeningokokkalHastalıkVaka) as MeningokokkalHastalıkVaka,
sum(MeningokokkalHastalıkKesin) as MeningokokkalHastalıkKesin,
sum(MeningokokkalHastalıkOlum)  as MeningokokkalHastalıkOlum,
sum(NeonatalTetenozVaka)        as NeonatalTetenozVaka,
sum(NeonatalTetenozKesin)       as NeonatalTetenozKesin,
sum(NeonatalTetenozOlum)        as NeonatalTetenozOlum,
sum(PoliomiyelitVaka)           as PoliomiyelitVaka,
sum(PoliomiyelitKesin)          as PoliomiyelitKesin,
sum(PoliomiyelitOlum)           as PoliomiyelitOlum,
sum(SıtmaVaka)                  as SıtmaVaka,
sum(SıtmaKesin)                 as SıtmaKesin,
sum(SıtmaOlum)                 as SıtmaOlum,
sum(SifilizVaka)                as SifilizVaka,
sum(SifilizKesin)               as SifilizKesin,
sum(SifilizOlum)                as SifilizOlum,
sum(ŞarbonVaka)                 as ŞarbonVaka,
sum(ŞarbonKesin)                as ŞarbonKesin,
sum(ŞarbonOlum)                as ŞarbonOlum,
sum(ŞarkçıbanıVaka)             as ŞarkçıbanıVaka,
sum(ŞarkçıbanıKesin)            as ŞarkçıbanıKesin,
sum(ŞarkçıbanıOlum)            as ŞarkçıbanıOlum,
sum(TetanozVaka)                as TetanozVaka,
sum(TetanozKesin)               as TetanozKesin,
sum(TetanozOlum)                as TetanozOlum,
sum(TifoVaka)                   as TifoVaka,
sum(TifoKesin)                  as TifoKesin,
sum(TifoOlum)                   as TifoOlum,
sum(TüberkülozVaka)                   as TüberkülozVaka,
sum(TüberkülozKesin)                  as TüberkülozKesin,
sum(TüberkülozOlum)                   as TüberkülozOlum
from #temptable 
Group by #temptable.Cinsiyeti

union all
select 

'TOPLAM' as YasGrubu,
'T'       as Cinsiyeti,
sum(AkutKanlıİshalvaka) as AkutKanlıİshalvaka,
sum(AkutKanlıİshalKesin)as AkutKanlıİshalKesin,
sum(AkutKanlıİshalOlum) as AkutKanlıİshalOlum,
sum(Brucellavaka) as Brucellavaka,
sum(BrucellaKesin) as BrucellaKesin,
sum(BrucellaOlum) as BrucellaOlum,
sum(Boğmacavaka)  as Boğmacavaka,
sum(BoğmacaKesin) as BoğmacaKesin,
sum(BoğmacaOlum)  as BoğmacaOlum,
sum(Difterivaka)  as Difterivaka,
sum(DifteriKesin) as DifteriKesin,
sum(DifteriOlum)  as DifteriOlum,
sum(Gonorevaka)   as Gonorevaka,
sum(GonoreKesin)  as GonoreKesin,
sum(GonoreOlum)   as GonoreOlum,
sum(HepatitAvaka) as HepatitAvaka,
sum(HepatitAKesin)as HepatitAKesin,
sum(HepatitAOlum) as HepatitAOlum,
sum(HepatitBvaka) as HepatitBvaka,
sum(HepatitBKesin)as HepatitBKesin,
sum(HepatitBOlum) as HepatitBOlum,
sum(HepatitCvaka) as HepatitCvaka,
sum(HepatitCKesin)as HepatitCKesin,
sum(HepatitCOlum) as HepatitCOlum,
sum(HepatitEVaka) as HepatitEVaka,
sum(HepatitEKesin)as HepatitEKesin,
sum(HepatitEOlum) as HepatitEOlum,
sum(KabakulakVaka)as KabakulakVaka,
sum(KabakulakKesin)as KabakulakKesin,
sum(KabakulakOlum) as KabakulakOlum,
sum(KızamıkVaka)   as KızamıkVaka,
sum(KızamıkKesin)  as KızamıkKesin,
sum(KızamıkOlum)   as KızamıkOlum,
sum(KızamıkçıkVaka) as KızamıkçıkVaka,
sum(KızamıkçıkKesin) as KızamıkçıkKesin,
sum(KızamıkçıkOlum)  as KızamıkçıkOlum,
sum(KoleraVaka)      as KoleraVaka,
sum(KoleraKesin)     as KoleraKesin,
sum(KoleraOlum)      as KoleraOlum,
sum(KuduzVaka)       as KuduzVaka,
sum(KuduzKesin)      as KuduzKesin,
sum(KuduzOlum)       as KuduzOlum,
sum(KuduzRiskliTemasVaka) as KuduzRiskliTemasVaka,
sum(KuduzRiskliTemasKesin) as KuduzRiskliTemasKesin,
sum(KuduzRiskliTemasOlum)  as KuduzRiskliTemasOlum,
sum(MeningokokkalHastalıkVaka) as MeningokokkalHastalıkVaka,
sum(MeningokokkalHastalıkKesin) as MeningokokkalHastalıkKesin,
sum(MeningokokkalHastalıkOlum)  as MeningokokkalHastalıkOlum,
sum(NeonatalTetenozVaka)        as NeonatalTetenozVaka,
sum(NeonatalTetenozKesin)       as NeonatalTetenozKesin,
sum(NeonatalTetenozOlum)        as NeonatalTetenozOlum,
sum(PoliomiyelitVaka)           as PoliomiyelitVaka,
sum(PoliomiyelitKesin)          as PoliomiyelitKesin,
sum(PoliomiyelitOlum)           as PoliomiyelitOlum,
sum(SıtmaVaka)                  as SıtmaVaka,
sum(SıtmaKesin)                 as SıtmaKesin,
sum(SıtmaOlum)                 as SıtmaOlum,
sum(SifilizVaka)                as SifilizVaka,
sum(SifilizKesin)               as SifilizKesin,
sum(SifilizOlum)                as SifilizOlum,
sum(ŞarbonVaka)                 as ŞarbonVaka,
sum(ŞarbonKesin)                as ŞarbonKesin,
sum(ŞarbonOlum)                as ŞarbonOlum,
sum(ŞarkçıbanıVaka)             as ŞarkçıbanıVaka,
sum(ŞarkçıbanıKesin)            as ŞarkçıbanıKesin,
sum(ŞarkçıbanıOlum)            as ŞarkçıbanıOlum,
sum(TetanozVaka)                as TetanozVaka,
sum(TetanozKesin)               as TetanozKesin,
sum(TetanozOlum)                as TetanozOlum,
sum(TifoVaka)                   as TifoVaka,
sum(TifoKesin)                  as TifoKesin,
sum(TifoOlum)                   as TifoOlum,
sum(TüberkülozVaka)                   as TüberkülozVaka,
sum(TüberkülozKesin)                  as TüberkülozKesin,
sum(TüberkülozOlum)                   as TüberkülozOlum
from #temptable 

)
B 
order by B.yasgrubu,B.Cinsiyeti

drop table #temptable


end  ");
            }
            catch 
            {
                
               
            }


try
{
    SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@" drop procedure [dbo].[SpForm18ARaporu]");
}
catch
{


}


            try
            {
SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(
@" CREATE procedure [dbo].[SpForm18ARaporu] (@BasTarih datetime,@BitTarih datetime,@doktor bigint) as
begin
set dateformat ymd;
Select
dbo.fn_YasGrubu(((DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365),'Form18A')																as YasGrubu,
Case h.Cinsiyeti
when 'Erkek' then 'E'
when 'Kadın' then 'K' else '' end																												as Cinsiyeti,
sum(case when kesinteshis.Kodu in ('J06','J06.0','J06.8','J06.9') then 1 else 0 end)															as 'AkutÜstSolunumYoluEnfeksiyonuVaka',
sum(case when olumteshisi.Kodu in ('J06','J06.0','J06.8','J06.9') then 1 else 0 end)															as 'AkutÜstSolunumYoluEnfeksiyonuOlum',
sum(case when kesinteshis.Kodu in  ('J01','J01.0','J01.1','J01.2','J01.3','J01.4','J01.8','J01.9') then 1 else 0 end)							as 'AkutSinüzitVaka',
sum(case when olumteshisi.Kodu in  ('J01','J01.0','J01.1','J01.2','J01.3','J01.4','J01.8','J01.9') then 1 else 0 end)							as 'AkutSinüzitOlum',
sum(case when kesinteshis.Kodu in   ('J02','J02.0','J02.8','J02.9') then 1 else 0 end)															as 'AkutFaranjitVaka ',
sum(case when olumteshisi.Kodu in   ('J02','J02.0','J02.8','J02.9') then 1 else 0 end)															as 'AkutFaranjitOlum',
sum(case when kesinteshis.Kodu in   ('J03','J03.0','J03.8','J03.9') then 1 else 0 end)															as 'AkutTonsilitVaka',
sum(case when olumteshisi.Kodu in   ('J03','J03.0','J03.8','J03.9') then 1 else 0 end)															as 'AkutTonsilitOlum',
sum(case when kesinteshis.Kodu in ('J04','J04.0','J04.1','J04.2') then 1 else 0 end)															as 'AkutLarenjitVaka',
sum(case when olumteshisi.Kodu in ('J04','J04.0','J04.1','J04.2') then 1 else 0 end)															as 'AkutLarenjitOlum',
sum(case when kesinteshis.Kodu in ('J20','J20.0','J20.1','J20.2','J20.3','J20.4','J20.5','J20.6','J20.7','J20.8','J20.9') then 1 else 0 end)	as 'AkutBronşitVaka',
sum(case when olumteshisi.Kodu in ('J20','J20.0','J20.1','J20.2','J20.3','J20.4','J20.5','J20.6','J20.7','J20.8','J20.9') then 1 else 0 end)	as 'AkutBronşitOlum',
sum(case when kesinteshis.Kodu in  ('J21','J21.0','J21.8','J21.9') then 1 else 0 end)															as 'AkutBronşiolitVaka',
sum(case when olumteshisi.Kodu in  ('J21','J21.0','J21.8','J21.9') then 1 else 0 end)															as 'AkutBronşiolitOlum',
sum(case when kesinteshis.Kodu in ('J18','J18.0','J18.1','J18.2','J18.8','J18.9') then 1 else 0 end)											as 'AkutPnömoniVaka',
sum(case when olumteshisi.Kodu in ('J18','J18.0','J18.1','J18.2','J18.8','J18.9') then 1 else 0 end)											as 'AkutPnömoniOlum',
sum(case when kesinteshis.Kodu in ('J44','J44.0','J44.1','J44.8','J44.9') then 1 else 0 end)													as 'ObstrüktifAkciğerHastalığıVaka',
sum(case when olumteshisi.Kodu in ('J44','J44.0','J44.1','J44.8','J44.9') then 1 else 0 end)													as 'ObstrüktifAkciğerHastalığıOlum',
sum(case when kesinteshis.Kodu in ('J45','J45.0','J45.1','J45.8','J45.9')  then 1 else 0 end)													as 'AstımVaka',
sum(case when olumteshisi.Kodu in ('J45','J45.0','J45.1','J45.8','J45.9')  then 1 else 0 end)													as 'AstımKesin',
sum(case when kesinteshis.Kodu in ('H66','H66.0','H66.1','H66.2','H66.3','H66.4','H66.9')  then 1 else 0 end) 									as 'AkutOtitisMediaVaka',
sum(case when olumteshisi.Kodu in ('H66','H66.0','H66.1','H66.2','H66.3','H66.4','H66.9')  then 1 else 0 end) 									as 'AkutOtitisMediaOlum',
sum(case when kesinteshis.Kodu in ('I00')  then 1 else 0 end) 																					as 'AkutRomatizmalAteşVaka',
sum(case when olumteshisi.Kodu in ('I00')  then 1 else 0 end) 																					as 'AkutRomatizmalAteşOlum',
sum(case when kesinteshis.Kodu in ('R56.0')  then 1 else 0 end) 																				as 'FebrilKonvülsiyonVaka',
sum(case when olumteshisi.Kodu in ('R56.0')  then 1 else 0 end) 																				as 'FebrilKonvülsiyonOlum',
sum(case when kesinteshis.Kodu in  ('B01','B01.0','B01.1','B01.2','B01.8','B01.9')   then 1 else 0 end)											as 'SuçiçeğiVaka',
sum(case when olumteshisi.Kodu in  ('B01','B01.0','B01.1','B01.2','B01.8','B01.9')   then 1 else 0 end)											as 'SuçiçeğiOlum ',
sum(case when kesinteshis.Kodu ='A38'   then 1 else 0 end) 																						as 'KızılVaka',
sum(case when olumteshisi.Kodu ='A38'   then 1 else 0 end) 																						as 'KızılOlum',
sum(case when kesinteshis.Kodu in ('E43')   then 1 else 0 end) 																					as 'ProteinEnerjiMalnitrüsyonuVaka',
sum(case when olumteshisi.Kodu in ('E43')   then 1 else 0 end) 																					as 'ProteinEnerjiMalnitrüsyonuOlum',
sum(case when kesinteshis.Kodu='E55.0'   then 1 else 0 end) 																					as 'RaşitizmVaka',
sum(case when olumteshisi.Kodu='E55.0'   then 1 else 0 end) 																					as 'RaşitizmOlum',
sum(case when kesinteshis.Kodu in ('D50','D50.0','D50.1','D50.8','D50.9')   then 1 else 0 end) 													as 'DemirEksikliğiAnemisiVaka',
sum(case when olumteshisi.Kodu in ('D50','D50.0','D50.1','D50.8','D50.9')   then 1 else 0 end) 													as 'DemirEksikliğiAnemisiOlum',
sum(case when kesinteshis.Kodu in ('A09')   then 1 else 0 end) 																					as 'İshallerVaka',
sum(case when olumteshisi.Kodu in ('A09')   then 1 else 0 end) 																					as 'İshallerOlum',
sum(case when kesinteshis.Kodu in ('N72')   then 1 else 0 end) 																					as 'ServisitVaka',
sum(case when olumteshisi.Kodu in ('N72')   then 1 else 0 end) 																					as 'ServisitOlum',
sum(case when kesinteshis.Kodu in ('I10')   then 1 else 0 end) 																					as 'hipertansiyonVaka',
sum(case when olumteshisi.Kodu in ('I10')   then 1 else 0 end) 																					as 'hipertansiyonOlum',
sum(case when kesinteshis.Kodu in ('E04','E04.0','E04.1','E04.2','E04.8','E04.9')   then 1 else 0 end) 											as 'GuatrVaka',
sum(case when olumteshisi.Kodu in ('E04','E04.0','E04.1','E04.2','E04.8','E04.9')   then 1 else 0 end) 											as 'GuatrOlum',
sum(case when kesinteshis.Kodu in ('E14','E14.0','E14.1','E14.2','E14.3','E14.4','E14.5','E14.6','E14.7','E14.8','E14.9',
'E11 ','E11.0 ',' E11.1 ',' E11.2 ',' E11.3 ',' E11.4 ',' E11.5 ',' E11.6 ',' E11.7 ',' E11.8 ','E119','E10','E10.0 ',
' E10.1 ',' E10.2 ',' E10.3 ',' E10.4 ',' E10.5 ',' E10.6 ',' E10.7 ',' E10.8 ','E10.9')   then 1 else 0 end)	as 'DiabetVaka',
sum(case when olumteshisi.Kodu IN ('E14','E14.0','E14.1','E14.2','E14.3','E14.4','E14.5','E14.6','E14.7','E14.8','E14.9',
'E11 ','E11.0 ',' E11.1 ',' E11.2 ',' E11.3 ',' E11.4 ',' E11.5 ',' E11.6 ',' E11.7 ',' E11.8 ','E119','E10','E10.0 ',
' E10.1 ',' E10.2 ',' E10.3 ',' E10.4 ',' E10.5 ',' E10.6 ',' E10.7 ',' E10.8 ','E10.9')   then 1 else 0 end)	as 'DiabetOlum',
sum(case when kesinteshis.Kodu in ('E66','E66.0','E66.1','E66.2','E66.8','E66.9')   then 1 else 0 end) 											as 'ObeziteVaka',
sum(case when olumteshisi.Kodu in ('E66','E66.0','E66.1','E66.2','E66.8','E66.9')   then 1 else 0 end) 											as 'ObeziteKesin',
sum(case when kesinteshis.Kodu in ('D56','D56.0','D56.1','D56.2','D56.3','D56.4','D56.8','D56.9')   then 1 else 0 end) 							as 'TalassemiVaka',
sum(case when olumteshisi.Kodu in ('D56','D56.0','D56.1','D56.2','D56.3','D56.4','D56.8','D56.9')   then 1 else 0 end) 							as 'TalassemiOlum',
sum(case when kesinteshis.Kodu in ('D.58.2')   then 1 else 0 end) 																				as 'DiğerHemoglobinopatilerVaka',
sum(case when olumteshisi.Kodu in ('D.58.2')   then 1 else 0 end) 																				as 'DiğerHemoglobinopatilerOlum'

into  #temptable
From Hasta h
join Muayene on Muayene.Hasta_Id=h.Id and Muayene.MuayeneKapalimi=1
join MuayeneTeshis on MuayeneTeshis.Hasta_Id=h.Id and MuayeneTeshis.Muayene_Id=Muayene.Id and MuayeneTeshis.Alerjikmi=0 and MuayeneTeshis.Kronikmi=0
join Teshis kesinteshis on kesinteshis.Id=MuayeneTeshis.Teshis_Id
left join OlumBildirimi on OlumBildirimi.Hasta_Id=h.Id and Muayene.Id=OlumBildirimi.Muayene_Id
left join Teshis olumteshisi on olumteshisi.Id=OlumBildirimi.Teshis3_Id 
Where 
Muayene.MuayeneTarihi between @BasTarih and @BitTarih
and Muayene.IsAutoImport=0 and MuayeneTeshis.IsAutoImport=0 and
h.Doktor_Id=@doktor and h.Aktif=1 
group by  dbo.fn_YasGrubu((DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365,'Form18A'),h.Cinsiyeti--,kesinteshis.Kodu,olumteshisi.Kodu  

--order by  h.YasGrubu ASC




select
* from 
(

select * from #temptable

union all

select 
#temptable.YasGrubu,
'T' as Cinsiyeti,
sum(AkutÜstSolunumYoluEnfeksiyonuVaka) as 'AkutÜstSolunumYoluEnfeksiyonuVaka',
sum(AkutÜstSolunumYoluEnfeksiyonuOlum) as 'AkutÜstSolunumYoluEnfeksiyonuOlum',
sum(AkutSinüzitVaka)	as 'AkutSinüzitVaka',
sum(AkutSinüzitOlum)	as 'AkutSinüzitOlum',
sum(AkutFaranjitVaka )	as 'AkutFaranjitVaka ',
sum(AkutFaranjitOlum)   as 'AkutFaranjitOlum',
sum(AkutTonsilitVaka)   as 'AkutTonsilitVaka',
sum(AkutTonsilitOlum)	as 'AkutTonsilitOlum',
sum(AkutLarenjitVaka)	as 'AkutLarenjitVaka',
sum(AkutLarenjitOlum)	as 'AkutLarenjitOlum',
sum(AkutBronşitVaka)	as 'AkutBronşitVaka',
sum(AkutBronşitOlum)	as 'AkutBronşitOlum',
sum(AkutBronşiolitVaka)	as 'AkutBronşiolitVaka',
sum(AkutBronşiolitOlum)	as 'AkutBronşiolitOlum',
sum(AkutPnömoniVaka)	as 'AkutPnömoniVaka',
sum(AkutPnömoniOlum)	as 'AkutPnömoniOlum',
sum(ObstrüktifAkciğerHastalığıVaka)	as 'ObstrüktifAkciğerHastalığıVaka',
sum(ObstrüktifAkciğerHastalığıOlum)	as 'ObstrüktifAkciğerHastalığıOlum',
sum(AstımVaka)			as 'AstımVaka',
sum(AstımKesin)			as 'AstımKesin',
sum(AkutOtitisMediaVaka)	as 'AkutOtitisMediaVaka',
sum(AkutOtitisMediaOlum)	as 'AkutOtitisMediaOlum',
sum(AkutRomatizmalAteşVaka)	as 'AkutRomatizmalAteşVaka',
sum(AkutRomatizmalAteşOlum)	as 'AkutRomatizmalAteşOlum',
sum(FebrilKonvülsiyonVaka)	as 'FebrilKonvülsiyonVaka',
sum(FebrilKonvülsiyonOlum)	as 'FebrilKonvülsiyonOlum',
sum(SuçiçeğiVaka)			as 'SuçiçeğiVaka',
sum(SuçiçeğiOlum)			as 'SuçiçeğiOlum ',
sum(KızılVaka)				as 'KızılVaka',
sum(KızılOlum)				as 'KızılOlum',
sum(ProteinEnerjiMalnitrüsyonuVaka)	as 'ProteinEnerjiMalnitrüsyonuVaka',
sum(ProteinEnerjiMalnitrüsyonuOlum)	as 'ProteinEnerjiMalnitrüsyonuOlum',
sum(RaşitizmVaka)			as 'RaşitizmVaka',
sum(RaşitizmOlum)			as 'RaşitizmOlum',
sum(DemirEksikliğiAnemisiVaka)	as 'DemirEksikliğiAnemisiVaka',
sum(DemirEksikliğiAnemisiOlum)	as 'DemirEksikliğiAnemisiOlum',
sum(İshallerVaka)				as 'İshallerVaka',
sum(İshallerOlum)				as 'İshallerOlum',
sum(ServisitVaka)				as 'ServisitVaka',
sum(ServisitOlum)				as 'ServisitOlum',
sum(hipertansiyonVaka)			as 'hipertansiyonVaka',
sum(hipertansiyonOlum)			as 'hipertansiyonOlum',
sum(GuatrVaka)					as 'GuatrVaka',
sum(GuatrOlum)					as 'GuatrOlum',
sum(DiabetVaka)					as 'DiabetVaka',
sum(DiabetOlum)					as 'DiabetOlum',
sum(ObeziteVaka)				as 'ObeziteVaka',
sum(ObeziteKesin)				as 'ObeziteKesin',
sum(TalassemiVaka)				as 'TalassemiVaka',
sum(TalassemiOlum)				as 'TalassemiOlum',
sum(DiğerHemoglobinopatilerVaka)as 'DiğerHemoglobinopatilerVaka',
sum(DiğerHemoglobinopatilerOlum)as 'DiğerHemoglobinopatilerOlum'
from #temptable 
Group by #temptable.YasGrubu



union all
--------------Tüm Toplamları veriyor----

select 

'TOPLAM' as YasGrubu,
Cinsiyeti,
sum(AkutÜstSolunumYoluEnfeksiyonuVaka) as 'AkutÜstSolunumYoluEnfeksiyonuVaka',
sum(AkutÜstSolunumYoluEnfeksiyonuOlum) as 'AkutÜstSolunumYoluEnfeksiyonuOlum',
sum(AkutSinüzitVaka)	as 'AkutSinüzitVaka',
sum(AkutSinüzitOlum)	as 'AkutSinüzitOlum',
sum(AkutFaranjitVaka )	as 'AkutFaranjitVaka ',
sum(AkutFaranjitOlum)   as 'AkutFaranjitOlum',
sum(AkutTonsilitVaka)   as 'AkutTonsilitVaka',
sum(AkutTonsilitOlum)	as 'AkutTonsilitOlum',
sum(AkutLarenjitVaka)	as 'AkutLarenjitVaka',
sum(AkutLarenjitOlum)	as 'AkutLarenjitOlum',
sum(AkutBronşitVaka)	as 'AkutBronşitVaka',
sum(AkutBronşitOlum)	as 'AkutBronşitOlum',
sum(AkutBronşiolitVaka)	as 'AkutBronşiolitVaka',
sum(AkutBronşiolitOlum)	as 'AkutBronşiolitOlum',
sum(AkutPnömoniVaka)	as 'AkutPnömoniVaka',
sum(AkutPnömoniOlum)	as 'AkutPnömoniOlum',
sum(ObstrüktifAkciğerHastalığıVaka)	as 'ObstrüktifAkciğerHastalığıVaka',
sum(ObstrüktifAkciğerHastalığıOlum)	as 'ObstrüktifAkciğerHastalığıOlum',
sum(AstımVaka)			as 'AstımVaka',
sum(AstımKesin)			as 'AstımKesin',
sum(AkutOtitisMediaVaka)	as 'AkutOtitisMediaVaka',
sum(AkutOtitisMediaOlum)	as 'AkutOtitisMediaOlum',
sum(AkutRomatizmalAteşVaka)	as 'AkutRomatizmalAteşVaka',
sum(AkutRomatizmalAteşOlum)	as 'AkutRomatizmalAteşOlum',
sum(FebrilKonvülsiyonVaka)	as 'FebrilKonvülsiyonVaka',
sum(FebrilKonvülsiyonOlum)	as 'FebrilKonvülsiyonOlum',
sum(SuçiçeğiVaka)			as 'SuçiçeğiVaka',
sum(SuçiçeğiOlum)			as 'SuçiçeğiOlum ',
sum(KızılVaka)				as 'KızılVaka',
sum(KızılOlum)				as 'KızılOlum',
sum(ProteinEnerjiMalnitrüsyonuVaka)	as 'ProteinEnerjiMalnitrüsyonuVaka',
sum(ProteinEnerjiMalnitrüsyonuOlum)	as 'ProteinEnerjiMalnitrüsyonuOlum',
sum(RaşitizmVaka)			as 'RaşitizmVaka',
sum(RaşitizmOlum)			as 'RaşitizmOlum',
sum(DemirEksikliğiAnemisiVaka)	as 'DemirEksikliğiAnemisiVaka',
sum(DemirEksikliğiAnemisiOlum)	as 'DemirEksikliğiAnemisiOlum',
sum(İshallerVaka)				as 'İshallerVaka',
sum(İshallerOlum)				as 'İshallerOlum',
sum(ServisitVaka)				as 'ServisitVaka',
sum(ServisitOlum)				as 'ServisitOlum',
sum(hipertansiyonVaka)			as 'hipertansiyonVaka',
sum(hipertansiyonOlum)			as 'hipertansiyonOlum',
sum(GuatrVaka)					as 'GuatrVaka',
sum(GuatrOlum)					as 'GuatrOlum',
sum(DiabetVaka)					as 'DiabetVaka',
sum(DiabetOlum)					as 'DiabetOlum',
sum(ObeziteVaka)				as 'ObeziteVaka',
sum(ObeziteKesin)				as 'ObeziteKesin',
sum(TalassemiVaka)				as 'TalassemiVaka',
sum(TalassemiOlum)				as 'TalassemiOlum',
sum(DiğerHemoglobinopatilerVaka)as 'DiğerHemoglobinopatilerVaka',
sum(DiğerHemoglobinopatilerOlum)as 'DiğerHemoglobinopatilerOlum'
from #temptable 
Group by #temptable.Cinsiyeti

union all
select 

'TOPLAM' as YasGrubu,
'T'       as Cinsiyeti,
sum(AkutÜstSolunumYoluEnfeksiyonuVaka) as 'AkutÜstSolunumYoluEnfeksiyonuVaka',
sum(AkutÜstSolunumYoluEnfeksiyonuOlum) as 'AkutÜstSolunumYoluEnfeksiyonuOlum',
sum(AkutSinüzitVaka)	as 'AkutSinüzitVaka',
sum(AkutSinüzitOlum)	as 'AkutSinüzitOlum',
sum(AkutFaranjitVaka )	as 'AkutFaranjitVaka ',
sum(AkutFaranjitOlum)   as 'AkutFaranjitOlum',
sum(AkutTonsilitVaka)   as 'AkutTonsilitVaka',
sum(AkutTonsilitOlum)	as 'AkutTonsilitOlum',
sum(AkutLarenjitVaka)	as 'AkutLarenjitVaka',
sum(AkutLarenjitOlum)	as 'AkutLarenjitOlum',
sum(AkutBronşitVaka)	as 'AkutBronşitVaka',
sum(AkutBronşitOlum)	as 'AkutBronşitOlum',
sum(AkutBronşiolitVaka)	as 'AkutBronşiolitVaka',
sum(AkutBronşiolitOlum)	as 'AkutBronşiolitOlum',
sum(AkutPnömoniVaka)	as 'AkutPnömoniVaka',
sum(AkutPnömoniOlum)	as 'AkutPnömoniOlum',
sum(ObstrüktifAkciğerHastalığıVaka)	as 'ObstrüktifAkciğerHastalığıVaka',
sum(ObstrüktifAkciğerHastalığıOlum)	as 'ObstrüktifAkciğerHastalığıOlum',
sum(AstımVaka)			as 'AstımVaka',
sum(AstımKesin)			as 'AstımKesin',
sum(AkutOtitisMediaVaka)	as 'AkutOtitisMediaVaka',
sum(AkutOtitisMediaOlum)	as 'AkutOtitisMediaOlum',
sum(AkutRomatizmalAteşVaka)	as 'AkutRomatizmalAteşVaka',
sum(AkutRomatizmalAteşOlum)	as 'AkutRomatizmalAteşOlum',
sum(FebrilKonvülsiyonVaka)	as 'FebrilKonvülsiyonVaka',
sum(FebrilKonvülsiyonOlum)	as 'FebrilKonvülsiyonOlum',
sum(SuçiçeğiVaka)			as 'SuçiçeğiVaka',
sum(SuçiçeğiOlum)			as 'SuçiçeğiOlum ',
sum(KızılVaka)				as 'KızılVaka',
sum(KızılOlum)				as 'KızılOlum',
sum(ProteinEnerjiMalnitrüsyonuVaka)	as 'ProteinEnerjiMalnitrüsyonuVaka',
sum(ProteinEnerjiMalnitrüsyonuOlum)	as 'ProteinEnerjiMalnitrüsyonuOlum',
sum(RaşitizmVaka)			as 'RaşitizmVaka',
sum(RaşitizmOlum)			as 'RaşitizmOlum',
sum(DemirEksikliğiAnemisiVaka)	as 'DemirEksikliğiAnemisiVaka',
sum(DemirEksikliğiAnemisiOlum)	as 'DemirEksikliğiAnemisiOlum',
sum(İshallerVaka)				as 'İshallerVaka',
sum(İshallerOlum)				as 'İshallerOlum',
sum(ServisitVaka)				as 'ServisitVaka',
sum(ServisitOlum)				as 'ServisitOlum',
sum(hipertansiyonVaka)			as 'hipertansiyonVaka',
sum(hipertansiyonOlum)			as 'hipertansiyonOlum',
sum(GuatrVaka)					as 'GuatrVaka',
sum(GuatrOlum)					as 'GuatrOlum',
sum(DiabetVaka)					as 'DiabetVaka',
sum(DiabetOlum)					as 'DiabetOlum',
sum(ObeziteVaka)				as 'ObeziteVaka',
sum(ObeziteKesin)				as 'ObeziteKesin',
sum(TalassemiVaka)				as 'TalassemiVaka',
sum(TalassemiOlum)				as 'TalassemiOlum',
sum(DiğerHemoglobinopatilerVaka)as 'DiğerHemoglobinopatilerVaka',
sum(DiğerHemoglobinopatilerOlum)as 'DiğerHemoglobinopatilerOlum'
from #temptable 

)
B 
order by B.yasgrubu,B.Cinsiyeti

drop table #temptable


end ");
            }
            catch 
            {
                
                
            }

             try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"drop procedure [dbo].[SpForm18BRaporu]");
                }
            
             catch 
            {
                
                
            }

             try
            {
                SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"create procedure [dbo].[SpForm18BRaporu] (@BasTarih datetime,@BitTarih datetime,@doktor bigint) as
begin

--declare @BasTarih datetime
--declare @BitTarih datetime
set dateformat ymd;
--set @BasTarih='01.01.2010'
--set @BitTarih='01.11.2011'

--select @BasTarih,@BitTarih

Select
dbo.fn_YasGrubu(((DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365),'Form18A')																as YasGrubu,
Case h.Cinsiyeti
when 'Erkek' then 'E'
when 'Kadın' then 'K' else '-1' end																						as Cinsiyeti,
sum(case when kesinteshis.Kodu in ('B77') then 1 else 0 end)															as 'AskariyazVaka',
sum(case when olumteshisi.Kodu in ('B77') then 1 else 0 end)															as 'AskariyazOlum',
sum(case when kesinteshis.Kodu in  ('B75') then 1 else 0 end)															as 'TrişinellozVaka',
sum(case when olumteshisi.Kodu in  ('B75') then 1 else 0 end)															as 'TrişinellozOlum',
sum(case when kesinteshis.Kodu in   ('B80') then 1 else 0 end)															as 'EnterobiyazVaka',
sum(case when olumteshisi.Kodu in   ('B80') then 1 else 0 end)															as 'EnterobiyazOlum',
sum(case when kesinteshis.Kodu in   ('B76') then 1 else 0 end)															as 'KancalıkurthastalıklarıVaka',
sum(case when olumteshisi.Kodu in   ('B76') then 1 else 0 end)															as 'KancalıkurthastalıklarıOlum',
sum(case when kesinteshis.Kodu in ('B78') then 1 else 0 end)															as 'StrongiloidiyazVaka',
sum(case when olumteshisi.Kodu in ('B78') then 1 else 0 end)															as 'StrongiloidiyazOlum',
sum(case when kesinteshis.Kodu in ('-1') then 1 else 0 end)																as 'AltiVaka',
sum(case when olumteshisi.Kodu in ('-1') then 1 else 0 end)																as 'AltiOlum',
sum(case when kesinteshis.Kodu in  ('B68') then 1 else 0 end)															as 'TenyaVaka',
sum(case when olumteshisi.Kodu in  ('B68') then 1 else 0 end)															as 'TenyaOlum',
sum(case when kesinteshis.Kodu in ('-1') then 1 else 0 end)																as 'SekizVaka',
sum(case when olumteshisi.Kodu in ('-1') then 1 else 0 end)																as 'SekizOlum',
sum(case when kesinteshis.Kodu in ('-1') then 1 else 0 end)																as 'DokuzVaka',
sum(case when olumteshisi.Kodu in ('-1') then 1 else 0 end)																as 'DokuzOlum',
sum(case when kesinteshis.Kodu in ('A59')  then 1 else 0 end)															as 'TrikomoniazVaka',
sum(case when olumteshisi.Kodu in ('A59')  then 1 else 0 end)															as 'TrikomoniazKesin',
sum(case when kesinteshis.Kodu in ('B86')  then 1 else 0 end) 															as 'SkabiyezVaka',
sum(case when olumteshisi.Kodu in ('B86')  then 1 else 0 end) 															as 'SkabiyezOlum',
sum(case when kesinteshis.Kodu in ('B85')  then 1 else 0 end) 															as 'PedikülozvepithiriyazVaka',
sum(case when olumteshisi.Kodu in ('B85')  then 1 else 0 end) 															as 'PedikülozvepithiriyazOlum',
sum(case when kesinteshis.Kodu in ('-1')  then 1 else 0 end) 															as 'OnucVaka',
sum(case when olumteshisi.Kodu in ('-1')  then 1 else 0 end) 															as 'OnucOlum'

into  #temptable
From Hasta h
join Muayene on Muayene.Hasta_Id=h.Id and Muayene.MuayeneKapalimi=1
join MuayeneTeshis on MuayeneTeshis.Hasta_Id=h.Id and MuayeneTeshis.Muayene_Id=Muayene.Id and MuayeneTeshis.Alerjikmi=0 and MuayeneTeshis.Kronikmi=0
join Teshis kesinteshis on kesinteshis.Id=MuayeneTeshis.Teshis_Id
left join OlumBildirimi on OlumBildirimi.Hasta_Id=h.Id and Muayene.Id=OlumBildirimi.Muayene_Id
left join Teshis olumteshisi on olumteshisi.Id=OlumBildirimi.Teshis3_Id 
Where 
Muayene.MuayeneTarihi between @BasTarih and @BitTarih
and Muayene.IsAutoImport=0 and MuayeneTeshis.IsAutoImport=0 and 
h.Doktor_Id=@doktor and h.Aktif=1 
group by  dbo.fn_YasGrubu((DATEDIFF(DD,isnull(BeyanDogumTarihi,DogumTarihi),getdate()))/365,'Form18A'),h.Cinsiyeti--,kesinteshis.Kodu,olumteshisi.Kodu  

--order by  h.YasGrubu ASC




select
* from 
(

select * from #temptable

union all

select 
#temptable.YasGrubu,
'T' as Cinsiyeti,
sum(AskariyazVaka) as 'AskariyazVaka',
sum(AskariyazOlum) as 'AskariyazOlum',
sum(TrişinellozVaka)	as 'TrişinellozVaka',
sum(TrişinellozOlum)	as 'TrişinellozOlum',
sum(EnterobiyazVaka)	as 'EnterobiyazVaka',
sum(EnterobiyazOlum)   as 'EnterobiyazOlum',
sum(KancalıkurthastalıklarıVaka)   as 'KancalıkurthastalıklarıVaka',
sum(KancalıkurthastalıklarıOlum)	as 'KancalıkurthastalıklarıOlum',
sum(StrongiloidiyazVaka)	as 'StrongiloidiyazVaka',
sum(StrongiloidiyazOlum)	as 'StrongiloidiyazOlum',
sum(AltiVaka)	as 'AltiVaka',
sum(AltiOlum)	as 'AltiOlum',
sum(TenyaVaka)	as 'TenyaVaka',
sum(TenyaOlum)	as 'TenyaOlum',
sum(SekizVaka)	as 'SekizVaka',
sum(SekizOlum)	as 'SekizOlum',
sum(DokuzVaka)	as 'DokuzVaka',
sum(DokuzOlum)	as 'DokuzOlum',
sum(TrikomoniazVaka)			as 'TrikomoniazVaka',
sum(TrikomoniazKesin)			as 'TrikomoniazKesin',
sum(SkabiyezVaka)	as 'SkabiyezVaka',
sum(SkabiyezOlum)	as 'SkabiyezOlum',
sum(PedikülozvepithiriyazVaka)	as 'PedikülozvepithiriyazVaka',
sum(PedikülozvepithiriyazOlum)	as 'PedikülozvepithiriyazOlum',
sum(OnucVaka)	as 'OnucVaka',
sum(OnucOlum)	as 'OnucOlum'
from #temptable 
Group by #temptable.YasGrubu



union all
--------------Tüm Toplamları veriyor----

select 

'TOPLAM' as YasGrubu,
Cinsiyeti,
sum(AskariyazVaka) as 'AskariyazVaka',
sum(AskariyazOlum) as 'AskariyazOlum',
sum(TrişinellozVaka)	as 'TrişinellozVaka',
sum(TrişinellozOlum)	as 'TrişinellozOlum',
sum(EnterobiyazVaka)	as 'EnterobiyazVaka',
sum(EnterobiyazOlum)   as 'EnterobiyazOlum',
sum(KancalıkurthastalıklarıVaka)   as 'KancalıkurthastalıklarıVaka',
sum(KancalıkurthastalıklarıOlum)	as 'KancalıkurthastalıklarıOlum',
sum(StrongiloidiyazVaka)	as 'StrongiloidiyazVaka',
sum(StrongiloidiyazOlum)	as 'StrongiloidiyazOlum',
sum(AltiVaka)	as 'AltiVaka',
sum(AltiOlum)	as 'AltiOlum',
sum(TenyaVaka)	as 'TenyaVaka',
sum(TenyaOlum)	as 'TenyaOlum',
sum(SekizVaka)	as 'SekizVaka',
sum(SekizOlum)	as 'SekizOlum',
sum(DokuzVaka)	as 'DokuzVaka',
sum(DokuzOlum)	as 'DokuzOlum',
sum(TrikomoniazVaka)			as 'TrikomoniazVaka',
sum(TrikomoniazKesin)			as 'TrikomoniazKesin',
sum(SkabiyezVaka)	as 'SkabiyezVaka',
sum(SkabiyezOlum)	as 'SkabiyezOlum',
sum(PedikülozvepithiriyazVaka)	as 'PedikülozvepithiriyazVaka',
sum(PedikülozvepithiriyazOlum)	as 'PedikülozvepithiriyazOlum',
sum(OnucVaka)	as 'OnucVaka',
sum(OnucOlum)	as 'OnucOlum'
from #temptable 
Group by #temptable.Cinsiyeti

union all
select 

'TOPLAM' as YasGrubu,
'T'       as Cinsiyeti,
sum(AskariyazVaka) as 'AskariyazVaka',
sum(AskariyazOlum) as 'AskariyazOlum',
sum(TrişinellozVaka)	as 'TrişinellozVaka',
sum(TrişinellozOlum)	as 'TrişinellozOlum',
sum(EnterobiyazVaka)	as 'EnterobiyazVaka',
sum(EnterobiyazOlum)   as 'EnterobiyazOlum',
sum(KancalıkurthastalıklarıVaka)   as 'KancalıkurthastalıklarıVaka',
sum(KancalıkurthastalıklarıOlum)	as 'KancalıkurthastalıklarıOlum',
sum(StrongiloidiyazVaka)	as 'StrongiloidiyazVaka',
sum(StrongiloidiyazOlum)	as 'StrongiloidiyazOlum',
sum(AltiVaka)	as 'AltiVaka',
sum(AltiOlum)	as 'AltiOlum',
sum(TenyaVaka)	as 'TenyaVaka',
sum(TenyaOlum)	as 'TenyaOlum',
sum(SekizVaka)	as 'SekizVaka',
sum(SekizOlum)	as 'SekizOlum',
sum(DokuzVaka)	as 'DokuzVaka',
sum(DokuzOlum)	as 'DokuzOlum',
sum(TrikomoniazVaka)			as 'TrikomoniazVaka',
sum(TrikomoniazKesin)			as 'TrikomoniazKesin',
sum(SkabiyezVaka)	as 'SkabiyezVaka',
sum(SkabiyezOlum)	as 'SkabiyezOlum',
sum(PedikülozvepithiriyazVaka)	as 'PedikülozvepithiriyazVaka',
sum(PedikülozvepithiriyazOlum)	as 'PedikülozvepithiriyazOlum',
sum(OnucVaka)	as 'OnucVaka',
sum(OnucOlum)	as 'OnucOlum'
from #temptable 

)
B 
order by B.yasgrubu,B.Cinsiyeti

drop table #temptable


end   ");

            }
             catch
             {


             }
             try
             {
                 SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"drop procedure [dbo].[SpForm23Raporu]");
             }

             catch
             {


             }

             try
             {
                 SharpBullet.OAL.Transaction.Instance.ExecuteNonQuery(@"CREATE procedure [dbo].[SpForm23Raporu] (@BasTarih datetime,@BitTarih datetime,@doktor bigint) as
begin
set dateformat ymd;
--declare @bastarih datetime
--declare @bittarih datetime
--declare @doktor bigint
--set @bastarih='01.01.2011'
--set @bittarih='01.01.2012'
--set @doktor=1

declare @MuayeneSayisi int
declare @SevkSayisi int
declare @BinBesYuzAlti int
declare @BinBesYuzIkiBinbesYuzArasi int


set @MuayeneSayisi=0;
set @SevkSayisi=0;
set @BinBesYuzAlti=0;
set @BinBesYuzIkiBinbesYuzArasi=0;


Select
@MuayeneSayisi=COUNT(Id)
From
Muayene M
Where
dbo.iszero(M.VekilDoktor_Id,M.doktor_Id)=@doktor
and M.MuayeneTarihi Between @bastarih and @bittarih
and M.MuayeneKapalimi=1
and M.Aktif=1 and m.IsAutoImport=0


Select
@SevkSayisi=COUNT(Id)
From
Muayene M
Where
dbo.iszero(M.VekilDoktor_Id,M.doktor_Id)=@doktor
and M.MuayeneTarihi Between @bastarih and @bittarih
and M.MuayeneDurumu='SevkEdildi'
and M.MuayeneKapalimi=1 and m.IsAutoImport=0
and M.Aktif=1





Select
@BinBesYuzAlti=
isnull(sum(Case When  1500>GBS.Agirligi Then 1 Else 0 End),0),
@BinBesYuzIkiBinbesYuzArasi=
isnull(sum(Case When  GBS.Agirligi Between 1500 and 2500 Then 1 Else 0  End),0)
From GebeSonuc GBS
Where
dbo.iszero(GBS.VekilDoktor_Id,GBS.doktor_Id)=@doktor
and GBS.IzlemTarihi  Between @bastarih and @bittarih
and GBS.GebelikSonucu='BasariliDogum'
and GBS.Aktif=1 and gbs.IsAutoImport=0

---------------------------------------------------Çoğul Doğumlar Başladı--------------------------------------------------------------------------
-----sadece canlı doğanlar mı sayılacak. Ölü doğanlar ya da Gebelik sonucu ne olanlar sayılacak...
declare @Ikiz		int			set @Ikiz	=0
declare @Ucuz		int			set @Ucuz	=0
declare @Dorduz		int			set @Dorduz	=0
declare @Besiz		int			set @Besiz	=0
declare @Altiz		int			set @Altiz	=0


Select
@Ikiz	=Sum(Case When GBS.CogulDogummu=1 and GBS.CanliDogumAdedi=2 then 1 else 0 end),
@Ucuz	=Sum(Case When GBS.CogulDogummu=1 and GBS.CanliDogumAdedi=3 then 1 else 0 end) ,
@Dorduz	=Sum(Case When GBS.CogulDogummu=1 and GBS.CanliDogumAdedi=4 then 1 else 0 end) ,
@Besiz	=Sum(Case When GBS.CogulDogummu=1 and GBS.CanliDogumAdedi=5 then 1 else 0 end) ,
@Altiz	=Sum(Case When GBS.CogulDogummu=1 and GBS.CanliDogumAdedi=6 then 1 else 0 end) 

From GebeSonuc GBS
Where
dbo.iszero(GBS.VekilDoktor_Id,GBS.doktor_Id)=@doktor
and GBS.IzlemTarihi  Between @bastarih and @bittarih
and GBS.GebelikSonucu='BasariliDogum'
and GBS.Aktif=1 and gbs.IsAutoImport=0
-----------------------------------------------------Çoğul Doğumlar Bitti--------------------------------------------------------------------------


----------------------------------------------------İzlem Sayıları---------------------------------------------------------------------------------
declare @GebeIzlemAdeti int
declare @LohusaIzleme	int
declare @BebekIzleme	int
declare @CocukIzleme	int
declare @KadinIzleme	int

set @KadinIzleme=0;
set @CocukIzleme=0;
set @BebekIzleme=0;
set @LohusaIzleme=0;
set @GebeIzlemAdeti=0;

Select
@GebeIzlemAdeti=COUNT(Id)
From 
GebeIzleme as GBI
where
dbo.iszero(GBI.VekilDoktor_Id,GBI.doktor_Id)=@doktor
and GBI.IzlemTarihi  Between @bastarih and @bittarih
and GBI.Aktif=1 and gbI.IsAutoImport=0


Select
@LohusaIzleme=COUNT(Id)
From 
LohusaIzleme as LI
where
dbo.iszero(LI.VekilDoktor_Id,LI.doktor_Id)=@doktor
and LI.IzlemTarihi  Between @bastarih and @bittarih
and LI.Aktif=1 and LI.IsAutoImport=0




Select
@BebekIzleme=COUNT(Id)
From 
BebekIzleme as BI
where
dbo.iszero(BI.VekilDoktor_Id,BI.doktor_Id)=@doktor
and BI.IzlemTarihi  Between @bastarih and @bittarih
and BI.Aktif=1 and BI.IsAutoImport=0


Select
@CocukIzleme=COUNT(Id)
From 
BebekIzleme as CI
where
dbo.iszero(CI.VekilDoktor_Id,CI.doktor_Id)=@doktor
and CI.IzlemTarihi  Between @bastarih and @bittarih
and CI.Aktif=1 and CI.IsAutoImport=0



Select
@KadinIzleme=COUNT(Id)
From 
KadinIzleme as KI
where
dbo.iszero(KI.VekilDoktor_Id,KI.doktor_Id)=@doktor
and KI.IzlemTarihi  Between @bastarih and @bittarih
and KI.Aktif=1 and KI.IsAutoImport=0
----------------------------------------------------İzlem Sayıları Bitti---------------------------------------------------------------------------------


-------------------------------------------------Gebe Durumu-----------------------------------------------
declare @GecenAydanDevredenGebe int
declare @BuAyIcindeTespitEdilenGebe int
declare @BuAyIcindeDusukYapanlarGebe int
declare @BuAyIcindeDoguranGebe int
declare @BuAyIcindeOlenGebe int
declare @AySonuGebeMevcuduGebe int



set @GecenAydanDevredenGebe=0;
set @BuAyIcindeTespitEdilenGebe=0;
set @BuAyIcindeDusukYapanlarGebe=0;
set @BuAyIcindeDoguranGebe=0;
set @BuAyIcindeOlenGebe=0;
set @AySonuGebeMevcuduGebe=0;


Select 
@GecenAydanDevredenGebe=count(GebeBaslangic.Id)
From GebeBaslangic
where
dbo.iszero(GebeBaslangic.VekilDoktor_Id,GebeBaslangic.doktor_Id)=@doktor
and GebeBaslangic.IzlemTarihi  Between dateadd(mm,-1,@bastarih) and dateadd(mm,-1,@bittarih)
and isnull(GebeBaslangic.GebelikSonuclanmaTarihi,getdate())>dateadd(mm,-1,@bittarih)
and GebeBaslangic.Aktif=1 and GebeBaslangic.IsAutoImport=0
 

Select 
@BuAyIcindeTespitEdilenGebe=count(GebeBaslangic.Id)
From GebeBaslangic
where
dbo.iszero(GebeBaslangic.VekilDoktor_Id,GebeBaslangic.doktor_Id)=@doktor
and GebeBaslangic.IzlemTarihi  Between @bastarih and @bittarih
--and GebeBaslangic.GebelikDurumu='Basladi'
and GebeBaslangic.Aktif=1 and GebeBaslangic.IsAutoImport=0





Select
@BuAyIcindeDusukYapanlarGebe=COUNT(GBS.Id)
From GebeSonuc GBS
Where
dbo.iszero(GBS.VekilDoktor_Id,GBS.doktor_Id)=@doktor
and GBS.IzlemTarihi  Between @bastarih and @bittarih
and GBS.GebelikSonucu='Dusuk'
and GBS.Aktif=1 and GBS.IsAutoImport=0





Select
@BuAyIcindeDoguranGebe=COUNT(GBS.Id)
From GebeSonuc GBS
Where
dbo.iszero(GBS.VekilDoktor_Id,GBS.doktor_Id)=@doktor
and GBS.IzlemTarihi  Between @bastarih and @bittarih
and GBS.GebelikSonucu='BasariliDogum'
and GBS.Aktif=1 and GBS.IsAutoImport=0



Select
@BuAyIcindeOlenGebe=COUNT(GBS.Id)
From GebeBaslangic GBS
inner join OlumBildirimi on OlumBildirimi.Hasta_Id=GBS.Hasta_Id
Where
dbo.iszero(OlumBildirimi.VekilDoktor_Id,OlumBildirimi.doktor_Id)=@doktor
and OlumBildirimi.OlumTarihi  Between @bastarih and @bittarih
and dbo.iszero(GBS.VekilDoktor_Id,GBS.doktor_Id)=@doktor
and GBS.Aktif=1 and GBS.IsAutoImport=0



set @AySonuGebeMevcuduGebe=(ISNULL(@GecenAydanDevredenGebe,0)+ISNULL(@BuAyIcindeTespitEdilenGebe,0))
- (ISNULL(@BuAyIcindeDusukYapanlarGebe,0)+ISNULL(@BuAyIcindeDoguranGebe,0)+ISNULL(@BuAyIcindeOlenGebe,0))
-------------------------------------------------Gebe Durumu Bitti-----------------------------------------------




-------------------------------------------------Bebek Durumu-----------------------------------------------
declare @GecenAydanDevredenBebek int
declare @BuAyIcindeTespitEdilenBebek int
declare @BuAyIcindeCanlıDoganBebek int
declare @BuAyIcindeOlenBebek int
declare @BuAyBebekliktenCikan int
declare @AySonuBebekMevcudu int
declare @OluDoganBebekSayisi int




set @GecenAydanDevredenBebek=0;
set @BuAyIcindeTespitEdilenBebek=0;
set @BuAyIcindeCanlıDoganBebek=0;
set @BuAyIcindeOlenBebek=0;
set @BuAyBebekliktenCikan=0;
set @AySonuBebekMevcudu=0;
set @OluDoganBebekSayisi=0;

Select 
@GecenAydanDevredenBebek=count(H.Id)
From Hasta H
where
H.Doktor_Id=@doktor
and isnull(H.BeyanDogumTarihi,H.DogumTarihi)  Between dateadd(mm,-1,@bastarih) and dateadd(mm,-1,@bittarih)
and 365>datepart(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))
and H.Aktif=1
 

Select 
@BuAyIcindeTespitEdilenBebek=count(H.Id)
From Hasta H
where
H.Doktor_Id=@doktor
and isnull(H.BeyanDogumTarihi,H.DogumTarihi)  Between @bastarih and @bittarih
and 365>datepart(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))
and H.Aktif=1





Select
@BuAyIcindeCanlıDoganBebek=COUNT(H.Id)
From Hasta H
left join OlumBildirimi on OlumBildirimi.Hasta_Id=H.Id
where
H.Doktor_Id=@doktor
and isnull(H.BeyanDogumTarihi,H.DogumTarihi)  Between @bastarih and @bittarih
and 365>datepart(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))
and H.Aktif=1
and OlumBildirimi.Id is null





Select
@BuAyIcindeOlenBebek=COUNT(H.Id)
From Hasta H
inner join OlumBildirimi on OlumBildirimi.Hasta_Id=H.Id
where
H.Doktor_Id=@doktor
and OlumBildirimi.OlumTarihi  Between @bastarih and @bittarih
and 365>datepart(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))
and H.Aktif=1




Select
@BuAyBebekliktenCikan=COUNT(H.Id)
From Hasta H
where
H.Doktor_Id=@doktor
and isnull(H.BeyanDogumTarihi,H.DogumTarihi)  Between @bastarih and @bittarih
and 395>datepart(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))
and H.Aktif=1

Select
@OluDoganBebekSayisi=COUNT(GBS.Id)
From GebeSonuc GBS
Where
dbo.iszero(GBS.VekilDoktor_Id,GBS.doktor_Id)=@doktor
--and GBS.IzlemTarihi  Between @bastarih and @bittarih
and GBS.GebelikSonucu='OluDogum'
and GBS.Aktif=1 and GBS.IsAutoImport=0


set @AySonuBebekMevcudu=(ISNULL(@GecenAydanDevredenBebek,0)+ISNULL(@BuAyIcindeTespitEdilenBebek,0)+ISNULL(@BuAyIcindeCanlıDoganBebek,0))
- (ISNULL(@BuAyIcindeOlenBebek,0)+ISNULL(@BuAyBebekliktenCikan,0))
-------------------------------------------------Bebek Durumu Bitti-----------------------------------------------



----------------------------------------------Lohusa Durumu Başladı--------------------------------------------------


declare @GecenAydanDevredenLohusa int
declare @BuAyIcindeTespitEdilenLohusa int
declare @BuAyLohusaligaGecen int
declare @BuAyIcindeOlenLohusa int
declare @BuAyLohusaliktanCikan int
declare @AySonuLohusaMevcudu int



set @GecenAydanDevredenLohusa=0;
set @BuAyIcindeTespitEdilenLohusa=0;
set @BuAyLohusaligaGecen=0;
set @BuAyIcindeOlenLohusa=0;
set @BuAyLohusaliktanCikan=0;
set @AySonuLohusaMevcudu=0;


Select 
@GecenAydanDevredenLohusa=count(GB.Id)
From GebeBaslangic GB
where
dbo.iszero(GB.VekilDoktor_Id,GB.doktor_Id)=@doktor
and GB.GebelikSonuclanmaTarihi  Between dateadd(mm,-1,@bastarih) and dateadd(mm,-1,@bittarih)
and GB.GebelikDurumu='Bitti'
and DATEDIFF(dd,GB.GebelikSonuclanmaTarihi,GETDATE())<43
and GB.Aktif=1 and GB.IsAutoImport=0



 
            
          


Select 
@BuAyIcindeTespitEdilenLohusa=count(GB.Id)
From GebeBaslangic GB
where
dbo.iszero(GB.VekilDoktor_Id,GB.doktor_Id)=@doktor
and GB.GebelikSonuclanmaTarihi  Between @bastarih and @bittarih
and GB.GebelikDurumu='Bitti'
and DATEDIFF(dd,GB.GebelikSonuclanmaTarihi,GETDATE())<43
and GB.Aktif=1 and GB.IsAutoImport=0





Select
@BuAyLohusaligaGecen=COUNT(GB.Id)
From GebeBaslangic GB
where
dbo.iszero(GB.VekilDoktor_Id,GB.doktor_Id)=@doktor
and GB.GebelikSonuclanmaTarihi  Between @bastarih and @bittarih
and GB.GebelikDurumu='Bitti'
and DATEDIFF(dd,GB.GebelikSonuclanmaTarihi,GETDATE())<43
and GB.Aktif=1 and GB.IsAutoImport=0





Select
@BuAyIcindeOlenLohusa=COUNT(distinct GB.Hasta_Id)
From GebeBaslangic GB 
inner join OlumBildirimi on OlumBildirimi.Hasta_Id=GB.Hasta_Id
where
dbo.iszero(GB.VekilDoktor_Id,GB.doktor_Id)=@doktor
and GB.GebelikSonuclanmaTarihi  Between @bastarih and @bittarih
and GB.GebelikDurumu='Bitti'
and DATEDIFF(dd,GB.GebelikSonuclanmaTarihi,GETDATE())<43
and GB.Aktif=1 and GB.IsAutoImport=0




Select
@BuAyLohusaliktanCikan=COUNT(GB.Id)
From GebeBaslangic GB
where
dbo.iszero(GB.VekilDoktor_Id,GB.doktor_Id)=@doktor
and GB.GebelikSonuclanmaTarihi  Between dateadd(mm,-1,@bastarih) and dateadd(mm,-1,@bittarih)
and GB.GebelikDurumu='Bitti'
and DATEDIFF(dd,GB.GebelikSonuclanmaTarihi,GETDATE())>43
and GB.Aktif=1 and GB.IsAutoImport=0




set @AySonuLohusaMevcudu=(ISNULL(@GecenAydanDevredenLohusa,0)+ISNULL(@BuAyIcindeTespitEdilenLohusa,0)+ISNULL(@BuAyLohusaligaGecen,0))
- (ISNULL(@BuAyIcindeOlenLohusa,0)+ISNULL(@BuAyLohusaliktanCikan,0))

----------------------------------------------Lohusa Durumu Bitti----------------------------------------------------


-------------------------------------------------Çocuk Durumu Başladı-----------------------------------------------
declare @GecenAydanDevredenCocuk int
declare @BuAyIcindeTespitEdilenCocuk int
declare @BuAyCocuklugaGecen int
declare @BuAyIcindeOlenCocuk int
declare @BuAyCocukluktanCikan int
declare @AySonuCocukMevcudu int



set @GecenAydanDevredenCocuk=0;
set @BuAyIcindeTespitEdilenCocuk=0;
set @BuAyCocuklugaGecen=0;
set @BuAyIcindeOlenCocuk=0;
set @BuAyCocukluktanCikan=0;
set @AySonuCocukMevcudu=0;


Select 
@GecenAydanDevredenCocuk=count(H.Id)
From Hasta H
where
H.doktor_Id=@doktor
and DATEPART(dd,dateadd(mm,-1,isnull(H.BeyanDogumTarihi,H.DogumTarihi)))>=365 
and H.Aktif=1



Select 
@BuAyIcindeTespitEdilenCocuk=count(H.Id)
From Hasta H
where
H.doktor_Id=@doktor
and DATEPART(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))>=365 
and H.Aktif=1





Select
@BuAyCocuklugaGecen=COUNT(H.Id)
From Hasta H
where
H.doktor_Id=@doktor
and DATEPART(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))>=365 
and H.Aktif=1





Select
@BuAyIcindeOlenCocuk=COUNT(distinct H.Id)
From Hasta H 
inner join OlumBildirimi on OlumBildirimi.Hasta_Id=H.Id
where
H.doktor_Id=@doktor
and DATEPART(dd,isnull(H.BeyanDogumTarihi,H.DogumTarihi))>=365 
and OlumBildirimi.OlumTarihi Between @bastarih and @bittarih
and H.Aktif=1




Select
@BuAyCocukluktanCikan=COUNT(H.Id)
From Hasta H
where
H.doktor_Id=@doktor
and datepart(dd,dateadd(dd,30,isnull(H.BeyanDogumTarihi,H.DogumTarihi)))>=365 
and H.Aktif=1





set @AySonuCocukMevcudu=(ISNULL(@GecenAydanDevredenCocuk,0)+ISNULL(@BuAyIcindeTespitEdilenCocuk,0)+ISNULL(@BuAyCocuklugaGecen,0))
- (ISNULL(@BuAyIcindeOlenCocuk,0)+ISNULL(@BuAyCocukluktanCikan,0))
-------------------------------------------------Çocuk Durumu Bitti-----------------------------------------------

declare @CerrahiMudahale 				int	set @CerrahiMudahale				=0
declare @OtopsiSayisi	 				int	set @OtopsiSayisi					=0
declare @AdliRaporSayisi 				int	set @AdliRaporSayisi				=0
declare @AdliToplam		 				int	set @AdliToplam						=@OtopsiSayisi + @AdliRaporSayisi
declare @DefinRuhsatiSayisi				int	set @DefinRuhsatiSayisi				=0

declare @PrematureBebekSayisi			int	set	@PrematureBebekSayisi			=0
declare @Hastahanede					int	set	@Hastahanede		 			=0
declare @Hekim							int set	@Hekim							=0
declare @Ebe							int set	@Ebe							=0
declare @DigerSP						int set	@DigerSP						=0
declare @SPYOlmadan						int set	@SPYOlmadan						=0
declare @Toplam							int set	@Toplam							=@Hekim + @Ebe + @DigerSP + @SPYOlmadan 
----------------------------------Tahlil Tanımlamaları-------------------------------------------------------------------
declare @Idrar							int set	@Idrar							=0
declare @Kan							int set	@Kan							=0
declare @Diski							int set	@Diski							=0
declare @Seroloji						int set	@Seroloji						=0
declare @SitmaKani						int set	@SitmaKani						=0
declare @GebelikTesti					int set	@GebelikTesti					=0
declare @Diger							int	set	@Diger							=0 	

Select
-------------- Muayene bilgileri-----------------------
@MuayeneSayisi as MuayeneSayisi,@SevkSayisi as SevkSayisi ,@CerrahiMudahale as CerrahiMudahale,@OtopsiSayisi as OtopsiSayisi,@AdliRaporSayisi as AdliRaporSayisi,@AdliToplam as AdliToplam,@DefinRuhsatiSayisi as DefinRuhsatiSayisi,
-------------- Bebek------------------------
@BinBesYuzAlti as BinBesYuzAlti,@BinBesYuzIkiBinbesYuzArasi as BinBesYuzIkiBinbesYuzArasi,@PrematureBebekSayisi as PrematureBebekSayisi,@Hastahanede as Hastahanede,@Hekim as Hekim,@Ebe as Ebe,@DigerSP as DigerSP,@SPYOlmadan as SPYOlmadan,@Toplam as Toplam,
-------------- Tahlil------------------------
@Idrar as Idrar,@Kan as Kan,@Diski as Diski,@Seroloji as Seroloji,@SitmaKani as SitmaKani,@GebelikTesti as GebelikTesti,@Diger as Diger,
-------------- İzlem Sayıları-------------------------
@GebeIzlemAdeti as GebeIzlemAdeti,@LohusaIzleme as LohusaIzleme,@BebekIzleme as BebekIzleme,@CocukIzleme as CocukIzleme,@KadinIzleme as KadinIzleme,
---------------Gebe Durumu----------------------------
@GecenAydanDevredenGebe as GecenAydanDevredenGebe,@BuAyIcindeTespitEdilenGebe as BuAyIcindeTespitEdilenGebe,@BuAyIcindeDusukYapanlarGebe as BuAyIcindeDusukYapanlarGebe ,@BuAyIcindeDoguranGebe as BuAyIcindeDoguranGebe,@BuAyIcindeOlenGebe as BuAyIcindeOlenGebe,@AySonuGebeMevcuduGebe as AySonuGebeMevcuduGebe,
----------------Bebek Durumu--------------------------
@GecenAydanDevredenBebek as GecenAydanDevredenBebek,@BuAyIcindeTespitEdilenBebek as BuAyIcindeTespitEdilenBebek,@BuAyIcindeCanlıDoganBebek as BuAyIcindeCanlıDoganBebek,@BuAyIcindeOlenBebek as BuAyIcindeOlenBebek,@BuAyBebekliktenCikan as BuAyBebekliktenCikan,@AySonuBebekMevcudu as AySonuBebekMevcudu,@OluDoganBebekSayisi as OluDoganBebekSayisi,
----------------Lohusa Durumu------------------------- 
@GecenAydanDevredenLohusa as GecenAydanDevredenLohusa,@BuAyIcindeTespitEdilenLohusa as BuAyIcindeTespitEdilenLohusa,@BuAyLohusaligaGecen as BuAyLohusaligaGecen,@BuAyIcindeOlenLohusa as BuAyIcindeOlenLohusa,@BuAyLohusaliktanCikan as BuAyLohusaliktanCikan,@AySonuLohusaMevcudu as AySonuLohusaMevcudu,
----------------Çocuk Durumu--------------------------
@GecenAydanDevredenCocuk as GecenAydanDevredenCocuk,@BuAyIcindeTespitEdilenCocuk as BuAyIcindeTespitEdilenCocuk,@BuAyCocuklugaGecen as BuAyCocuklugaGecen,@BuAyIcindeOlenCocuk as BuAyIcindeOlenCocuk,@BuAyCocukluktanCikan as BuAyCocukluktanCikan,@AySonuCocukMevcudu as AySonuCocukMevcudu,

------Çoğul Doğumlar----------------------------------
@Ikiz as Ikiz,@Ucuz as Ucuz,@Dorduz as Dorduz,@Besiz as Besiz,@Altiz as Altiz	







end 
");
             }
             catch 
             {
                 
 
             } 
        }
    }
}
