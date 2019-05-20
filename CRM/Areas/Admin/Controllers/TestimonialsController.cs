using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Extension;
using CRM.Models.InnerModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CRM.Utilities.Utilities;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialsController : Controller
    {
        private readonly CRMdb _context;
        private readonly IHostingEnvironment _env;
        public TestimonialsController(CRMdb context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Index(Testimonials) 
        public async Task<IActionResult> Index()
        {
            var model = await _context.Testimonials.ToListAsync();
            return View(model);
        }

        //Create(Testimonials) (GET)
        [HttpGet]
        public IActionResult Create()
        {
            Testimonial testimonial = new Testimonial();
            return View(testimonial);
        }

        //Create(Testimonials) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "FullName, Profession, Profession_Az, Profession_Ru, Desc, Desc_Az,Desc_Ru, Photo")] Testimonial testimonial)
        {
            if (!ModelState.IsValid) return View(testimonial);

            if (testimonial.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can't be null");
                return View(testimonial);
            }

            if (!testimonial.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Photo type is not valid...");
                return View(testimonial);
            }

            testimonial.Image = await testimonial.Photo.SaveAsync(_env.WebRootPath, "img", "testimonial");

            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Edit(Testimonials) (GET)
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var testimonial = _context.Testimonials.Find(id);

            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        //Edit(Testimonials) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "FullName, Profession, Profession_Az, Profession_Ru, Desc, Desc_Az,Desc_Ru, Photo")] Testimonial testimonial, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(testimonial);
            }

            var testimonialBefore = await _context.Testimonials.FindAsync(id);

            if (testimonial.Photo != null)
            {
                if (!testimonial.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Photo type is not valid!");
                    return View(testimonial);
                }
                testimonialBefore.Image = await testimonial.Photo.SaveAsync(_env.WebRootPath, "img", "testimonial");

            }
            testimonialBefore.FullName = testimonial.FullName;
            testimonialBefore.Profession = testimonial.Profession;
            testimonialBefore.Profession_Az = testimonial.Profession_Az;
            testimonialBefore.Profession_Ru = testimonial.Profession_Ru;
            testimonialBefore.Desc = testimonial.Desc;
            testimonialBefore.Desc_Az = testimonial.Desc_Az;
            testimonialBefore.Desc_Ru = testimonial.Desc_Ru;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Delete(Testimonials) (GET)
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = _context.Testimonials.Find(id);

            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        //Delete(Testimonials) (POST)
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var testimonial = _context.Testimonials.Find(id);

            RemoveFile(_env.WebRootPath, "img", "blog", testimonial.Image);

            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}