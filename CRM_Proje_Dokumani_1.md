ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

# **CRM WEB PROJEİ** 

## Stajyer Proje Dokümanı 

_ASP.NET Core MVC  •  Entity Framework Core  •  SQL Server  •  Bootstrap 5_ 

## **<mark>1. PROJEYE GİRİŞ</mark>** 

Bu doküman, ESBİ Bilişim ve Telekomünikasyon bünyesinde staj yapan geliştiricilere yönelik hazırlanmıştır. Proje; ASP.NET Core MVC teknolojisi kullanılarak geliştirilecek bir Müşteri İlişkileri Yönetim Sistemi (CRM) web uygulamasını kapsamaktadır. 

### **1.1 Projenin Amacı** 

Bu projenin temel amacı; stajyerin .NET ekosistemini, MVC mimarisini ve veri tabanı işlemlerini öğrenip uygulayabilmesini sağlamaktır. Proje tamamlandığında aşağıdaki yetkinlikler kazanılmış olacaktır: 

- ASP.NET Core MVC mimarisi ve proje klasör yapısı 

- Entity Framework Core ile veri tabanı işlemleri (Code First) 

- Kullanıcı kimlik doğrulama ve yetkilendirme (ASP.NET Core Identity) 

- CRUD işlemleri (Oluşturma, Okuma, Güncelleme, Silme) 

- Bootstrap 5 ile duyarlı (responsive) arayüz tasarımı 

- Katmanlı mimari (N-Tier) prensipleri 

### **1.2 Teknoloji Stack** 

|**Katman**|**Teknoloji**|**Versiyon / Not**|
|---|---|---|
|Backend|ASP.NET Core MVC|.NET 8|
|ORM|Entity Framework Core|8.x – Code First|
|Veri Tabanı|Microsoft SQL Server|2019 / 2022|
|Kimlik Doğrulama|ASP.NET Core Identity|Cookie tabanlı|
|Ön Yüz|Bootstrap 5 + Razor|HTML5, CSS3, JS|
|Simgeler|Font Awesome 6|CDN üzerinden|



Sayfa 1 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

|**Katman**|**Teknoloji**|**Versiyon / Not**|
|---|---|---|
|IDE|Visual Studio 2022|Community veya üstü|



Sayfa 2 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>2. PROJE MİMARİSİ</mark>** 

Proje; bakım kolaylığı ve sorumlulukların ayrıştırılması ilkesine göre katmanlı (N-Tier) mimari ile tasarlanmıştır. 

### **2.1 Klasör Yapısı** 



<!-- Start of picture text -->
CRMProject/<br>├── CRMProject.Web/               ← Ana web uygulaması (MVC)<br>│   ├── Controllers/<br>│   │   ├── AccountController.cs   ← Giriş / çıkış işlemleri<br>│   │   ├── DashboardController.cs ← Ana panel<br>│   │   ├── CariController.cs      ← Müşteri / cari işlemleri<br>│   │   └── MalzemeController.cs   ← Malzeme işlemleri<br>│   ├── Views/<br>│   │   ├── Shared/                ← _Layout.cshtml, menü<br>│   │   ├── Account/               ← Login, Kayıt<br>│   │   ├── Dashboard/             ← Ana Panel<br>│   │   ├── Cari/                  ← Liste, Ekle, Düzenle, Sil<br>│   │   └── Malzeme/               ← Liste, Ekle, Düzenle, Sil<br>│   ├── Models/                    ← View Model sınıfları<br>│   ├── wwwroot/                   ← Statik dosyalar (CSS, JS)<br>│   └── appsettings.json<br>├── CRMProject.Data/               ← Veri erişim katmanı<br>│   ├── Context/AppDbContext.cs<br>│   ├── Entities/                  ← Veri tabanı varlık sınıfları<br>│   └── Migrations/<br>└── CRMProject.Services/           ← İş mantığı katmanı<br>    ├── Interfaces/<br>    └── Implementations/<br><!-- End of picture text -->

### **2.2 MVC Akışı** 

Bir kullanıcı isteği uygulamada aşağıdaki sırayla işlenir: 

1. Tarayıcı HTTP isteği gönderir  (örn. GET /Cari/Index) 

2. Router isteği ilgili Controller'a yönlendirir 

3. Controller, Service katmanını çağırarak işlem yapar 

4. Service, DbContext aracılığıyla SQL Server'a erişir 

