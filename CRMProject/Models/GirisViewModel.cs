using System.ComponentModel.DataAnnotations;

namespace CRMProject.Models;

public class GirisViewModel
{
    [Required(ErrorMessage = "E-posta adresi zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "E-Posta")]
    public string EPosta { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Şifre { get; set; } = string.Empty;

    [Display(Name = "Beni Hatırla")]
    public bool BeniHatırla { get; set; }

    public string? ReturnUrl { get; set; }
}
