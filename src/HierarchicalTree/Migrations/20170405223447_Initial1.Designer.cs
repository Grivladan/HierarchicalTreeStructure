using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HierarchicalTree.Data;

namespace HierarchicalTree.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170405223447_Initial1")]
    partial class Initial1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HierarchicalTree.Entities.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LocationCountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LocationCountryId");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Name");

                    b.Property<int>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("OfferingId");

                    b.HasKey("Id");

                    b.HasIndex("OfferingId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Family", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusinessId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Family");
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Offering", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FamilyId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("FamilyId");

                    b.ToTable("Offering");
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("OwnerId");

                    b.Property<int?>("Type");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("HierarchicalTree.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Business", b =>
                {
                    b.HasOne("HierarchicalTree.Entities.Country", "LocationCountry")
                        .WithMany("Businesses")
                        .HasForeignKey("LocationCountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Country", b =>
                {
                    b.HasOne("HierarchicalTree.Entities.Organization", "Organization")
                        .WithMany("Countries")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Department", b =>
                {
                    b.HasOne("HierarchicalTree.Entities.Offering", "Offering")
                        .WithMany("Departments")
                        .HasForeignKey("OfferingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Family", b =>
                {
                    b.HasOne("HierarchicalTree.Entities.Business", "Business")
                        .WithMany("Families")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Offering", b =>
                {
                    b.HasOne("HierarchicalTree.Entities.Family", "Family")
                        .WithMany("Offerings")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HierarchicalTree.Entities.Organization", b =>
                {
                    b.HasOne("HierarchicalTree.Models.ApplicationUser", "Owner")
                        .WithMany("Organizations")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HierarchicalTree.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HierarchicalTree.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HierarchicalTree.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
