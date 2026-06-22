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

    public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options ) : base ( options ) { }

    public ApplicationDbContext ( DbContextOptions<ApplicationDbContext> options,ITenantSetter tenantSetter )
        : base ( options )
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

    protected override void OnModelCreating ( ModelBuilder builder )
    {
        base.OnModelCreating ( builder );

        builder.Entity<TenantInfo> ( )
            .Property ( t => t.TenantId )
            .HasValueGenerator<Microsoft.EntityFrameworkCore.ValueGeneration.GuidValueGenerator> ( );


        builder.Entity<UserTenant> ( )
            .HasKey ( ut => new { ut.UserId,ut.TenantId,ut.TenantRole } );


        builder.Entity<ApplicationUser> ( )
            .HasIndex ( u => u.Email )
            .IsUnique ( );


        builder.Entity<UserTenant> ( )
            .HasOne ( ut => ut.User )
            .WithMany ( au => au.UserTenants )
            .HasForeignKey ( ut => ut.UserId );


        builder.Entity<Post> ( b =>
        {
            b.Property ( p => p.Price )
             .HasColumnType ( "decimal(18,2)" )
             .IsRequired ( );
        } );


        builder.Entity<Product> ( b =>
        {
            b.Property ( p => p.Price )
             .HasColumnType ( "decimal(18,2)" )
             .IsRequired ( );

            b.Property ( p => p.Discount )
             .HasColumnType ( "decimal(18,2)" );

            b.Property ( p => p.SaleCommission )
             .HasColumnType ( "decimal(18,2)" );
        } );

        TenantQueryFilter ( builder );
    }

    private void UserRoleSeed ( ModelBuilder builder,string userId,string tenantUserName,string tenant1Email,string password,string seedTenancyId,string roleId )
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

        user.PasswordHash = hasher.HashPassword ( user,password );
        builder.Entity<ApplicationUser> ( ).HasData ( user );
    }

    private void TenantQueryFilter ( ModelBuilder builder )
    {
        // Apply global query filter for data isolation across multi-tenant tables
        string currentTenant = _tenantSetter.CurrentTenantId;

        builder.Entity<Product> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<ProductImageFile> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<ProductComment> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AdminPost> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AdminImageFile> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AdminPostComment> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<Post> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<Panel> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<Page> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );

        builder.Entity<AValue> ( ).HasQueryFilter ( p => p.TenantId == _tenantSetter.CurrentTenantId );
    }

    public override int SaveChanges ( bool acceptAllChangesOnSuccess )
    {
        ApplyTenantId ( );

        return base.SaveChanges ( acceptAllChangesOnSuccess );
    }

    public override Task<int> SaveChangesAsync ( bool acceptAllChangesOnSuccess,CancellationToken cancellationToken = default )
    {
        ApplyTenantId ( );

        return base.SaveChangesAsync ( acceptAllChangesOnSuccess,cancellationToken );
    }

    private void ApplyTenantId ( )
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added && e.Entity is IMustHaveTenant);

        foreach ( var entry in entries )
        {
            ( ( IMustHaveTenant ) entry.Entity ).TenantId = _tenantSetter.CurrentTenantId;
        }
    }

    private void TenantSeed ( ModelBuilder builder,string seedTenantId,string name,string domain,EnumStoreType shopType )
    {
        builder.Entity<TenantInfo> ( ).HasData (
            new TenantInfo ( seedTenantId )
            {
                Name = name,
                Domain = domain,
                Store = shopType
            } );
    }

}
