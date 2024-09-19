using Edupocket.Domain.AggregatesModel.WalletAggregate;
using Edupocket.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Edupocket.Infrastructure;

public class WalletDbContext: DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options): base(options)
    {        
    }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Wallet> Wallet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>().ToTable("Wallets").Property(t => t.Status).HasConversion<string>();
        modelBuilder.Entity<Profile>().ToTable("Profiles").Property(t => t.UserType).HasConversion<string>();
       
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var parameter = Expression.Parameter(entityType.ClrType, "p");
            if (!entityType.IsOwned())
            {
                var deletedCheck = Expression.Lambda(Expression.Equal(Expression.Property(parameter, "IsDeleted"), Expression.Constant(false)), parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
            }           
        }        


        base.OnModelCreating(modelBuilder);
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = "SYSTEM";
                    entry.Entity.CreatedByIp = "127.0.0.1";
                    break;
                case EntityState.Modified:
                    entry.Entity.DateUpdated = DateTime.Now;
                    entry.Entity.UpdatedBy = "SYSTEM";
                    entry.Entity.ModifiedByIp = "127.0.0.1";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = "SYSTEM";
                    entry.Entity.CreatedByIp = "127.0.0.1";
                    break;
                case EntityState.Modified:
                    entry.Entity.DateUpdated = DateTime.Now;
                    entry.Entity.UpdatedBy = "SYSTEM";
                    entry.Entity.ModifiedByIp = "127.0.0.1";
                    break;
            }
        }

        return base.SaveChanges();
    }


}
