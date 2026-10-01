using System;
using System.Linq;
using System.Threading.Tasks;
using CRMProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CRMProject.Data.Context;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // 1. Rolleri Oluştur
        string[] roles = ["Admin", "Kullanıcı", "Kullanici"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Admin Kullanıcısı
        var adminEmail = "admin@esbi.com.tr";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                AdıSoyadı = "Sistem Yöneticisi",
                Görev = "Admin",
                Aktif = true,
                OluşturmaTarihi = DateTime.Now
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // 3. Standart Kullanıcı
        var demoEmail = "kullanici@esbi.com.tr";
        var demoUser = await userManager.FindByEmailAsync(demoEmail);
        if (demoUser == null)
        {
            demoUser = new AppUser
            {
                UserName = demoEmail,
                Email = demoEmail,
                EmailConfirmed = true,
                AdıSoyadı = "Ahmet Yılmaz",
                Görev = "Kullanıcı",
                Aktif = true,
                OluşturmaTarihi = DateTime.Now
            };

            var result = await userManager.CreateAsync(demoUser, "User@123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(demoUser, "Kullanıcı");
                await userManager.AddToRoleAsync(demoUser, "Kullanici");
            }
        }

        // 4. Örnek Cari Kayıtları
        if (!context.Caris.Any())
        {
            context.Caris.AddRange(
                new Cari
                {
                    CariKodu = "C001",
                    Ünvan = "ESBİ Bilişim ve Telekomünikasyon Ltd. Şti.",
                    CariTipi = "Müşteri",
                    VergiDairesi = "Kadıköy",
                    VergiNo = "1234567890",
                    Telefon = "0216 555 0101",
                    E_posta = "info@esbi.com.tr",
                    Adres = "Kozyatağı Mah. Değirmen Sok. No:10",
                    İl = "İstanbul",
                    İlçe = "Kadıköy",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-20)
                },
                new Cari
                {
                    CariKodu = "C002",
                    Ünvan = "Atlas Teknoloji Sanayi A.Ş.",
                    CariTipi = "Tedarikçi",
                    VergiDairesi = "Çankaya",
                    VergiNo = "9876543210",
                    Telefon = "0312 444 0202",
                    E_posta = "iletisim@atlastekno.com",
                    Adres = "Kızılay Cad. No:45",
                    İl = "Ankara",
                    İlçe = "Çankaya",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-15)
                },
                new Cari
                {
                    CariKodu = "C003",
                    Ünvan = "Ege Lojistik & Dağıtım Hizmetleri",
                    CariTipi = "Her İkisi",
                    VergiDairesi = "Konak",
                    VergiNo = "4561237890",
                    Telefon = "0232 333 0303",
                    E_posta = "destek@egelojistik.com",
                    Adres = "Alsancak Liman Cad. No:12",
                    İl = "İzmir",
                    İlçe = "Konak",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-10)
                },
                new Cari
                {
                    CariKodu = "C004",
                    Ünvan = "Akdeniz Endüstriyel Çözümler",
                    CariTipi = "Müşteri",
                    VergiDairesi = "Muratpaşa",
                    VergiNo = "7894561230",
                    Telefon = "0242 222 0404",
                    E_posta = "info@akdenizendustri.com",
                    Adres = "Sanayi Mah. 120. Sok.",
                    İl = "Antalya",
                    İlçe = "Muratpaşa",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-5)
                },
                new Cari
                {
                    CariKodu = "C005",
                    Ünvan = "Bursa Otomotiv Parça İthalat",
                    CariTipi = "Tedarikçi",
                    VergiDairesi = "Nilüfer",
                    VergiNo = "3216549870",
                    Telefon = "0224 111 0505",
                    E_posta = "siparis@bursaoto.com",
                    Adres = "Organize Sanayi Bölgesi 4. Cad.",
                    İl = "Bursa",
                    İlçe = "Nilüfer",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-2)
                }
            );
            await context.SaveChangesAsync();
        }

        // 5. Örnek Malzeme Kayıtları (Bazıları kritik stok altında)
        if (!context.Malzemes.Any())
        {
            context.Malzemes.AddRange(
                new Malzeme
                {
                    MalzemeKodu = "MLZ001",
                    MalzemeAdı = "Cat6 Ağ Kablosu (305m Makaralı)",
                    Kategori = "Kablo",
                    Birim = "Adet",
                    SatışFiyatı = 3500.00m,
                    AlışFiyatı = 2600.00m,
                    StokMiktarı = 3.00m,
                    KritikStok = 5.00m,
                    Açıklama = "Yüksek hızlı Gigabit UTP Ethernet kablosu",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-25)
                },
                new Malzeme
                {
                    MalzemeKodu = "MLZ002",
                    MalzemeAdı = "24 Port Gigabit Yönetilebilir Switch",
                    Kategori = "Ağ Cihazları",
                    Birim = "Adet",
                    SatışFiyatı = 8200.00m,
                    AlışFiyatı = 6400.00m,
                    StokMiktarı = 2.00m,
                    KritikStok = 4.00m,
                    Açıklama = "VLAN ve QoS destekli kurumsal switch",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-20)
                },
                new Malzeme
                {
                    MalzemeKodu = "MLZ003",
                    MalzemeAdı = "Endüstriyel Wi-Fi 6 Access Point",
                    Kategori = "Ağ Cihazları",
                    Birim = "Adet",
                    SatışFiyatı = 4900.00m,
                    AlışFiyatı = 3800.00m,
                    StokMiktarı = 14.00m,
                    KritikStok = 5.00m,
                    Açıklama = "Çift bantlı kurumsal tavan tipi erişim noktası",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-15)
                },
                new Malzeme
                {
                    MalzemeKodu = "MLZ004",
                    MalzemeAdı = "1U 24 Port Patch Panel Cat6",
                    Kategori = "Kabin ve Aksesuar",
                    Birim = "Adet",
                    SatışFiyatı = 950.00m,
                    AlışFiyatı = 650.00m,
                    StokMiktarı = 25.00m,
                    KritikStok = 8.00m,
                    Açıklama = "19 inç rack kabinet uyumlu patch panel",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-10)
                },
                new Malzeme
                {
                    MalzemeKodu = "MLZ005",
                    MalzemeAdı = "RJ45 Konnektör (100'lü Paket)",
                    Kategori = "Sarf Malzeme",
                    Birim = "Paket",
                    SatışFiyatı = 280.00m,
                    AlışFiyatı = 170.00m,
                    StokMiktarı = 4.00m,
                    KritikStok = 10.00m,
                    Açıklama = "Cat6 altın kaplama pinli konnektör paketi",
                    AktifMi = true,
                    OluşturmaTarihi = DateTime.Now.AddDays(-5)
                }
            );
            await context.SaveChangesAsync();
        }
    }
}
