using System.ComponentModel.DataAnnotations;

namespace CRMProject.Models;

public class UserItemViewModel
{
    public string Id { get; set; } = string.Empty;
    public string AdıSoyadı { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Görev { get; set; } = string.Empty;
    public bool Aktif { get; set; }
    public string Roles { get; set; } = string.Empty;
}

public class UserCreateViewModel
{
    [Required(ErrorMessage = "Adı Soyadı alanı zorunludur.")]
    [Display(Name = "Adı Soyadı")]
    public string AdıSoyadı { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    [Display(Name = "E-Posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre alanı zorunludur.")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Şifre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rol seçimi zorunludur.")]
    [Display(Name = "Rol / Görev")]
    public string Rol { get; set; } = "Kullanıcı";
}
