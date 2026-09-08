/* BKS Ribbon v1.1.0 — Mevcut BKS veritabanında çalıştırılır.
   Mevcut kayıtlar silinmez veya otomatik olarak bir şirkete atanmaz.
   Çok şirketli kayıtlarda eski satırların sahipliği şirket yöneticisi tarafından eşlenmelidir.
   Aynı betik tekrar çalıştırılabilir. İstemci bu betiği kendiliğinden çalıştırmaz. */
SET XACT_ABORT ON;
BEGIN TRANSACTION;
DECLARE @objectId int = OBJECT_ID(N'GelirGider');
IF @objectId IS NULL
    THROW 50001, N'GelirGider tablosu bulunamadı. Doğru veritabanı ve varsayılan şema ile bağlanın.', 1;
DECLARE @table nvarchar(520) = QUOTENAME(OBJECT_SCHEMA_NAME(@objectId)) + N'.' + QUOTENAME(OBJECT_NAME(@objectId));
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id=@objectId AND name=N'SirketId')
    EXEC(N'ALTER TABLE '+@table+N' ADD SirketId uniqueidentifier NULL;');
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=@objectId AND name=N'IX_GelirGider_SirketId')
    EXEC(N'CREATE INDEX IX_GelirGider_SirketId ON '+@table+N'(SirketId);');
COMMIT TRANSACTION;
EXEC(N'SELECT COUNT(*) AS SirketEslemesiBekleyenKayit FROM '+@table+N' WHERE SirketId IS NULL;');
/* Sahipliği doğrulandıktan sonra yetkili yönetici, uygun satır koşuluyla günceller:
   UPDATE <gerçek şema>.GelirGider SET SirketId=<doğrulanmış şirket GUID>
   WHERE <yalnız o şirkete ait kayıtları belirleyen koşul> AND SirketId IS NULL;
*/
