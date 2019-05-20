using CRM.Data;
using CRM.Extension;
using CRM.Models;
using CRM.Models.InnerModel;
using CRM.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static CRM.Extension.FileExtensions;

namespace CRM.Controllers
{
    public class HomeController : Controller
    {
    
        private readonly CRMdb _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHostingEnvironment _env;


        public HomeController(CRMdb context,
                                  UserManager<User> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<User> signInManager, IHostingEnvironment env
                                                                  )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _env = env;
        }
               
        //Index GET
        public async Task<IActionResult> Index(string lang)
        {
            ViewBag.lang = lang;

            ViewBag.aboutPage = "About";

            ViewModelClasses viewModel = new ViewModelClasses()
            {
                Sliders = await _context.Sliders.ToListAsync(),
                About = _context.Abouts.FirstOrDefault(),
                Testimonials = await _context.Testimonials.ToListAsync(),
                WhyChooseUs = await _context.WhyChooseUs.ToListAsync()

            };
            return View(viewModel);
        }

        //Buy Now 
        public IActionResult BuyNow(string fullname, string cvnumber, string digitcard)
        {
            if (!string.IsNullOrWhiteSpace(fullname) && !string.IsNullOrWhiteSpace(cvnumber) && !string.IsNullOrWhiteSpace(digitcard))
            {
                return Json(new { status = "200" });
            }
            else
            {
                return Json(new { status = "404" });
            }
        }

        //Service GET
        [HttpGet]
        public async Task<IActionResult> Service()
        {
            ViewModelClasses viewModel = new ViewModelClasses()
            {
                Feedbacks = new Feedbacks(),            
                _Complaint = _context.Complaints.FirstOrDefault(),
                _CV = _context.CVs.FirstOrDefault()
            };

            if (User.Identity.IsAuthenticated)
            {
                string currentuser = "";
                var userAuth = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                currentuser = userAuth.Id;
                viewModel.Feedbacks.Like = (from feed in _context.Feedbacks
                                            where feed.UserId == currentuser
                                            select feed.Like).FirstOrDefault();

                viewModel.Feedbacks.DisLike = (from feed in _context.Feedbacks
                                               where feed.UserId == currentuser
                                               select feed.DisLike).FirstOrDefault();
            }

            viewModel.Feedbacks.LikeCount = _context.Feedbacks.Select(x => x.LikeCount).Sum();

            ViewBag.Status = Status.Stat;

            Status.Stat = "empty";           

            return View(viewModel);
        }

        //Service POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Service(IFormCollection keys)
        {
            Complaint complaint = new Complaint();

            if (User.Identity.IsAuthenticated)
            {
                var UserEmail = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                complaint.UserId = UserEmail.Id;
            }

                complaint.Fullname = keys["namecomplaint"];
                complaint.Email = keys["emailcomplaint"];
            if (User.Identity.IsAuthenticated)
            {
                var UserEmail = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);              

                if (UserEmail.ToString() != complaint.Email)
                {
                    return RedirectToAction("WarningPage", "Home");
                }
            }            
                complaint.Subject = keys["subjectcomplaint"];
                complaint.Message = keys["messagecomplaint"];

           
            _context.Complaints.Add(complaint);
            await _context.SaveChangesAsync();
            Status.Stat = "success";

