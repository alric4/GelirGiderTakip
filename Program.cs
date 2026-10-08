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
                                (string)reader["Kategori"]
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
                    Console.Write("Açıklama: ");
                    string aciklama = Console.ReadLine();
                    Console.Write("Tutar: ");
                    decimal tutar = decimal.Parse(Console.ReadLine());
                    Console.Write("Tür (Gelir/Gider): ");
                    string tur = Console.ReadLine();
                    Console.Write("Tarih (yyyy-MM-dd): ");
                    DateTime tarih = DateTime.Parse(Console.ReadLine());
                    Console.Write("Kategori: ");
                    string kategori = Console.ReadLine();
                    Hareket hareket = new Hareket(0, aciklama, tutar, tur, tarih, kategori);
                    HareketEkle(hareket);
                }
                else if(islem==3)
                {
                    Console.WriteLine("Silme");
                    Console.Write("Silinecek Hareketin Id'si: ");
                    int hareketId = int.Parse(Console.ReadLine());
                    HareketSil(hareketId);
                    Console.WriteLine("Hareket başarıyla silindi.");
                }
                else if (islem == 4)
                {
                    Console.Write("Güncellenecek Hareketin Id'si: ");
                    int hareketId = int.Parse(Console.ReadLine());
                    Console.Write("Yeni Açıklama: ");
                    string aciklama = Console.ReadLine();
                    Console.Write("Yeni Tutar: ");
                    decimal tutar = decimal.Parse(Console.ReadLine());
                    Console.Write("Yeni Tür (Gelir/Gider): ");
                    string tur = Console.ReadLine();
                    Console.Write("Yeni Tarih (yyyy-MM-dd): ");
                    DateTime tarih = DateTime.Parse(Console.ReadLine());
                    Console.Write("Yeni Kategori: ");
                    string kategori = Console.ReadLine();
                    Hareket hareket = new Hareket(hareketId, aciklama, tutar, tur, tarih, kategori);
                    HareketGuncelle(hareket);
                }
                else if (islem == 5)
                {
                    OzetGoster();
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
