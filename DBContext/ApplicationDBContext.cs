using Covid19Tracing.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuela.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Administration> Administration { get; set; }


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
           : base(options)
        {

        }


        public DbSet<Covid19Tracing.Models.Covid_status> Covid_status { get; set; }


        public DbSet<Covid19Tracing.Models.Patients> Patients { get; set; }


        public DbSet<Covid19Tracing.Models.Departments> Departments { get; set; }


        public DbSet<Covid19Tracing.Models.SuspectedCases> SuspectedCases { get; set; }



    }
}
