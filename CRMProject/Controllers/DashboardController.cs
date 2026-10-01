using System.Threading.Tasks;
using CRMProject.Models;
using CRMProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ICariService _cariService;
    private readonly IMalzemeService _malzemeService;

    public DashboardController(ICariService cariService, IMalzemeService malzemeService)
    {
        _cariService = cariService;
        _malzemeService = malzemeService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            ToplamCari = await _cariService.ToplamSayıAsync(),
            ToplamMalzeme = await _malzemeService.ToplamSayıAsync(),
            KritikStoklar = await _malzemeService.KritikStoklarAsync(),
            SonCariler = await _cariService.SonEklenenlerAsync(5)
        };

        return View(model);
    }
}
