using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(100)]
        public string Title { get; set; }

        //Az
        [StringLength(100)]
        public string Title_Az { get; set; }

        //Ru
        [StringLength(100)]
        public string Title_Ru { get; set; }

        [Required]
        public string Description { get; set; }
        
        //Az
        public string Description_Az { get; set; }
        
        //Az
        public string Description_Ru { get; set; }

       
        public string Date { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
