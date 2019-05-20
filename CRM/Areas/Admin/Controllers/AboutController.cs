using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly CRMdb _context;
        public AboutController(CRMdb context)
        {
            _context = context;
        }

        //Index(About) GET
        public async Task<IActionResult> Index()
        {
            var model = await _context.Abouts.ToListAsync();
            return View(model);
        }

        //Create(About) GET
        [HttpGet]
        public IActionResult Create()
        {
            About about = new About();
            return View(about);
        }

        //Create(About) POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Description, Description_Az, Description_Ru")] About about)
        {
            if (!ModelState.IsValid) return View(about);

            _context.Abouts.Add(about);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Edit(About) GET
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var about = _context.Abouts.Find(id);

            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        //Edit(About) POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "Description, Description_Az, Description_Ru")] About about, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }

            var aboutBefore = await _context.Abouts.FindAsync(id);

            aboutBefore.Description = about.Description;
            aboutBefore.Description_Az = about.Description_Az;
            aboutBefore.Description_Ru = about.Description_Ru;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Delete(About) GET
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = _context.Abouts.Find(id);

            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        //Delete(About) POST
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var about = _context.Abouts.Find(id);

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}