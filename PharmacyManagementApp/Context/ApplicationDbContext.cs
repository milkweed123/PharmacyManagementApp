using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PharmacyManagementApp.Models.Data;

namespace PharmacyManagementApp.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Change> Changes { get; set; }
        public ApplicationDbContext():base("DefaultConnection")
        {

        }
    }
}
