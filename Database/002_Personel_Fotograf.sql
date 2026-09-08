-- Only needed when the existing Personel table has no compatible binary photo column.
-- Run in the BKS database after reviewing; no existing photo data is removed.
IF OBJECT_ID(N'dbo.Personel', N'U') IS NULL
    THROW 50001, N'Personel tablosu bulunamadı. Doğru veritabanını seçin.', 1;

IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id=OBJECT_ID(N'dbo.Personel')
      AND name IN (N'Photo',N'photo',N'photobinary',N'PhotoBinary')
      AND system_type_id IN (34,165,173)
)
BEGIN
    IF COL_LENGTH(N'dbo.Personel', N'Photo') IS NOT NULL
        THROW 50002, N'Photo alanı var fakat ikili veri türünde değil. Dönüşüm yönetici tarafından incelenmeli.', 1;
    ALTER TABLE dbo.Personel ADD Photo VARBINARY(MAX) NULL;
END;
-- AddPersonel already receives @photo in the supplied client.
-- If this script added Photo, verify that the existing procedure writes @photo to it.
