using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models.InnerModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CRM.Models;
using CRM.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
//using SendGrid.Helpers.Mail;

namespace CRM.Controllers
{
    public class AccountController : Controller
    {
        //Register form (GET)
        public IActionResult RegisterForm()
        {
            RegisterModel registerModel = new RegisterModel();
            return View(registerModel);
        }

        //Login form (GET)
        public IActionResult LoginForm()
        {
            LoginModel loginModel = new LoginModel();
            return View(loginModel);
        }

        //Profile form (GET)
        [HttpGet]
        public IActionResult Profile()
        {
            
            var userid = _userManager.GetUserId(HttpContext.User);
            if (userid == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                User user = _userManager.FindByIdAsync(userid).Result;
             
                return View(user);
            }
        }

        //Profile form (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile([Bind(include: "FullName, UserName, Email, PasswordHash")] User user, string id)
        {
            if (!ModelState.IsValid)
            {
                return View("Profile");
            }

            string currentuser = "";
            if (User.Identity.IsAuthenticated)
            {
                var userAuth = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                currentuser = userAuth.Id;


                userAuth.FullName = user.FullName;
                userAuth.UserName = user.UserName;
                userAuth.Email = user.Email;
                userAuth.NormalizedEmail = user.Email;
                userAuth.NormalizedUserName = user.Email;
                userAuth.PasswordHash = _userManager.PasswordHasher.HashPassword(user,user.PasswordHash);
                userAuth.EmailConfirmed = true;
                await _context.SaveChangesAsync();

            }           
            return View(user);
        }

        //Register action (GET)
        [HttpGet]
        public IActionResult Register()
        {
            ViewModelClasses view = new ViewModelClasses()
            {
                RegisterModel = new RegisterModel()
            };
            return View(view);
        }

        private readonly CRMdb _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;


        public AccountController(CRMdb context,
                                  UserManager<User> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<User> signInManager
                                                                  )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        //Register action (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var User = new User()
            {
                FullName = registerModel.FullName,
                UserName = registerModel.UserEmail,
                Email = registerModel.UserEmail,
                BirthDate = registerModel.BirthDate
            };

            var result = await _userManager.CreateAsync(User, registerModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Index");
            }

            //add member role
            await _userManager.AddToRoleAsync(User, StaticData.Member);
            await _signInManager.SignInAsync(User, false);
            return RedirectToAction("Index", "Home");
        }

        //Login user action (GET)
        [HttpGet]
        public IActionResult LoginUser()
        {
            ViewModelClasses viewModel = new ViewModelClasses()
            {
                LoginModel = new LoginModel()
            };
            return View(viewModel);
        }

        //Login user action (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginForm");
            }
            var user = await _userManager.FindByEmailAsync(loginModel.UserEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "email or password incorrect");                
                return View("LoginForm");
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.KeepMeLogged, true);

            if (result.IsLockedOut)

            {
                ModelState.AddModelError("", "Please wait 5 minutes.Your Account is blocked...");               
                return View("LoginForm");
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "email or password incorrect");
                return View("LoginForm");
            }
            return RedirectToAction("Index", "Home");
        }

        //LogOut user action (GET)
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}