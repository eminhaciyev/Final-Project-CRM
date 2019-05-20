using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Extension;
using CRM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CRM.Utilities.Utilities;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly CRMdb _context;
        private readonly IHostingEnvironment _env;
        public SliderController(CRMdb context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Index(Slider) 
        public async Task<IActionResult> Index()
        {
            var model = await _context.Sliders.ToListAsync();
            return View(model);
        }

        //Create(Slider) (GET)
        [HttpGet]
        public IActionResult Create()
        {
            Slider slider = new Slider();
            return View(slider);
        }

        //Create(Slider) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Title, TitleAz, TitleRu, Photo")] Slider slider)
        {
            if (!ModelState.IsValid) return View(slider);

            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can't be null");
                return View(slider);
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Photo type is not valid...");
                return View(slider);
            }

            slider.Image = await slider.Photo.SaveAsync(_env.WebRootPath, "img", "slider");

            _context.Sliders.Add(slider);
           await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Edit(Slider) (GET)
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var slider = _context.Sliders.Find(id);

            if (slider == null)
            {
                return NotFound();
            }
          
            return View(slider);
        }

        //Edit(Slider) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "Title, TitleAz, TitleRu, Photo")] Slider slider, int id )
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            var sliderBefore = await _context.Sliders.FindAsync(id);

            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Photo type is not valid!");
                    return View(slider);
                }

                sliderBefore.Image = await slider.Photo.SaveAsync(_env.WebRootPath, "img", "slider");                
            }

            sliderBefore.Title = slider.Title;
            sliderBefore.TitleAz = slider.TitleAz;
            sliderBefore.TitleRu = slider.TitleRu;

           await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Delete(Slider) (GET)
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = _context.Sliders.Find(id);

            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        //Delete(Slider) (POST)
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        { 
            var slider = _context.Sliders.Find(id);

            RemoveFile(_env.WebRootPath, "img", "slider",slider.Image);

            _context.Sliders.Remove(slider);
           await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}