5. Elde edilen veri Model olarak View'a aktarılır 

6. View HTML üretir ve tarayıcıya geri döner 

Sayfa 3 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>3. VERİTABANI TASARIMI</mark>** 

Veri tabanı Code First yaklaşımıyla tasarlanmıştır. Tüm tablolar EF Core Migration’lar aracılığıyla oluşturulur. 

### **3.1 Varlık Sınıfları** 

#### **3.1.1 Kullanıcı (AppUser)** 

```
public class AppUser : IdentityUser
{
    public string   AdıSoyadı          { get; set; }
    public string   Görev              { get; set; }  // Admin | Kullanici
    public bool     Aktif              { get; set; } = true;
    public DateTime Oluşturma Tarihi  { get; set; } = DateTime.Now;
}
```

#### **3.1.2 Cari (Müşteri / Tedarikçi)** 

```
public class Cari
{
    public int      Id                   { get; set; }
    public string   CariKodu             { get; set; }  // Zorunlu, benzersiz
    public string   Ünvan                { get; set; }  // Zorunlu
    public string   CariTipi             { get; set; }  // Müşteri | Tedarikçi |
Her İkisi
    public string   VergiDairesi         { get; set; }
    public string   VergiNo              { get; set; }
    public string   Telefon              { get; set; }
    public string   E_posta              { get; set; }
    public string   Adres                { get; set; }
    public string   İl                   { get; set; }
    public string   İlçe                 { get; set; }
    public bool     AktifMi              { get; set; } = true;
    public DateTime Oluşturma Tarihi    { get; set; } = DateTime.Now;
    public DateTime? Güncelleme Tarihi  { get; set; }
}
```

#### **3.1.3 Malzeme (Ürün / Stok)** 

```
public class Malzeme
{
    public int      Id                   { get; set; }
    public string   MalzemeKodu          { get; set; }  // Zorunlu, benzersiz
    public string   MalzemeAdı           { get; set; }  // Zorunlu
    public string   Kategori             { get; set; }
    public string   Birim                { get; set; }  // Adet, Kg, Lt, Mt …
    public decimal  Satış Fiyatı         { get; set; }
    public decimal  Alış Fiyatı          { get; set; }
    public decimal  Stok Miktarı         { get; set; }
```

Sayfa 4 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

```
    public decimal  Kritik Stok          { get; set; }
    public string   Açıklama             { get; set; }
    public bool     AktifMi              { get; set; } = true;
    public DateTime Oluşturma Tarihi    { get; set; } = DateTime.Now;
    public DateTime? Güncelleme Tarihi  { get; set; }
}
```

### **3.2 Tablo Özeti** 

|**Tablo Adı**|**Birincil Anahtar**|**Açıklama**|
|---|---|---|
|AspNetUsers|Id (GUID)|Kimlik doğrulama kullanıcıları|
|AspNetRoles|Id (GUID)|Rol tanımları (Admin, Kullanıcı)|
|Caris|Id (int)|Müşteri ve tedarikçi kayıtları|
|Malzemes|Id (int)|Ürün ve stok kayıtları|



Sayfa 5 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>4. KULLANICI GİRİŞİ VE YETKİLENDİRME</mark>** 

Uygulama ASP.NET Core Identity altyapısını kullanır. Kullanıcılar sisteme e-posta ve şifre ile giriş yapar; her sayfaya erişim rol bazlı yetkilendirme ile kontrol edilir. 

### **4.1 Roller ve Yetkiler** 

|**Rol**|**Yetkiler**|
|---|---|
|Admin|Tüm modüllere tam erişim, kullanıcı yönetimi, sistem ayarları|
|Kullanıcı|Cari ve malzeme listeleme, ekleme ve güncelleme; silme yetkisi yok|



### **4.2 Giriş Akışı** 

Kullanıcı giriş yapmadan hiçbir sayfaya erişemez. Giriş yapilmamış istekler otomatik olarak /Account/Login adresine yönlendirilir. 

```
[HttpPost]
public async Task<IActionResult> Login(GirisViewModel model)
{
    if (!ModelState.IsValid) return View(model);
    var sonuç = await _signInManager.PasswordSignInAsync(
        model.EPosta, model.Şifre,
        model.BeniHatırla, lockoutOnFailure: true);
    if (sonuç.Succeeded)
        return RedirectToAction("Index", "Dashboard");
    ModelState.AddModelError("", "E-posta veya şifre hatalı.");
    return View(model);
}
```

