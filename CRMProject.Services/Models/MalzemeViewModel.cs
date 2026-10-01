using System.ComponentModel.DataAnnotations;

namespace CRMProject.Services.Models;

public class MalzemeViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Malzeme kodu zorunludur.")]
    [StringLength(30, ErrorMessage = "Malzeme kodu en fazla 30 karakter olabilir.")]
    [Display(Name = "Malzeme Kodu")]
    public string MalzemeKodu { get; set; } = string.Empty;

    [Required(ErrorMessage = "Malzeme adı zorunludur.")]
    [StringLength(200, ErrorMessage = "Malzeme adı en fazla 200 karakter olabilir.")]
    [Display(Name = "Malzeme Adı")]
    public string MalzemeAdı { get; set; } = string.Empty;

    [Display(Name = "Kategori")]
    public string? Kategori { get; set; }

    [Required(ErrorMessage = "Birim zorunludur.")]
    [Display(Name = "Birim")]
    public string Birim { get; set; } = string.Empty;   // Adet, Kg, Lt, Mt …

    [Range(0, double.MaxValue, ErrorMessage = "Satış fiyatı negatif olamaz.")]
    [Display(Name = "Satış Fiyatı")]
    public decimal SatışFiyatı { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Alış fiyatı negatif olamaz.")]
    [Display(Name = "Alış Fiyatı")]
    public decimal AlışFiyatı { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
    [Display(Name = "Stok Miktarı")]
    public decimal StokMiktarı { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Kritik stok negatif olamaz.")]
    [Display(Name = "Kritik Stok")]
    public decimal KritikStok { get; set; }

    [Display(Name = "Açıklama")]
    public string? Açıklama { get; set; }
}
