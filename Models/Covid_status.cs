using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Tracing.Models
{
    public class Covid_status
    {


        [Key]
        [Required]
        [Display(Name = "ID", Prompt = "ID")]
        public int ID { get; set; }

       
        [Display(Name = "Patients id", Prompt = "Patients id")]
        //[MaxLength(50)]
        //[ForeignKey("ID")]
        public int Patients_id { get; set; }

        [Display(Name = "COVID19 Status", Prompt = "COVID19 Status")]
        [MaxLength(50)] 
        public string Status { get; set; }
          
        
        [Display(Name = "Date", Prompt = "Date")]
        [DataType(DataType.Date)]
        [MaxLength(50)] 
        public string Date { get; set; }


    }
}