### **4.3 Yetkilendirme Örnekleri** 

```
// Tüm controller’a giriş zorunluluğu
[Authorize]
public class CariController : Controller { }
// Yalnızca Admin rolüne izin ver
[Authorize(Roles = "Admin")]
public IActionResult Sil(int id) { }
```

_Önemli: Silme işlemleri yalnızca Admin rolüne açıktır. Kullanıcı rolündeki kişiler silme düğmesini_ 

Sayfa 6 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

_göremez._ 

Sayfa 7 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>5. ANA PANEL (DASHBOARD)</mark>** 

Kullanıcı giriş yaptıktan sonra ana panel ekranı karşısına gelir. Bu ekranda sistemin genel durumu özet kartlar hâlinde gösterilir. 

### **5.1 Panel Kartı Bileşenleri** 

|**Kart Başlığı**|**Açıklama**|
|---|---|
|Toplam Cari Sayısı|Veri tabanındaki aktif cari kayıt sayısı|
|Toplam Malzeme Sayısı|Sistemde tanımlı aktif malzeme sayısı|
|Kritik Stok Uyardı|Stok miktarı kritik eşiğin altına düşen malzemeler|
|Son Eklenen Cariler|Son 5 eklenen cari kayıdının listesi|



### **5.2 Dashboard Controller** 

```
[Authorize]
public class DashboardController : Controller
{
    private readonly ICariService    _cariService;
    private readonly IMalzemeService _malzemeService;
    public DashboardController(ICariService c, IMalzemeService m)
    { _cariService = c; _malzemeService = m; }
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            ToplamCari    = await _cariService.ToplamSayıAsync(),
            ToplamMalzeme = await _malzemeService.ToplamSayıAsync(),
            KritikStoklar = await _malzemeService.KritikStoklarAsync(),
            SonCariler    = await _cariService.SonEklenenlerAsync(5)
        };
        return View(model);
    }
}
```

### **5.3 Sol Menü Yapısı** 

|**Menü Öğesi**|**Simge (Font Awesome)**|**Erişim Rolü**|
|---|---|---|
|Ana Panel|fa-home|Admin, Kullanıcı|
|Cari Yönetimi|fa-users|Admin, Kullanıcı|



Sayfa 8 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

|**Menü Öğesi**|**Simge (Font Awesome)**|**Erişim Rolü**|
|---|---|---|
|Malzeme Yönetimi|fa-boxes|Admin, Kullanıcı|
|Kullanıcı Yönetimi|fa-user-cog|Yalnızca Admin|
|Sistem Ayarları|fa-cogs|Yalnızca Admin|



Sayfa 9 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>6. CARİ YÖNETİMİ</mark>** 

Cari modülü; müşteri ve tedarikçi kayıtlarının listelenmesi, eklenmesi, güncellenmesi ve silinmesi işlemlerini kapsamaktadır. 

### **6.1 Cari Listeleme** 

```
[Authorize]
public async Task<IActionResult> Index(string aramaMetni = "")
{
    var cariler = await _cariService.ListeAsync(aramaMetni);
    ViewBag.AramaMetni = aramaMetni;
    return View(cariler);
}
```

### **6.2 Cari Ekleme** 

```
[Authorize]
public IActionResult Ekle() => View(new CariViewModel());
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Ekle(CariViewModel model)
{
    if (!ModelState.IsValid) return View(model);
    if (await _cariService.KodMevcutMuAsync(model.CariKodu))
    {
        ModelState.AddModelError("CariKodu", "Bu cari kodu zaten
kullanılmaktadır.");
        return View(model);
    }
    await _cariService.EkleAsync(model);
    TempData["Başarı"] = "Cari başarıyla eklendi.";
    return RedirectToAction(nameof(Index));
}
```

### **6.3 Cari Güncelleme** 

```
[Authorize]
public async Task<IActionResult> Düzedle(int id)
{
    var cari = await _cariService.GetirAsync(id);
    if (cari == null) return NotFound();
    return View(cari);
}
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Düzedle(int id, CariViewModel model)
{
    if (!ModelState.IsValid) return View(model);
```

Sayfa 10 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

```
    await _cariService.GüncelleAsync(id, model);
    TempData["Başarı"] = "Cari başarıyla güncellendi.";
    return RedirectToAction(nameof(Index));
}
```

