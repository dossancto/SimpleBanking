using Microsoft.EntityFrameworkCore;
using SimpleBanking.Infra.Database.EF.Entities;

namespace SimpleBanking.Infra.Database.EF.Contexts;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<EFPerson> Persons { get; set; } = default!;
    public DbSet<EFMerchant> Merchants { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
