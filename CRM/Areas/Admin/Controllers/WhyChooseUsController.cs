using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WhyChooseUsController : Controller
    {
        private readonly CRMdb _context;
        private readonly IHostingEnvironment _env;
        public WhyChooseUsController(CRMdb context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Index(WhyChooseUs)
        public async Task<IActionResult> Index()
        {
            var model = await _context.WhyChooseUs.ToListAsync();
            return View(model);
        }

        //Create(WhyChooseUs) (GET)
        [HttpGet]
        public IActionResult Create()
        {
            WhyChooseUs chooseUs = new WhyChooseUs();
            return View(chooseUs);
        }

        //Create(WhyChooseUs) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "IconClass, Title, Title_Az, Title_Ru, Description, Description_Az,Description_Ru")] WhyChooseUs chooseUs)
        {
            if (!ModelState.IsValid) return View(chooseUs);

            _context.WhyChooseUs.Add(chooseUs);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        //Edit(WhyChooseUs) (GET)
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var chooseUs = _context.WhyChooseUs.Find(id);

            if (chooseUs == null)
            {
                return NotFound();
            }

            return View(chooseUs);
        }

        //Edit(WhyChooseUs) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "IconClass, Title, Title_Az, Title_Ru, Description, Description_Az,Description_Ru")] WhyChooseUs chooseUs, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(chooseUs);
            }
            var testimonialBefore = await _context.WhyChooseUs.FindAsync(id);

            testimonialBefore.IconClass = chooseUs.IconClass;
            testimonialBefore.Title = chooseUs.Title;
            testimonialBefore.Title_Az = chooseUs.Title_Az;
            testimonialBefore.Title_Ru = chooseUs.Title_Ru;
            testimonialBefore.Description = chooseUs.Description;
            testimonialBefore.Description_Az = chooseUs.Description_Az;
            testimonialBefore.Description_Ru = chooseUs.Description_Ru;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Delete(WhyChooseUs) (GET)
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chooseUs = _context.WhyChooseUs.Find(id);

            if (chooseUs == null)
            {
                return NotFound();
            }

            return View(chooseUs);
        }

        //Delete(WhyChooseUs) (POST)
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var chooseUs = _context.WhyChooseUs.Find(id);
            _context.WhyChooseUs.Remove(chooseUs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}