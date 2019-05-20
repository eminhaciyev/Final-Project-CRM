using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models.InnerModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticData.Admin)]
    public class AdminController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        public AdminController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Admin Panel Logout (POST)
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Logout", "Account", new { area = "Identity" });      
        }
        
    }
}