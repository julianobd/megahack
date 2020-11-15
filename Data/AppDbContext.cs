using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MegaHack5.Models;

namespace MegaHack5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<MegaHack5.Models.Company> Company { get; set; }
        public DbSet<MegaHack5.Models.Department> Department { get; set; }
        public DbSet<MegaHack5.Models.Status> Status { get; set; }
        public DbSet<MegaHack5.Models.Planning> Planning { get; set; }
        public DbSet<MegaHack5.Models.BusinessOccupation> BusinessOccupation { get; set; }
        public DbSet<MegaHack5.Models.Maturity> Maturity { get; set; }
        public DbSet<MegaHack5.Models.InternalInvestment> InternalInvestment { get; set; }
        public DbSet<MegaHack5.Models.File> File { get; set; }
    }
}
