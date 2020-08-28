﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19Tracing.Models
{
    public class Departments
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Department:", Prompt = "Add department here")]
        public string Department { get; set; }
    }
}
