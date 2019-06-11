using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanCalculatorWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace LoanCalculatorWebApp.Models
{
    public class AppDataDbContext : DbContext, IAppDataDbContext
    {
        public AppDataDbContext (DbContextOptions<AppDataDbContext> options)
            : base(options)
        {
        }

        public DbContext Instance => this;
        public DbSet<LoanData> LoanData { get; set; }
    }
}
