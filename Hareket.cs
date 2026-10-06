using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GelirGiderTakip
{
    public class Hareket
    {
        public Hareket(int hareketId, string aciklama, decimal tutar, string tur, DateTime tarih, string kategory)
        {
            HareketId = hareketId;
            Aciklama = aciklama;
            Tutar = tutar;
            Tur = tur;
            Tarih = tarih;
            Kategori = kategory;
        }

    public int HareketId { get; set; }
    public string Aciklama { get; set; }
    public decimal Tutar { get; set; }
    public string Tur { get; set; }
    public DateTime Tarih { get; set; }
    public string Kategori { get; set; }
    public object Id { get; internal set; }



        public override string ToString()
        {
            return $"HareketId: {HareketId}, Aciklama: {Aciklama}, Tutar: {Tutar}, Tur: {Tur}, Tarih: {Tarih}, Kategory: {Kategori}";
        }
    }


    }
