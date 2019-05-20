using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Slider
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(100)]
        public string Title { get; set; }
        
        [StringLength(100)]
        public string TitleAz { get; set; }

        [StringLength(100)]
        public string TitleRu { get; set; }

        [StringLength(500)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
