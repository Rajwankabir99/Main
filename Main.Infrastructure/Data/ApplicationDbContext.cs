using Domain.Model;

using Main.Common.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Data;

namespace Main.Infrastructure;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    public readonly ITenantSetter _tenantSetter;

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options,ITenantSetter tenantSetter)
        : base (options)
    {
        _tenantSetter = tenantSetter;

    }

    public DbSet<TenantInfo> Tenants
    {
        get; set;
    }
    public DbSet<UserTenant> UserTenants
    {
        get; set;
    }

    public DbSet<Page> Pages
    {
        get; set;
    }

    public DbSet<Panel> Panels
    {
        get; set;
    }

    public DbSet<Post> Posts
    {
        get; set;
    }

    public DbSet<Product> Products
    {
        get; set;
    }

    public DbSet<ProductImageFile> ProductImageFiles
    {
        get; set;
    }

    public DbSet<ProductComment> ProductComments
    {
        get; set;
    }

    public DbSet<AdminPost> AdPosts
    {
        get; set;
    }

    public DbSet<AdminPostComment> AdPostComments
    {
        get; set;
    }

    public DbSet<AdminImageFile> AdImageFiles
    {
        get; set;
    }

    protected override void OnModelCreating (ModelBuilder builder)
    {
        base.OnModelCreating (builder);

        _ = builder.Entity<TenantInfo> ()
            .Property (t => t.TenantId)
            .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.GuidValueGenerator> ();


        _ = builder.Entity<UserTenant> ()
            .HasKey (ut => new { ut.UserId,ut.TenantId,ut.TenantRole });


        _ = builder.Entity<ApplicationUser> ()
            .HasIndex (u => u.Email)
            .IsUnique ();


        _ = builder.Entity<UserTenant> ()
            .HasOne (ut => ut.User)
            .WithMany (au => au.UserTenants)
            .HasForeignKey (ut => ut.UserId);


        _ = builder.Entity<Post> (b =>
        {
            _ = b.Property (p => p.Price)
             .HasColumnType ("decimal(18,2)")
             .IsRequired ();
        });


        _ = builder.Entity<Product> (b =>
        {
            _ = b.Property (p => p.Price)
             .HasColumnType ("decimal(18,2)")
             .IsRequired ();

            _ = b.Property (p => p.Discount)
             .HasColumnType ("decimal(18,2)");

            _ = b.Property (p => p.SaleCommission)
             .HasColumnType ("decimal(18,2)");
        });

        TenantQueryFilter (builder);
    }

    private void UserRoleSeed (ModelBuilder builder,string userId,string tenantUserName,string tenant1Email,string password,string seedTenancyId,string roleId)
    {
        var hasher = new PasswordHasher<IdentityUser>();

        var user = new ApplicationUser
        {
            Id = userId,
            UserName = tenantUserName,
            NormalizedUserName = tenantUserName.ToUpper(),
            Email = tenant1Email,
            NormalizedEmail = tenant1Email.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        user.PasswordHash = hasher.HashPassword (user,password);
        _ = builder.Entity<ApplicationUser> ().HasData (user);
    }

    private void TenantQueryFilter (ModelBuilder builder)
    {
        // Apply global query filter for data isolation across multi-tenant tables
        string currentTenant = _tenantSetter.CurrentTenantId;

        _ = builder.Entity<Product> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<ProductImageFile> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<ProductComment> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<AdminPost> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<AdminImageFile> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<AdminPostComment> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<Post> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<Panel> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<Page> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);

        _ = builder.Entity<AValue> ().HasQueryFilter (p => p.TenantId == _tenantSetter.CurrentTenantId);
    }

    public override int SaveChanges (bool acceptAllChangesOnSuccess)
    {
        ApplyTenantId ();

        return base.SaveChanges (acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync (bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default)
    {
        ApplyTenantId ();

        return base.SaveChangesAsync (acceptAllChangesOnSuccess,cancellationToken);
    }

    private void ApplyTenantId ()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added && e.Entity is IMustHaveTenant);

        foreach ( var entry in entries )
        {
            ( ( IMustHaveTenant ) entry.Entity ).TenantId = _tenantSetter.CurrentTenantId;
        }
    }

    private void TenantSeed (ModelBuilder builder,string seedTenantId,string name,string domain,EnumStoreType shopType)
    {
        _ = builder.Entity<TenantInfo> ().HasData (
            new TenantInfo (seedTenantId)
            {
                Name = name,
                Domain = domain,
                Store = shopType
            });
    }

}
