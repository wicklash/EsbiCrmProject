using System.ComponentModel.DataAnnotations;

namespace CRMProject.Services.Models;

public class CariViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Cari kodu zorunludur.")]
    [StringLength(20, ErrorMessage = "Cari kodu en fazla 20 karakter olabilir.")]
    [Display(Name = "Cari Kodu")]
    public string CariKodu { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ünvan zorunludur.")]
    [StringLength(200, ErrorMessage = "Ünvan en fazla 200 karakter olabilir.")]
    [Display(Name = "Ünvan")]
    public string Ünvan { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cari tipi zorunludur.")]
    [Display(Name = "Cari Tipi")]
    public string CariTipi { get; set; } = string.Empty;

    [Display(Name = "Vergi Dairesi")]
    public string? VergiDairesi { get; set; }

    [Display(Name = "Vergi No")]
    public string? VergiNo { get; set; }

    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    [Display(Name = "Telefon")]
    public string? Telefon { get; set; }

    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "E-Posta")]
    public string? EPosta { get; set; }

    [Display(Name = "Adres")]
    public string? Adres { get; set; }

    [Display(Name = "İl")]
    public string? İl { get; set; }

    [Display(Name = "İlçe")]
    public string? İlçe { get; set; }
}
