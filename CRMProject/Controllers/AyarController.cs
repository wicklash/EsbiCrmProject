using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMProject.Controllers;

[Authorize(Roles = "Admin")]
public class AyarController : Controller
{
    public IActionResult Index()
    {
        ViewBag.FrameworkVersion = RuntimeInformation.FrameworkDescription;
        ViewBag.OSDescription = RuntimeInformation.OSDescription;
        ViewBag.ServerTime = DateTime.Now;
        return View();
    }
}
