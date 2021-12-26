using Honoured.ArtDisciplines;
using Honoured.Artists;
using Honoured.ArtistSubscriptions;
using Honoured.ArtLovers;
using Honoured.ArtWorks;
using Honoured.CareInstructions;
using Honoured.Countries;
using Honoured.Deliveries;
using Honoured.Dimensions;
using Honoured.Enumerations;
using Honoured.Markets;
using Honoured.Models;
using Honoured.Placements;
using Honoured.Subscriptions;
using Honoured.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Honoured.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class HonouredDbContext : 
        AbpDbContext<HonouredDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */
        
        #region Entities from the modules
        
        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */
        
        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }
        
        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion
        
        public HonouredDbContext(DbContextOptions<HonouredDbContext> options)
            : base(options)
        {

        }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside HonouredDbContextModelCreatingExtensions.ConfigureHonoured
         */
        public DbSet<Address> Addresses { get; set; }

        public DbSet<ArtDiscipline> Categories { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<ArtWork> ArtPieces { get; set; }

        public DbSet<ContactPoint> ContactPoints { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<ArtistPersonalInfo> ArtistPersonalInfos { get; set; }

        public ArtLoverPersonalInfo ArtLoverPersonalInfos { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ArtLover> ArtLovers { get; set; }

        public DbSet<Placement> Placements { get; set; }

        public DbSet<ArtLoverSubscription> Subscriptions { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Market> Areas { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Dimension> Dimensions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */

            Check.NotNull(builder, nameof(builder));

            builder.Entity<ContactPoint>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "ContactPoints", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Address>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Addresses", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Person>()
                .HasDiscriminator<PersonType>(prop => prop.ParentType)
                    .HasValue<ArtistPersonalInfo>(PersonType.Artist)
                    .HasValue<ArtLoverPersonalInfo>(PersonType.ArtLover)
                    .HasValue<Person>(PersonType.Admin);
            builder.Entity<Person>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Persons", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<ArtDiscipline>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Disciplines", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<ArtLoverDiscipline>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "SubsCatgories", HonouredConsts.DbSchema);
                b.HasKey(s => new { s.ArtDisciplineId, s.ArtLoverId });
            });

            builder.Entity<ArtLoverDiscipline>(b =>
            {
                b.HasOne(sc => sc.ArtLover)
                 .WithMany(c => c.Categories)
                 .HasForeignKey(s => s.ArtLoverId);
            });

            builder.Entity<ArtLoverPersonalInfo>().HasBaseType<Person>();
            builder.Entity<ArtLover>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "ArtLovers", HonouredConsts.DbSchema);
                b.HasOne<ArtLoverPersonalInfo>(a => a.Profile);
                b.HasMany(a => a.Categories).WithOne(b => b.ArtLover).HasForeignKey(s => s.ArtLoverId);
                b.ConfigureByConvention();
            });

            builder.Entity<ArtWorkDiscipline>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "ArtworkDisciplines", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
                //b.HasKey(s => new { s.DisciplineId, s.ArtWorkId });
            });

            builder.Entity<ArtLoverDiscipline>(b =>
            {
                b.HasOne(sc => sc.ArtLover)
                 .WithMany(c => c.Categories)
                 .HasForeignKey(s => s.ArtLoverId);
            });

            builder.Entity<ArtWork>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "ArtPieces", HonouredConsts.DbSchema);
                b.HasMany<ArtDiscipline>(a => a.Categories).WithMany(m => m.ArtWorks)
                        .UsingEntity<ArtWorkDiscipline>(a => a.HasOne(a => a.Discipline).WithMany().OnDelete(DeleteBehavior.NoAction),
                                                    a => a.HasOne(a => a.ArtWork).WithMany().OnDelete(DeleteBehavior.NoAction));
                b.HasOne<ArtDiscipline>(a => a.MainDiscipline);
                b.ConfigureByConvention();
            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Tags", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Placement>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Placements", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Country>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Countries", HonouredConsts.DbSchema);

                b.ConfigureByConvention();
            });

            builder.Entity<Market>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Markets", HonouredConsts.DbSchema);

                b.ConfigureByConvention();
            });


            builder.Entity<Dimension>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Dimensions", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });


            builder.Entity<ArtistPersonalInfo>().HasBaseType<Person>();
            builder.Entity<ArtistMarket>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "ArtistMarkets", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<ArtistDiscipline>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "ArtistDisciplines", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<ArtistSubscription>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "ArtistsSubscriptions", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<Artist>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Artists", HonouredConsts.DbSchema);
                b.HasOne<ArtistPersonalInfo>(a => a.PersonalDetails);
                b.HasOne<ContactPoint>(c => c.DefaultContactPoint);
                b.HasMany<ArtWork>(a => a.Portfolio).WithOne(a => a.Artist);
                b.HasMany<Market>(a => a.SubscribedMarkets).WithMany(m => m.SubscribedArtists)
                        .UsingEntity<ArtistMarket>(a => a.HasOne(a => a.Market).WithMany().HasForeignKey(a => a.MarketId),
                                                    a => a.HasOne(a => a.Artist).WithMany().HasForeignKey(a => a.Artistid));
                b.HasMany<ArtDiscipline>(a => a.Disciplines).WithMany(a => a.Artists)
                        .UsingEntity<ArtistDiscipline>(a => a.HasOne(a => a.Discipline).WithMany().OnDelete(DeleteBehavior.NoAction),
                                                        a => a.HasOne(a => a.Artist).WithMany().HasForeignKey(a => a.ArtistId).OnDelete(DeleteBehavior.NoAction));
                b.ConfigureByConvention();
            });

            builder.Entity<Delivery>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Deliveries", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<SubscriptionTier>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "SubscriptionTiers", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });


            builder.Entity<ArtLoverSubscription>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "Subscriptions", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

            builder.Entity<CareInstruction>(b =>
            {
                b.ToTable(HonouredConsts.DbTablePrefix + "CareInstructions", HonouredConsts.DbSchema);
                b.ConfigureByConvention();
            });

        }
    }
}
