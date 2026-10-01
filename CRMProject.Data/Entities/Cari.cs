using System;

namespace CRMProject.Data.Entities;

public class Cari
{
    public int Id { get; set; }
    public string CariKodu { get; set; } = string.Empty;  // Zorunlu, benzersiz
    public string Ünvan { get; set; } = string.Empty;     // Zorunlu
    public string CariTipi { get; set; } = string.Empty;  // Müşteri | Tedarikçi | Her İkisi
    public string? VergiDairesi { get; set; }
    public string? VergiNo { get; set; }
    public string? Telefon { get; set; }
    public string? E_posta { get; set; }
    public string? Adres { get; set; }
    public string? İl { get; set; }
    public string? İlçe { get; set; }
    public bool AktifMi { get; set; } = true;
    public DateTime OluşturmaTarihi { get; set; } = DateTime.Now;
    public DateTime? GüncellemeTarihi { get; set; }

    // Helper property to map EPosta to E_posta seamlessly
    public string? EPosta
    {
        get => E_posta;
        set => E_posta = value;
    }
}