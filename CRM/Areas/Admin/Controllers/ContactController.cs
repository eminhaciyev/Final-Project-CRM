using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly CRMdb _context;
        private readonly IHostingEnvironment _env;
        public ContactController(CRMdb context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Index(Contact) 
        public IActionResult Index()
        {
            List<Contact> contacts = _context.Contacts.ToList();
            return View(contacts);
        }

        //Deleting Contact (GET)
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(Id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        //Deleting Contact (POST)
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var DeletingElement = await _context.Contacts.FindAsync(Id);
            _context.Remove(DeletingElement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Contact");
        }
    }
}