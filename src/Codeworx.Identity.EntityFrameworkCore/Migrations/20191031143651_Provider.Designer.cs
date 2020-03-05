﻿// <auto-generated />
using Codeworx.Identity.EntityFrameworkCore;
using Codeworx.Identity.EntityFrameworkCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Codeworx.Identity.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(CodeworxIdentityDbContext))]
    [Migration("20191031143651_Provider")]
    partial class Provider
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.AuthenticationProviderUser", b =>
                {
                    b.Property<Guid>("RightHolderId");

                    b.Property<Guid>("ProviderId");

                    b.Property<string>("ExternalIdentifier");

                    b.HasKey("RightHolderId", "ProviderId");

                    b.HasIndex("ProviderId");

                    b.ToTable("AuthenticationProviderUser");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.ClientConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("ClientSecret");

                    b.Property<byte[]>("ClientSecretSalt");

                    b.Property<string>("DefaultRedirectUri");

                    b.Property<int>("FlowTypes");

                    b.Property<TimeSpan>("TokenExpiration");

                    b.Property<string>("ValidRedirectUrls");

                    b.HasKey("Id");

                    b.ToTable("ClientConfiguration");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.ExternalAuthenticationProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndpointConfiguration");

                    b.Property<string>("EndpointType")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid?>("FilterId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("FilterId");

                    b.ToTable("ExternalAuthenticationProvider");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.ProviderFilter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ProviderFilter");

                    b.HasDiscriminator<string>("Type").HasValue("ProviderFilter");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.RightHolder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<Guid?>("RoleId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RightHolder");

                    b.HasDiscriminator<string>("Type").HasValue("RightHolder");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Tenant");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.TenantUser", b =>
                {
                    b.Property<Guid>("RightHolderId");

                    b.Property<Guid>("TenantId");

                    b.HasKey("RightHolderId", "TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("TenantUser");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.DomainNameProviderFilter", b =>
                {
                    b.HasBaseType("Codeworx.Identity.EntityFrameworkCore.Model.ProviderFilter");

                    b.Property<string>("DomainName");

                    b.ToTable("DomainNameProviderFilter");

                    b.HasDiscriminator().HasValue("Domain");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.IPv4ProviderFilter", b =>
                {
                    b.HasBaseType("Codeworx.Identity.EntityFrameworkCore.Model.ProviderFilter");

                    b.Property<byte[]>("RangeEnd")
                        .IsRequired();

                    b.Property<byte[]>("RangeStart")
                        .IsRequired();

                    b.ToTable("IPv4ProviderFilter");

                    b.HasDiscriminator().HasValue("IPv4");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.Role", b =>
                {
                    b.HasBaseType("Codeworx.Identity.EntityFrameworkCore.Model.RightHolder");

                    b.Property<Guid>("TenantId");

                    b.HasIndex("TenantId");

                    b.ToTable("Role");

                    b.HasDiscriminator().HasValue("Role");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.User", b =>
                {
                    b.HasBaseType("Codeworx.Identity.EntityFrameworkCore.Model.RightHolder");

                    b.Property<Guid?>("DefaultTenantId");

                    b.Property<bool>("IsDisabled");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasIndex("DefaultTenantId");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.AuthenticationProviderUser", b =>
                {
                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.ExternalAuthenticationProvider", "Provider")
                        .WithMany("Users")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.User", "User")
                        .WithMany("Providers")
                        .HasForeignKey("RightHolderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.ExternalAuthenticationProvider", b =>
                {
                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.ProviderFilter", "Filter")
                        .WithMany()
                        .HasForeignKey("FilterId");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.RightHolder", b =>
                {
                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.Role")
                        .WithMany("Members")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.TenantUser", b =>
                {
                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.User", "User")
                        .WithMany("Tenants")
                        .HasForeignKey("RightHolderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.UserRole", b =>
                {
                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.Role", "Role")
                        .WithMany("MemberOf")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.User", "User")
                        .WithMany("MemberOf")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.Role", b =>
                {
                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Codeworx.Identity.EntityFrameworkCore.Model.User", b =>
                {
                    b.HasOne("Codeworx.Identity.EntityFrameworkCore.Model.Tenant", "DefaultTenant")
                        .WithMany()
                        .HasForeignKey("DefaultTenantId");
                });
#pragma warning restore 612, 618
        }
    }
}