### **6.4 Cari Silme (Soft Delete – Yalnızca Admin)** 

```
[Authorize(Roles = "Admin")]
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Sil(int id)
{
    await _cariService.PasifYapAsync(id);   // AktifMi = false
    TempData["Başarı"] = "Cari başarıyla silindi.";
    return RedirectToAction(nameof(Index));
}
```

### **6.5 Cari ViewModel** 

```
public class CariViewModel
{
    [Required(ErrorMessage = "Cari kodu zorunludur.")]
    [StringLength(20)]
    public string CariKodu      { get; set; }
    [Required(ErrorMessage = "Ünvan zorunludur.")]
    [StringLength(200)]
    public string Ünvan         { get; set; }
    [Required(ErrorMessage = "Cari tipi zorunludur.")]
    public string CariTipi      { get; set; }
    public string VergiDairesi  { get; set; }
    public string VergiNo       { get; set; }
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
    public string Telefon       { get; set; }
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string EPosta        { get; set; }
    public string Adres         { get; set; }
    public string İl            { get; set; }
    public string İlçe          { get; set; }
}
```

Sayfa 11 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>7. MALZEME YÖNETİMİ</mark>** 

Malzeme modülü; ürün ve stok kayıtlarının listelenmesi, eklenmesi, güncellenmesi ve silinmesi işlemlerini kapsamaktadır. 

### **7.1 Malzeme Listeleme** 

```
[Authorize]
public async Task<IActionResult> Index(string aramaMetni = "", string kategori =
"")
{
    var malzemeler = await _malzemeService.ListeAsync(aramaMetni, kategori);
    ViewBag.Kategoriler = await _malzemeService.KategoriListesiAsync();
    return View(malzemeler);
}
```

### **7.2 Malzeme Ekleme** 

```
[Authorize]
public IActionResult Ekle()
{
    ViewBag.Kategoriler = _malzemeService.KategoriListesi();
    return View(new MalzemeViewModel());
}
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Ekle(MalzemeViewModel model)
{
    if (!ModelState.IsValid) { ViewBag.Kategoriler =
_malzemeService.KategoriListesi(); return View(model); }
    if (await _malzemeService.KodMevcutMuAsync(model.MalzemeKodu))
    {
        ModelState.AddModelError("MalzemeKodu", "Bu malzeme kodu zaten
mevcuttur.");
        return View(model);
    }
    await _malzemeService.EkleAsync(model);
    TempData["Başarı"] = "Malzeme başarıyla eklendi.";
    return RedirectToAction(nameof(Index));
}
```

### **7.3 Malzeme Güncelleme** 

```
[Authorize]
public async Task<IActionResult> Düzedle(int id)
{
    var malzeme = await _malzemeService.GetirAsync(id);
    if (malzeme == null) return NotFound();
    ViewBag.Kategoriler = _malzemeService.KategoriListesi();
    return View(malzeme);
```

Sayfa 12 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

```
}
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Düzedle(int id, MalzemeViewModel model)
{
    if (!ModelState.IsValid) return View(model);
    await _malzemeService.GüncelleAsync(id, model);
    TempData["Başarı"] = "Malzeme başarıyla güncellendi.";
    return RedirectToAction(nameof(Index));
}
```

### **7.4 Malzeme Silme (Soft Delete – Yalnızca Admin)** 

```
[Authorize(Roles = "Admin")]
[HttpPost, ValidateAntiForgeryToken]
public async Task<IActionResult> Sil(int id)
{
    await _malzemeService.PasifYapAsync(id);
    TempData["Başarı"] = "Malzeme başarıyla silindi.";
    return RedirectToAction(nameof(Index));
}
```

### **7.5 Malzeme ViewModel** 

```
public class MalzemeViewModel
{
    [Required(ErrorMessage = "Malzeme kodu zorunludur.")]
    [StringLength(30)]
    public string  MalzemeKodu   { get; set; }
    [Required(ErrorMessage = "Malzeme adı zorunludur.")]
    [StringLength(200)]
    public string  MalzemeAdı    { get; set; }
    public string  Kategori      { get; set; }
    [Required(ErrorMessage = "Birim zorunludur.")]
    public string  Birim         { get; set; }   // Adet, Kg, Lt, Mt …
    [Range(0, double.MaxValue, ErrorMessage = "Satış fiyatı negatif olamaz.")]
    public decimal Satış Fiyatı  { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Alış fiyatı negatif olamaz.")]
    public decimal Alış Fiyatı   { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Stok miktarı negatif olamaz.")]
    public decimal Stok Miktarı  { get; set; }
    public decimal Kritik Stok   { get; set; }
    public string  Açıklama      { get; set; }
}
```

