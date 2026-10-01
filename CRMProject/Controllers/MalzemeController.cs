using System.Threading.Tasks;
using CRMProject.Services.Interfaces;
using CRMProject.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers;

[Authorize]
public class MalzemeController : Controller
{
    private readonly IMalzemeService _malzemeService;

    public MalzemeController(IMalzemeService malzemeService)
    {
        _malzemeService = malzemeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string aramaMetni = "", string kategori = "")
    {
        var malzemeler = await _malzemeService.ListeAsync(aramaMetni, kategori);
        ViewBag.Kategoriler = await _malzemeService.KategoriListesiAsync();
        ViewBag.AramaMetni = aramaMetni;
        ViewBag.SeciliKategori = kategori;
        return View(malzemeler);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        ViewBag.Kategoriler = _malzemeService.KategoriListesi();
        return View(new MalzemeViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(MalzemeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Kategoriler = _malzemeService.KategoriListesi();
            return View(model);
        }

        if (await _malzemeService.KodMevcutMuAsync(model.MalzemeKodu))
        {
            ModelState.AddModelError("MalzemeKodu", "Bu malzeme kodu zaten mevcuttur.");
            ViewBag.Kategoriler = _malzemeService.KategoriListesi();
            return View(model);
        }

        await _malzemeService.EkleAsync(model);
        TempData["Başarı"] = "Malzeme başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [ActionName("Duzenle")]
    public async Task<IActionResult> Duzenle(int id)
    {
        var malzeme = await _malzemeService.GetirViewModelAsync(id);
        if (malzeme == null)
        {
            TempData["Hata"] = "Belirtilen malzeme kaydı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Kategoriler = _malzemeService.KategoriListesi();
        return View(malzeme);
    }

    [HttpPost]
    [ActionName("Duzenle")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Duzenle(int id, MalzemeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Kategoriler = _malzemeService.KategoriListesi();
            return View(model);
        }

        if (await _malzemeService.KodMevcutMuAsync(model.MalzemeKodu, id))
        {
            ModelState.AddModelError("MalzemeKodu", "Bu malzeme kodu başka bir kayıtta mevcuttur.");
            ViewBag.Kategoriler = _malzemeService.KategoriListesi();
            return View(model);
        }

        await _malzemeService.GüncelleAsync(id, model);
        TempData["Başarı"] = "Malzeme başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Sil(int id)
    {
        var malzeme = await _malzemeService.GetirAsync(id);
        if (malzeme == null)
        {
            TempData["Hata"] = "Silinmek istenen malzeme kaydı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        await _malzemeService.PasifYapAsync(id);
        TempData["Başarı"] = "Malzeme başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}