            return RedirectToAction("Service", "Home");
        }

        //Warning page 
        public IActionResult WarningPage()
        {
            return View();
        }


        //CV        
        [HttpPost]       
        [ActionName("CV")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CV(ViewModelClasses model)
        {
            var _cv = model._CV;

            if (User.Identity.IsAuthenticated)
            {
                var UserEmail = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                _cv.UserId = UserEmail.Id;

                if (UserEmail.ToString() != _cv.Email)
                {
                    return RedirectToAction("WarningPage", "Home");
                }
            }                                 
            if (_cv.CV_File == null)
            {
                ModelState.AddModelError("", "file sould be selected");
                return View("Service", model);
            }           
            _cv.CV_Name = await _cv.CV_File.SaveAsync(_env.WebRootPath, "img", "cv");
            
            _context.CVs.Add(_cv);
            await _context.SaveChangesAsync();
            Status.Stat = "success";
            return RedirectToAction("Service", "Home");
        }

        //Cash your Card
        public IActionResult CashCard()
        {          
            return RedirectToAction("WarningCashCardPage", "Home");
        }

        
        //Warning Cash Card page and Show My Informations
        public IActionResult WarningCashCardPage()
        {
            return View();
        }

        //Blog
        public async Task<IActionResult> Blog(string lang)
        {
            ViewBag.lang = lang;

            var length = _context.Blogs.Count();
            int pageLength = length / 3;
            ViewBag.blogLength = pageLength;
            var vm = new ViewModelClasses()
            {
                Blogs = await _context.Blogs.Take(3).ToListAsync()
            };

            return View(vm);
        }

        //BLog Details
        public IActionResult BlogDetails(int? id)
        {
            if (id == null)
            {
                return NotFound("Belə bir blog tapılmadı...");
            }

            var blog = _context.Blogs.Find(id);

            if (blog == null)
            {
                return NotFound("Belə bir blog tapılmadı...");
            }
            return View(blog);
        }

        //Contact GET
        [HttpGet]
        public IActionResult Contact()
        {
            ViewBag.Status = Status.Stat;

            Status.Stat = "empty";

            ViewModelClasses viewModel = new ViewModelClasses()
            {
                Contacts = _context.Contacts.FirstOrDefault()
            };
            return View(viewModel);
        }

        //Contact POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(IFormCollection keys)
        {
            Contact contact = new Models.Contact();

            if (User.Identity.IsAuthenticated)
            {
                var UserEmail = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
                contact.UserId = UserEmail.Id;
            }

            contact.Fullname = keys["fullname"];
            contact.EmailOrNumber = keys["email"];
            if (User.Identity.IsAuthenticated)
            {
                var UserEmail = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);

                if (UserEmail.ToString() != contact.EmailOrNumber)
                {
                    return RedirectToAction("WarningPage", "Home");
                }
            }
            contact.Subject = keys["subject"];
            contact.Message = keys["message"];

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            Status.Stat = "success";

            return RedirectToAction("Contact", "Home");
        }

        //Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Load page BLogs
        public IActionResult LoadPage(int skip, string lang)
        {
            ViewBag.lang = lang;

            IEnumerable<Blog> blogs = _context.Blogs.Skip(skip).Take(3);

            return PartialView(blogs);
        }

        //Set Language
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true }
                );

            Uri uri = new Uri($"{Request.Scheme}://{Request.Host}" + returnUrl.Substring(1));
            return Redirect(uri.AbsoluteUri);
        }


        //Like System
        [HttpPost]
        public async Task<IActionResult> Like(Feedbacks feedback, int likecount)
        {           
            string currentuser = "";
            var userAuth = await _userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            currentuser = userAuth.Id;
            bool isliked = (from feed in _context.Feedbacks
                            where feed.UserId == currentuser
                            select feed.Like).FirstOrDefault();
           
            var user = (from feed in _context.Feedbacks
                        where feed.UserId == currentuser
                        select feed).FirstOrDefault();
            if (user == null)
            {
                Feedbacks feed = new Feedbacks();
                feed.UserId = currentuser;
                feed.Like = true;
                feed.DisLike = false;
                feed.LikeCount += 1;
                if (_context.Feedbacks.ToList().Count()==0)
                {
                    likecount += 1;
                }
                else
                {
                    likecount = _context.Feedbacks.Select(x => x.LikeCount).Sum() + 1;
                }
                _context.Feedbacks.Add(feed);
                await _context.SaveChangesAsync();
            }
            else
            {
                feedback.UserId = currentuser;
                if (User.Identity.IsAuthenticated && isliked)
                {
                    user.Like = false;
                    user.DisLike = false;
                    user.LikeCount -= 1;
                    likecount = _context.Feedbacks.Select(x => x.LikeCount).Sum() -1;
                }
                else
                {
                    user.Like = true;
                    user.DisLike = false;
                    user.LikeCount += 1;
                    likecount = _context.Feedbacks.Select(x => x.LikeCount).Sum() +1;
                }

                 _context.Update(user);
                await _context.SaveChangesAsync();
            }           
           
            return Json(new { status = "200", likecount,isliked });
        }      
    }
}
