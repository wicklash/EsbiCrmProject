# ESBİ CRM Web Projesi

ASP.NET Core MVC mimarisi ile geliştirilmiş, katmanlı mimariye (N-Tier) sahip Müşteri İlişkileri (Cari) ve Malzeme/Stok Yönetim Sistemi web uygulaması.

---

## 🚀 Kullanılan Teknolojiler

* **Backend:** ASP.NET Core MVC (.NET 8)
* **ORM:** Entity Framework Core 8 (Code First)
* **Veritabanı:** Microsoft SQL Server
* **Kimlik Doğrulama:** ASP.NET Core Identity (Cookie bazlı)
* **Frontend:** Razor View Engine, Bootstrap 5, Font Awesome 6

---

## 📋 Temel Özellikler

* **Kimlik Doğrulama ve Yetkilendirme:** Rol bazlı erişim kontrolü (Admin ve Kullanıcı).
* **Ana Panel (Dashboard):** Toplam müşteri, malzeme ve aktif cari özet sayaçları.
* **Cari Yönetimi:** Müşteri ve tedarikçi ekleme, güncelleme, silme (soft delete), detaylı filtreleme ve arama.
* **Malzeme Yönetimi:** Ürün/stok takibi, birim fiyat, KDV oranları ve kritik stok seviyesi görsel uyarıları.
* **Kullanıcı Yönetimi:** Yöneticiler için sisteme personel ekleme ve rol atama ekranı.

---

## 🛠️ Kurulum ve Çalıştırma

### 1. Ön Gereksinimler
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* [Visual Studio 2022](https://visualstudio.microsoft.com/) (.NET masaüstü ve web geliştirme iş yükü ile)
* Microsoft SQL Server veya SQL Express

### 2. Projeyi Çalıştırma Adımları

1. Çözümü Visual Studio ile açın:
   ```text
   CRMProject.sln
   ```
2. `CRMProject/appsettings.json` içerisindeki veritabanı bağlantı dizesinin (`DefaultConnection`) kendi SQL Server örneğinize uygun olduğunu kontrol edin:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.\\SQLEXPRESS;Database=CRMProject;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
3. Visual Studio'da **Package Manager Console** ekranını açın (`Tools > NuGet Package Manager > Package Manager Console`), **Default project** olarak `CRMProject.Data` seçin ve veritabanını oluşturun:
   ```powershell
   Update-Database
   ```
4. `CRMProject.Web` projesini başlangıç projesi (Startup Project) olarak belirleyin ve **F5** veya **Ctrl + F5** ile projeyi başlatın.

---

## 🔑 Varsayılan Giriş Bilgileri

Uygulama ilk ayağa kalktığında örnek veriler ve test hesapları otomatik olarak veritabanına eklenir:

| Rol | E-Posta | Şifre |
|---|---|---|
| **Yönetici (Admin)** | `admin@esbi.com.tr` | `Admin@123!` |
| **Kullanıcı** | `kullanici@esbi.com.tr` | `User@123!` |
