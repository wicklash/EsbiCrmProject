using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMProject.Data.Entities;
using CRMProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMProject.Controllers;

[Authorize(Roles = "Admin")]
public class KullaniciController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public KullaniciController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var modelList = new List<UserItemViewModel>();

        foreach (var user in users)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            modelList.Add(new UserItemViewModel
            {
                Id = user.Id,
                AdıSoyadı = user.AdıSoyadı,
                Email = user.Email ?? string.Empty,
                Görev = user.Görev,
                Aktif = user.Aktif,
                Roles = string.Join(", ", userRoles)
            });
        }

        return View(modelList);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        return View(new UserCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Ekle(UserCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılmaktadır.");
            return View(model);
        }

        var user = new AppUser
        {
            UserName = model.Email,
            Email = model.Email,
            AdıSoyadı = model.AdıSoyadı,
            Görev = model.Rol,
            Aktif = true,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, model.Şifre);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        if (!await _roleManager.RoleExistsAsync(model.Rol))
        {
            await _roleManager.CreateAsync(new IdentityRole(model.Rol));
        }

        await _userManager.AddToRoleAsync(user, model.Rol);
        TempData["Başarı"] = $"{user.AdıSoyadı} kullanıcısı başarıyla oluşturuldu.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DurumDegistir(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            TempData["Hata"] = "Kullanıcı bulunamadı.";
            return RedirectToAction(nameof(Index));
        }

        // Prevent admin from deactivating themselves
        if (user.UserName == User.Identity?.Name)
        {
            TempData["Hata"] = "Kendi hesabınızı pasife alamazsınız.";
            return RedirectToAction(nameof(Index));
        }

        user.Aktif = !user.Aktif;
        await _userManager.UpdateAsync(user);

        TempData["Başarı"] = $"Kullanıcı durumu başarıyla {(user.Aktif ? "aktif" : "pasif")} yapıldı.";
        return RedirectToAction(nameof(Index));
    }
}
