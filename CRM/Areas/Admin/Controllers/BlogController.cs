using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CRM.Extension.FileExtensions;
using static CRM.Utilities.Utilities;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly CRMdb _context;
        private readonly IHostingEnvironment _env;
        public BlogController(CRMdb context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Index(Blog) 
        public async Task<IActionResult> Index()
        {
            var model = await _context.Blogs.ToListAsync();
            return View(model);
        }

        //Create(Blog) (GET)
        [HttpGet]
        public IActionResult Create()
        {
            Blog blog = new Blog();
            return View(blog);
        }

        //Create(Blog) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Title, Title_Az, Title_Ru, Description, Description_Az, Description_Ru, Photo")] Blog blog)
        {
            if (!ModelState.IsValid) return View(blog);

            if (blog.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo can't be null");
                return View(blog);
            }

            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Photo type is not valid...");
                return View(blog);
            }

            blog.Image = await blog.Photo.SaveAsync(_env.WebRootPath, "img", "blog");

            blog.Date = DateTime.Now.ToString("MMM dd, yyyy");

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Edit(Blog) (GET)
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var blog = _context.Blogs.Find(id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        //Edit(Blog) (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind(include: "Title, Title_Az, Title_Ru, Description, Description_Az, Description_Ru, Photo")] Blog blog, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(blog);
            }

            var blogBefore = await _context.Blogs.FindAsync(id);

            if (blog.Photo != null)
            {
                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Photo type is not valid!");
                    return View(blog);
                }

                blogBefore.Image = await blog.Photo.SaveAsync(_env.WebRootPath, "img", "blog");

            }

            blogBefore.Title = blog.Title;
            blogBefore.Title_Az = blog.Title_Az;
            blogBefore.Title_Ru = blog.Title_Ru;
            blogBefore.Date = DateTime.Now.ToString("MMM dd, yyyy");
            blogBefore.Description = blog.Description;
            blogBefore.Description_Az = blog.Description_Az;
            blogBefore.Description_Ru = blog.Description_Ru;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //Delete(Blog) (GET)
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = _context.Blogs.Find(id);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        //Delete(Blog) (POST)
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var blog = _context.Blogs.Find(id);

            RemoveFile(_env.WebRootPath, "img", "blog", blog.Image);

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}