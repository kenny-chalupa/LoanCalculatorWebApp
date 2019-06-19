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
