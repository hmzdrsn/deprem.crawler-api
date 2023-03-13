using deprem.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deprem.Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        

        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base (options)
        {
            
        }
        public DbSet<deprem.Model.Models.deprem> Depremler { get; set; }


    }
}
