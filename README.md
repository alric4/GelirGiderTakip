# Gelir Gider Takip

C# ve SQL Server (ADO.NET) ile yazılmış, konsol tabanlı kişisel gelir-gider takip uygulaması.

## Özellikler

- Gelir veya gider kaydı ekleme
- Kayıtları listeleme
- Kayıt güncelleme
- Kayıt silme
- - Toplam gelir, toplam gider ve bakiye hesabı
  - Türe ve kategoriye göre filtreleme

## Kullanılan Teknolojiler

- C# (.NET konsol uygulaması)
- SQL Server
- ADO.NET (SqlConnection, SqlCommand, SqlDataReader)

## Veritabanı

Veritabanı adı: `GelirGiderDb`, tablo adı: `Hareketler`

| Sütun | Tip |
|---|---|
| HareketId | int (identity, birincil anahtar) |
| Aciklama | nvarchar(100) |
| Tutar | decimal(18,2) |
| Tur | nvarchar(20) |
| Tarih | date |
| Kategori | nvarchar(50) |

## Çalıştırmak İçin

1. SQL Server'da `GelirGiderDb` veritabanını ve yukarıdaki `Hareketler` tablosunu oluştur.
2. `Program.cs` içindeki bağlantı cümlesinde (`connectionString`) sunucu adını kendi bilgisayarına göre düzenle.
3. Projeyi Visual Studio ile açıp çalıştır.

## Yapılacaklar
- Hatalı girişlerin kontrolü
