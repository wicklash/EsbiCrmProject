using System.Threading.Tasks;
using CRMProject.Services.Interfaces;
using CRMProject.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers;

[Authorize]
public class CariController : Controller
{
    private readonly ICariService _cariService;

    public CariController(ICariService cariService)
    {
        _cariService = cariService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string aramaMetni = "")
    {
        var cariler = await _cariService.ListeAsync(aramaMetni);
        ViewBag.AramaMetni = aramaMetni;
        return View(cariler);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        return View(new CariViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(CariViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _cariService.KodMevcutMuAsync(model.CariKodu))
        {
            ModelState.AddModelError("CariKodu", "Bu cari kodu zaten kullanılmaktadır.");
            return View(model);
        }

        await _cariService.EkleAsync(model);
        TempData["Başarı"] = "Cari başarıyla eklendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [ActionName("Duzenle")]
    public async Task<IActionResult> Duzenle(int id)
    {
        var cari = await _cariService.GetirViewModelAsync(id);
        if (cari == null)
        {
            TempData["Hata"] = "Belirtilen cari kaydı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        return View(cari);
    }

    [HttpPost]
    [ActionName("Duzenle")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Duzenle(int id, CariViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _cariService.KodMevcutMuAsync(model.CariKodu, id))
        {
            ModelState.AddModelError("CariKodu", "Bu cari kodu başka bir kayıtta kullanılmaktadır.");
            return View(model);
        }

        await _cariService.GüncelleAsync(id, model);
        TempData["Başarı"] = "Cari başarıyla güncellendi.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Sil(int id)
    {
        var cari = await _cariService.GetirAsync(id);
        if (cari == null)
        {
            TempData["Hata"] = "Silinmek istenen cari kaydı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        await _cariService.PasifYapAsync(id);
        TempData["Başarı"] = "Cari başarıyla silindi.";
        return RedirectToAction(nameof(Index));
    }
}
