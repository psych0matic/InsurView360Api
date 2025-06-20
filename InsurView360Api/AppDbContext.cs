using Microsoft.EntityFrameworkCore;

namespace InsurView360Api
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Member> Member { get; set; } = null!;

        public DbSet<Models.Claim> Claim { get; set; } = null!;

        public DbSet<Models.Dependent> Dependent { get; set; } = null!;
        
        public DbSet<Models.Document> Document { get; set; } = null!;

        public DbSet<Models.Policy> Policy { get; set; } = null!;

        public DbSet<Models.Payment> Payment { get; set; } = null!;
    }
}