Sayfa 13 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

Sayfa 14 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>8. ARAYÜZ TASARIMI</mark>** 

Tüm sayfalar Bootstrap 5 çerçevesi ve Razor View motoru kullanılarak geliştirilir. Arayüz hem masaüstü hem mobil cihazlarda düzgün görünecek şekilde duyarlı (responsive) tasarlanır. 

### **8.1 Genel Sayfa Yapısı (_Layout.cshtml)** 

|**Bölüm**|**Açıklama**|
|---|---|
|Üst Çubuk (Navbar)|Logo, kullanıcı adı ve çıkış düğmesi|
|Sol Menü (Sidebar)|Gezinti bağlantıları, rol bazlı görünürlik|
|Ana İçerik Alanı|@RenderBody() ile sayfa içeriği burada görünür|
|Alt Bilgi (Footer)|Telif hakkı ve uygulama versiyon bilgisi|



### **8.2 Bildirim (Alert) Yapısı** 

```
@if (TempData["Başarı"] != null)
{
    <div class="alert alert-success alert-dismissible fade show">
        <i class="fas fa-check-circle me-2"></i>@TempData["Başarı"]
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>
}
@if (TempData["Hata"] != null)
{
    <div class="alert alert-danger alert-dismissible fade show">
        <i class="fas fa-exclamation-circle me-2"></i>@TempData["Hata"]
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>
}
```

Sayfa 15 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>9. KURULUM VE ÇALIŞTIRMA</mark>** 

### **9.1 Ön Gereksinimler** 

|**Yazılım**|**Minimum Versiyon**|
|---|---|
|.NET SDK|8.0 veya üstü|
|Visual Studio|2022 Community veya üstü|
|SQL Server|2019 Express veya üstü|
|SQL Server Management Studio|19.x (önerilir)|
|Git|Son sürüm|



### **9.2 Adım Adım Kurulum** 

7. Depoyu klonlayın:  git clone https://github.com/esbi/crm-proje.git 

8. Visual Studio 2022 ile CRMProject.sln dosyasını açın 

9. appsettings.json içindeki bağlantı dizesini kendi SQL Server bilgilerinize göre düzenleyin 

10. Paket Yöneticisi Konsolu’nda (PMC) aşağıdaki komutu çalıştırın: 

```
# Paket Yöneticisi Konsolu (PMC)
Update-Database
# veya Terminal
dotnet ef database update
```

11. Uygulamayı F5 tuşuyla veya ‘dotnet run’ komutuyla başlatın 

12. Tarayıcıdan https://localhost:7xxx adresine gidin 

13. İlk giriş için seed verisiyle oluşturulan Admin hesabını kullanın: 

|**Alan**|**Değer**|
|---|---|
|E-posta|admin@esbi.com.tr|
|Şifre|Admin@123!|



_Güvenlik Notu: Uygulamayı üretime taşımadan önce varsayılan admin şifresini mutlaka değiştirin!_ 

### **9.3 Bağlantı Dizesi (appsettings.json)** 

Sayfa 16 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=CRMProject;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Sayfa 17 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>10. STAJYER GÖREV LİSTESİ</mark>** 

Aşağıdaki görevler, projenin tamamlanması için gereken aşama aşama yapılacaklar listesini içermektedir. 

### **Aşama 1 – Proje Kurulumu (1. Hafta)** 

|**#**|**Görev**|**Durum**|
|---|---|---|
|1|ASP.NET Core MVC projesi oluşturma|Tamamlandı<br>☑|
|2|EF Core ve Identity NuGet paketlerini yükleme|Tamamlandı<br>☑|
|3|AppDbContext ve bağlantı dizesini yapılandırma|Tamamlandı<br>☑|
|4|Varlık sınıflarını (Cari, Malzeme, AppUser) oluşturma|Tamamlandı<br>☑|
|5|Migration oluşturma ve veri tabanına uygulama|Tamamlandı<br>☑|
|6|Admin ve Kullanıcı rollerini seed verisiyle oluşturma|Tamamlandı<br>☑|



