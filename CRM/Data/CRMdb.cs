using CRM.Models;
using CRM.Models.InnerModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class CRMdb : IdentityDbContext<User>
    {
        //private readonly UserManager<User> _userManager;
        //public CRMdb(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        public CRMdb(DbContextOptions<CRMdb> options) : base(options) { }

        public DbSet<User> ApplicationUser { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        public DbSet<SpeedInternet> SpeedInternets { get; set; }
        public DbSet<Package> Packages { get; set; }   

        public DbSet<HomeNumber> HomeNumbers { get; set; }
        public DbSet<MobileNumber> MobileNumbers { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<WhyChooseUs> WhyChooseUs { get; set; }
        public DbSet<UserBalance> UserBalances { get; set; }

        public DbSet<Feedbacks> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }


       

    }
}

