using System.Threading.Tasks;
using CRMProject.Data.Entities;
using CRMProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Dashboard");
        }

        ViewData["ReturnUrl"] = returnUrl;
        return View(new GirisViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(GirisViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.EPosta);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
            return View(model);
        }

        if (!user.Aktif)
        {
            ModelState.AddModelError(string.Empty, "Hesabınız pasife alınmıştır. Lütfen sistem yöneticisiyle iletişime geçiniz.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName ?? model.EPosta,
            model.Şifre,
            model.BeniHatırla,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Hesabınız geçici olarak kilitlendi. Lütfen daha sonra tekrar deneyiniz.");
            return View(model);
        }

        ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
