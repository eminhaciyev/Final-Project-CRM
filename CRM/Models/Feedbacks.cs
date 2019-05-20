using CRM.Models.InnerModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models
{
    public class Feedbacks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Like { get; set; }

        public bool DisLike { get; set; }

        public int LikeCount { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
