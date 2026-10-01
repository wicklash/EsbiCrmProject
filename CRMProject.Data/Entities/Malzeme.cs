using System;

namespace CRMProject.Data.Entities;

public class Malzeme
{
    public int Id { get; set; }
    public string MalzemeKodu { get; set; } = string.Empty;  // Zorunlu, benzersiz
    public string MalzemeAdı { get; set; } = string.Empty;   // Zorunlu
    public string? Kategori { get; set; }
    public string Birim { get; set; } = string.Empty;        // Adet, Kg, Lt, Mt …
    public decimal SatışFiyatı { get; set; }
    public decimal AlışFiyatı { get; set; }
    public decimal StokMiktarı { get; set; }
    public decimal KritikStok { get; set; }
    public string? Açıklama { get; set; }
    public bool AktifMi { get; set; } = true;
    public DateTime OluşturmaTarihi { get; set; } = DateTime.Now;
    public DateTime? GüncellemeTarihi { get; set; }
}