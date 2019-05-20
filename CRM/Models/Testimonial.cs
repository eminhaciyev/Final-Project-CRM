using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.InnerModel
{
    public class Testimonial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(100)]
        public string Profession { get; set; }

        [StringLength(100)]
        public string Profession_Az { get; set; }

        [StringLength(100)]
        public string Profession_Ru { get; set; }

        [Required]
        public string  Desc { get; set; }

        public string Desc_Az { get; set; }

        public string Desc_Ru { get; set; }


        [StringLength(500)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
