using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Tracing.Models
{
    public class SuspectedCases
    {
        [Key]
        [Required]
        public int ID { get; set; }
        
       
        [Display(Name = "Primary patient ID", Prompt = "Enter id here")]
        public int P_patient_id { get; set; }

        
        [Display(Name = "Full names", Prompt = "Full names")]
        [MaxLength(50)]
        public string Full_names { get; set; }


        
        [Display(Name = "Location", Prompt = "Location")]
        public string S_patient_id { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Phone", Prompt = "Phone")]
        public int Phone_number { get; set; }
    }
}