### **Aşama 2 – Kimlik Doğrulama (2. Hafta)** 

|**#**|**Görev**|**Durum**|
|---|---|---|
|7|Login sayfası ve AccountController|Tamamlandı<br>☑|
|8|_Layout.cshtml ile sol menü ve navbar oluşturma|Tamamlandı<br>☑|
|9|Dashboard sayfası ve özet kartları|Tamamlandı<br>☑|
|10|Rol bazlı menü görünürlüğü|Tamamlandı<br>☑|



### **Aşama 3 – Cari Modülü (3. Hafta)** 

|**#**|**Görev**|**Durum**|
|---|---|---|
|11|ICariService arayüzü ve CariService gerçeklemesi|Tamamlandı<br>☑|
|12|CariController – Index (listeleme ve arama)|Tamamlandı<br>☑|
|13|CariController – Ekle (GET ve POST)|Tamamlandı<br>☑|
|14|CariController – Düzedle (GET ve POST)|Tamamlandı<br>☑|
|15|CariController – Sil (Admin – soft delete)|Tamamlandı<br>☑|
|16|Cari Razor View’larının tasarlanması|Tamamlandı<br>☑|



Sayfa 18 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

### **Aşama 4 – Malzeme Modülü (4. Hafta)** 

|**#**|**Görev**|**Durum**|
|---|---|---|
|17|IMalzemeService ve MalzemeService yazımı|Tamamlandı<br>☑|
|18|MalzemeController – Index (filtreleme ve arama)|Tamamlandı<br>☑|
|19|MalzemeController – Ekle (GET ve POST)|Tamamlandı<br>☑|
|20|MalzemeController – Düzedle (GET ve POST)|Tamamlandı<br>☑|
|21|MalzemeController – Sil (Admin – soft delete)|Tamamlandı<br>☑|
|22|Kritik stok renk vurgulamasını ekleme|Tamamlandı<br>☑|
|23|Malzeme Razor View’larının tasarlanması|Tamamlandı<br>☑|



Sayfa 19 / 20 

ASP.NET Core MVC | 2025 

**CRM Web Projesi – Stajyer Proje Dokümanı** 

## **<mark>11. KODLAMA STANDARTLARI VE ÖNERİLER</mark>** 

### **11.1 Genel Kurallar** 

- Tüm değişken, metot ve sınıf adları Türkçe ya da İngilizce olarak tutarlı kullanılmalıdır 

- • Controller’lar yalnızca HTTP isteklerini karşılamalı; iş mantığı Service katmanında olmalıdır 

- Her form gönderiminde [ValidateAntiForgeryToken] attribute’u kullanılmalıdır 

- Hata mesajları TempData ile kullanıcıya iletilmelidir 

- Entity sınıfları doğrudan View’a gönderilmemeli; ViewModel kullanılmalıdır 

- async/await yapısı tüm veri tabanı işlemlerinde kullanılmalıdır 

### **11.2 Güvenlik Kontrol Listesi** 

- Her controller metodu uygun [Authorize] attribute’uyla korunmalıdır 

- SQL Injection’a karşı EF Core parametrik sorgular kullanılmalıdır 

- XSS saldırılarına karşı Razor otomatik kodlama yapmasına izin verilmelidir 

- Üretim ortamında hata ayrıntıları kullanıcıya gösterilmemelidir 

- Parolalar Identity aracılığıyla hashlenerek saklanmalıdır 

### **11.3 Proje Teslim Kriterleri** 

|**Kriter**|**Ağırlık**|**Durum**|
|---|---|---|
|Kullanıcı girişi ve rol bazlı yetkilendirme|%20<br>☑|Tamamlandı|
|Cari CRUD işlemleri|%25<br>☑|Tamamlandı|
|Malzeme CRUD işlemleri|%25<br>☑|Tamamlandı|
|Dashboard ve özet kartları|%10<br>☑|Tamamlandı|
|Duyarlı arayüz tasarımı (Bootstrap 5)|%10<br>☑|Tamamlandı|
|Kod kalitesi ve yorumlar|%10<br>☑|Tamamlandı|



_Bu doküman ESBİ Bilişim ve Telekomünikasyon San. Tic. Ltd. Şti. tarafından hazırlanmış olup stajyer geliştiricilerin kullanımına yöneliktir. Sorularınız için mentörünüze başvurunuz._ 

Sayfa 20 / 20 


