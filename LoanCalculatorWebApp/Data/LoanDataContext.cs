using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoanCalculatorWebApp.Models
{
    public class LoanDataContext : DbContext
    {
        public LoanDataContext (DbContextOptions<LoanDataContext> options)
            : base(options)
        {
        }

        public DbSet<LoanCalculatorWebApp.Models.LoanData> LoanData { get; set; }
    }
}
