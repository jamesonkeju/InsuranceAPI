﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Insurance.Data.Models;
using Insurance.Data.Models.Domains;

using Insurance.Data.Models.Indentity;

namespace Insurance.Data
{
    public class InsuranceAppContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole,
        IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>

    {

        public InsuranceAppContext(DbContextOptions<InsuranceAppContext> options) : base(options)
        {
            //Database.Migrate();

        }


        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<InsuranceAppContext>
        {

            public InsuranceAppContext CreateDbContext(string[] args)
            {

                var optionsBuilder = new DbContextOptionsBuilder<InsuranceAppContext>();

                var config = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false)
                  .Build();



                var connect = config.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();
                // optionsBuilder.UseSqlServer(connect);
                optionsBuilder.UseSqlServer(connect, options => options.MigrationsAssembly("Insurance.Data"));

                return new InsuranceAppContext(optionsBuilder.Options);
            }
        }

        public InsuranceAppContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();



            var connect = config.GetSection("ConnectionStrings").Get<List<string>>().FirstOrDefault();
            // optionsBuilder.UseSqlServer(connect);
            optionsBuilder.UseSqlServer(connect,options=>options.MigrationsAssembly("Insurance.Data"));
        }
        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-3.1&tabs=visual-studio
        /// </summary>
        /// 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Seed();

            builder.Ignore<ApplicationUserLogin>();
            //  builder.Ignore<ApplicationUserRole>();
            builder.Ignore<UserLoginHistory>();



            builder.Entity<ApplicationUser>(b => {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.HasMany(x => x.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.Roles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            //  builder.Entity<ApplicationUserRole>().HasNoKey();
            //  base.OnModelCreating(builder);
        }

        #region StartUp
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<ApplicationUserPasswordHistory> ApplicationUserPasswordHistorys { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<ProviderList> ProviderLists { get; set; }
       
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ApplicationSession> applicationSessions { get; set; }

        public DbSet<ProductBenefit>  productBenefits { get; set; }

        public DbSet<ProductList>  productLists { get; set; }
        public DbSet<CustomerKYC> CustomerKYCs { get; set; }
        public DbSet<LGA> LGAs { get; set; }



        #endregion





        #region Errorlog::
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<PartnerRequest> PartnerRequests { get; set; }
        public DbSet<CustomerPolicy> CustomerPolicies { get; set; }

        #endregion

       

        public async Task<bool> TrySaveChangesAsync(ILogger logger)
        {
            try
            {
                await SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                logger.LogError($"unable to add  >>>>> {e.Message}");
                logger.LogError($"DB add  Inner Exception Message >>>>> {e.InnerException?.Message}");
                return false;
            }
        }

    }
}

