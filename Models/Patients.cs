using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Tracing.Models
{
    public class Patients
    {


        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "National ID/Birth Cert No.", Prompt = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Full names", Prompt = "Full names")]
        [MaxLength(50)]
        public string Full_names { get; set; }

        
       
    
        [Display(Name = "Age", Prompt = "Age")]
        public int Age { get; set; }
        
        
        
        
        [Display(Name = "Location", Prompt = "Location")]
        public string Location { get; set; }


        
        [Display(Name = "Phone number", Prompt = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public int Phone_number { get; set; }

        
        [Display(Name = "Date", Prompt = "Date")]
        [DataType(DataType.DateTime)]
        public string Date { get; set; }

        
        [Display(Name = "Condition", Prompt = "Condition")]
        //[DataType(DataType.Password)]
        public string Condition { get; set; }



        //[Required]
        [Display(Name = "History of travel", Prompt = "History of travel")]
        public string T_History { get; set; }

       









    }
}
