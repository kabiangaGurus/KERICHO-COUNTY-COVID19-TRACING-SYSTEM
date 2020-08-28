using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Tracing.Models
{
    public class Status
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Status ", Prompt = "Add")]
        public string C_status { get; set; }

    }
}
