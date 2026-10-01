using System;
using Microsoft.AspNetCore.Identity;

namespace CRMProject.Data.Entities;

public class AppUser : IdentityUser
{
    public string AdıSoyadı { get; set; } = string.Empty;
    public string Görev { get; set; } = string.Empty;  // Admin | Kullanici
    public bool Aktif { get; set; } = true;
    public DateTime OluşturmaTarihi { get; set; } = DateTime.Now;
}