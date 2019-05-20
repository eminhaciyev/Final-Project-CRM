using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class WhyChooseUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,StringLength(50)]
        public string IconClass { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Title_Az { get; set; }

        [StringLength(50)]
        public string Title_Ru { get; set; }

        [Required, StringLength(600)]
        public string Description { get; set; }

        [StringLength(600)]
        public string Description_Az { get; set; }

        [StringLength(600)]
        public string Description_Ru { get; set; }

    }
}
