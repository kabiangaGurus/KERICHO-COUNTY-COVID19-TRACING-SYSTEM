using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Tracing.Models
{
    public class Covid19_contacts
    {
        [Key]

        public int ID { get; set; }

        [Required]
        [Display(Name = "Full names", Prompt = "Full names")]
        [MaxLength(50)]
        public string Full_names { get; set; }


      


    }
}
