using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GelirGiderTakip
{
    internal class Program
    {
    static void BaglantiTest()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS2025;Initial Catalog=GelirGiderDb;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Veritabanına bağlantı başarılı!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Veritabanına bağlantı başarısız: " + ex.Message);
                }
            }
        }

        static void HareketListele()
        {
            string connectionString = "Data Source=.\\SQLEXPRESS2025;Initial Catalog=GelirGiderDb;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Hareketler";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    List<Hareket> hareketler = new List<Hareket>();
                    Console.WriteLine("Hareketler Listesi:");
                    while (reader.Read())
                    {
                        int hareketId = reader.GetInt32(0);
                        string aciklama = reader.GetString(1).Trim();
                        decimal tutar = reader.GetDecimal(2);
                        string tur = reader.GetString(3).Trim();
                        DateTime tarih = reader.GetDateTime(4);
                        string kategory = reader.GetString(5).Trim();

                        Hareket hareket = new Hareket(hareketId, aciklama, tutar, tur, tarih, kategory);
                        {
                            hareket.HareketId = hareketId;
                            hareket.Aciklama = aciklama;
                            hareket.Tutar = tutar;
                            hareket.Tur = tur;
                            hareket.Tarih = tarih;
                            hareket.Kategori = kategory;
                        };
                        hareketler.Add(hareket);

                    }
                    reader.Close();

                    foreach (var hareket in hareketler)
                    {
                        Console.WriteLine($"HareketId: {hareket.HareketId}, Açıklama: {hareket.Aciklama}, Tutar: {hareket.Tutar}, Tür: {hareket.Tur}, Tarih: {hareket.Tarih}, Kategori: {hareket.Kategori}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hareketleri listeleme başarısız: " + ex.Message);
                }
            }
        }
        static void HareketEkle(Hareket hareket)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS2025;Initial Catalog=GelirGiderDb;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Hareketler (Aciklama, Tutar, Tur, Tarih, Kategori) VALUES (@Aciklama, @Tutar, @Tur, @Tarih, @Kategori)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Aciklama", hareket.Aciklama);
                    command.Parameters.AddWithValue("@Tutar", hareket.Tutar);
                    command.Parameters.AddWithValue("@Tur", hareket.Tur);
                    command.Parameters.AddWithValue("@Tarih", hareket.Tarih);
                    command.Parameters.AddWithValue("@Kategori", hareket.Kategori);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} satır eklendi.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata: " + ex.Message);
                    }
                }
            }
        }
        static void HareketSil(int hareketId)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS2025;Initial Catalog=GelirGiderDb;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Hareketler WHERE HareketId = @HareketId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HareketId", hareketId);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} satır silindi.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata: " + ex.Message);
                    }
                }
            }
        }
        static void HareketGuncelle(Hareket hareket)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS2025;Initial Catalog=GelirGiderDb;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Hareketler SET Aciklama = @Aciklama, Tutar = @Tutar, Tur = @Tur, Tarih = @Tarih, Kategori = @Kategori WHERE HareketId = @HareketId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HareketId", hareket.HareketId);
                    command.Parameters.AddWithValue("@Aciklama", hareket.Aciklama);
                    command.Parameters.AddWithValue("@Tutar", hareket.Tutar);
                    command.Parameters.AddWithValue("@Tur", hareket.Tur);
                    command.Parameters.AddWithValue("@Tarih", hareket.Tarih);
                    command.Parameters.AddWithValue("@Kategori", hareket.Kategori);
                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} satır güncellendi.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata: " + ex.Message);
                    }
                }
            }
        }
        static List<Hareket> HareketleriGetir()
        {
            List<Hareket> hareketler = new List<Hareket>();
            string connectionString = "Data Source=.\\SQLEXPRESS2025;Initial Catalog=GelirGiderDb;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Hareketler";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Hareket hareket = new Hareket(
                                (int)reader["HareketId"],
                                (string)reader["Aciklama"],
                                (decimal)reader["Tutar"],
                                (string)reader["Tur"].ToString().Trim(),
                                (DateTime)reader["Tarih"],
                                (string)reader["Kategori"].ToString().Trim()
                            );
                            hareketler.Add(hareket);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata: " + ex.Message);
                    }
                }
            }
            return hareketler;
        }
        static void OzetGoster()
        {
            List<Hareket> hareketler = HareketleriGetir();
            decimal toplamGelir = hareketler.Where(h => h.Tur == "Gelir").Sum(h => h.Tutar);
            decimal toplamGider = hareketler.Where(h => h.Tur == "Gider").Sum(h => h.Tutar);
            decimal bakiye = toplamGelir - toplamGider;
            Console.WriteLine($"Toplam Gelir: {toplamGelir}");
            Console.WriteLine($"Toplam Gider: {toplamGider}");
            Console.WriteLine($"Bakiye: {bakiye}");
        }
        static void TureGoreListele(string tur)
        {
            List<Hareket> hareketler = HareketleriGetir();
            bool bulundu = false;
            var filtrelenmisHareketler = hareketler.Where(h => h.Tur.Equals(tur, StringComparison.CurrentCultureIgnoreCase)).ToList();
            Console.WriteLine($"{tur} Hareketler Listesi:");
            foreach (var hareket in filtrelenmisHareketler)
            {
                Console.WriteLine(hareket);
                bulundu = true;
            }
            if(bulundu == false)
            {
                Console.WriteLine($"{tur} türünde hareket bulunamadı.");
            }
        }
        static public void KategoriyeGoreListele(string kategori)
        {
            List<Hareket> hareketler = HareketleriGetir();
            var filtrelenmisHareketler = hareketler.Where(h => h.Kategori.Equals(kategori, StringComparison.CurrentCultureIgnoreCase)).ToList();

            if (filtrelenmisHareketler.Count == 0)
            {
                Console.WriteLine($"{kategori} kategorisine ait hareket bulunamadı.");
            }
            else
            { 
                Console.WriteLine($"{kategori} Kategorisine Ait Hareketler Listesi:");
                foreach (var hareket in filtrelenmisHareketler)
                {
                    Console.WriteLine(hareket);
                }
            }
        }
        static decimal TutarOku()
        {
            while (true)
            {
                Console.Write("Tutar: ");
                string girdi = Console.ReadLine();
                girdi = girdi.Replace('.', ',');
                if (decimal.TryParse(girdi, out decimal tutar) && tutar > 0)
                {
                    return tutar;
                }
                Console.WriteLine("Hatalı tutar! Pozitif bir sayı girin (örnek: 150,50)");
            }
        }
        static DateTime TarihOku()
        {
            while (true)
            {
                Console.Write("Tarih (yyyy-MM-dd): ");
                string girdi = Console.ReadLine();
                if (DateTime.TryParse(girdi, out DateTime tarih))
                {
                    return tarih;
                }
                Console.WriteLine("Hatalı tarih! Örnek: 2026-10-10");
            }
        }
        static string TurOku()
        {
            while (true)
            {
                Console.Write("Tür (Gelir/Gider): ");
                string tur = Console.ReadLine();
                if (tur.Equals("Gelir", StringComparison.CurrentCultureIgnoreCase) || tur.Equals("Gider", StringComparison.CurrentCultureIgnoreCase))
                {
                    return tur;
                }
                Console.WriteLine("Hatalı tür! 'Gelir' veya 'Gider' girin.");
            }
        }
        static int IdOku()
        {
            while (true)
            {
                Console.Write("Hareket Id: ");
                string girdi = Console.ReadLine();
                if (int.TryParse(girdi, out int id) && id > 0)
                {
                    return id;
                }
                Console.WriteLine("Hatalı Id! Pozitif bir sayı girin.");
            }
        }
        static string MetinOku(string mesaj)
        {
            while (true)
            {
                Console.Write(mesaj);
                string girdi = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(girdi))
                {
                    return girdi.Trim();
                }
                Console.WriteLine("Hatalı giriş! Boş bir değer giremezsiniz.");
            }
        }
        
        static void Main(string[] args)
        {
        
            int islem;
            do
            {
                    Console.WriteLine("0-Çıkış");
                    Console.WriteLine("1-Hareketleri Listele");
                    Console.WriteLine("2-Hareket Ekle");
                    Console.WriteLine("3-Hareket Sil");
                    Console.WriteLine("4-Hareket Güncelle");
                    Console.WriteLine("5-Özet Göster");
                    Console.WriteLine("6-Türe Göre Listele");
                    Console.WriteLine("7-Kategoriye Göre Listele");
                    Console.Write("Yapmak istediğiniz işlemi giriniz:");
                try
                {
                    islem = int.Parse(Console.ReadLine());
                }
                catch
                {
                    islem = -1;
                }
                if (islem == 1)
                {
                    Console.WriteLine();
                    HareketListele();
                }
                else if(islem == 2)
                {
                    Console.WriteLine();
                    string aciklama = MetinOku("Açıklama: ");
                    decimal tutar = TutarOku();
                    string tur = TurOku();
                    DateTime tarih = TarihOku();
                    string kategori = MetinOku("Kategori: ");
                    Hareket hareket = new Hareket(0, aciklama, tutar, tur, tarih, kategori);
                    HareketEkle(hareket);
                }
                else if(islem==3)
                {
                    Console.WriteLine("Silme");
                    int hareketId = IdOku();
                    HareketSil(hareketId);
                    Console.WriteLine("Hareket başarıyla silindi.");
                }
                else if (islem == 4)
                {
                    int hareketId = IdOku();
                    string aciklama = MetinOku("Açıklama: ");
                    decimal tutar = TutarOku();
                    string tur = TurOku();
                    DateTime tarih = TarihOku();
                    string kategori = MetinOku("Kategori: ");
                    Hareket hareket = new Hareket(hareketId, aciklama, tutar, tur, tarih, kategori);
                    HareketGuncelle(hareket);
                }
                else if (islem == 5)
                {
                    OzetGoster();
                }
                else if (islem == 6)
                {
                    Console.Write("Listelemek istediğiniz türü giriniz (Gelir/Gider): ");
                    string tur = Console.ReadLine();
                    TureGoreListele(tur);
                }
                else if (islem == 7)
                {
                    Console.Write("Listelemek istediğiniz kategoriyi giriniz: ");
                    string kategori = Console.ReadLine();
                    KategoriyeGoreListele(kategori);
                }
                else if (islem == 0)
                {
                    Console.WriteLine("Çıkış Yapıldı");
                }
                else
                {
                    Console.WriteLine("Hatalı Tuşlama Yaptınız!");
                }
                

                
                

            } while (islem != 0);

            Console.Read();
        }
    }
}
