using InvestorsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestorsApi.Context
{
    public class InvestorDbContext : DbContext
    {
        public InvestorDbContext(DbContextOptions<InvestorDbContext> options) : base(options)
        {
        }

        public DbSet<Investor> Investors { get; set; }
        public DbSet<Commitment> Commitments { get; set; }
    }
}
