using CRM.Models.InnerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModel
{
    public class ViewModelClasses
    {
        public ViewModelClasses()
        {
            RegisterModel = new RegisterModel();
            LoginModel = new LoginModel();
        }


        public RegisterModel RegisterModel { get; set; }

        public LoginModel LoginModel { get; set; }

        public CV _CV { get; set; }

        public Complaint _Complaint { get; set; }

        public User User { get; set; }

        public IEnumerable<Slider> Sliders { get; set; }

        public About About { get; set; }

        public IEnumerable<Testimonial> Testimonials { get; set; }

        public IEnumerable<WhyChooseUs> WhyChooseUs { get; set; }

        public IEnumerable<Blog> Blogs { get; set; }

        public Feedbacks Feedbacks { get; set; }

        public Contact Contacts { get; set; }


    }
}
