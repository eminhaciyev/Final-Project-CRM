using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using static CRM.Utilities.Utilities;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly CRMdb _context;
        private readonly IHostingEnvironment _env;
        public ServiceController(CRMdb context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Index(Service) 
        public IActionResult Index()
        {
            List<Complaint> complaints = _context.Complaints.ToList();
            return View(complaints);
        }

        //Deleting Complaints (GET)
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(Id);
            if (complaint == null)
            {
                return NotFound();
            }
            return View(complaint);
        }

        //Deleting Complaints (POST)
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var DeletingElement = await _context.Complaints.FindAsync(Id);
            _context.Remove(DeletingElement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Service");
        }

        //Index(CV)
        public IActionResult CV_Index()
        {
            IEnumerable<CV> cv = _context.CVs.ToList();
            return View(cv);
        }

        //Delete(CV) (GET)
        public async Task<IActionResult> DeleteCV(int? Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            var CV = await _context.CVs.FindAsync(Id);
            if (CV == null)
            {
                return NotFound();
            }
            return View(CV);
        }

        //Delete(CV) (POST)
        [HttpPost]
        public async Task<IActionResult> DeleteCV(int Id)
        {
            var DeletingElement = await _context.CVs.FindAsync(Id);
            _context.CVs.Remove(DeletingElement);
            RemoveFile(_env.WebRootPath, "img", "cv", DeletingElement.CV_Name);
            await _context.SaveChangesAsync();
            return RedirectToAction("CV_Index", "Service");
        }

    }
}