﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data.Context;

#nullable disable

namespace backend.Data.Migrations
{
    [DbContext(typeof(TaskManagerContext))]
    [Migration("20230810165412_M01_DataInitial")]
    partial class M01_DataInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("backend.Models.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(150)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserAvatar")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UserTheme")
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "63347488-03b2-44c1-86d7-e1281af868a0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ce4673f5-291d-4694-81d6-5447213665dd",
                            CreatedDate = new DateTime(2023, 8, 10, 23, 54, 12, 225, DateTimeKind.Local).AddTicks(7309),
                            Email = "SystemAdmin@123",
                            EmailConfirmed = true,
                            FullName = "Admin system 0",
                            IsDeleted = false,
                            LastModify = new DateTime(2023, 8, 10, 23, 54, 12, 225, DateTimeKind.Local).AddTicks(7323),
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEHOovOv9J9wCB+8EXJ2sJDQNye5Xn4C/s/PquXo/NngVQQ14S9O+G8sfl2OhKmmzaQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ba144bf7-708d-4c10-b03f-9919584dbfab",
                            TwoFactorEnabled = false,
                            UserAvatar = "not set",
                            UserName = "AdminSystem",
                            UserTheme = "not set"
                        });
                });

            modelBuilder.Entity("backend.Models.Entities.TaskDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ListId");

                    b.ToTable("TaskDetail", (string)null);
                });

            modelBuilder.Entity("backend.Models.Entities.TaskList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("TableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("varchar(5000)");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("TaskList", (string)null);
                });

            modelBuilder.Entity("backend.Models.Entities.TaskTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Tilte")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("WorkSpaceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("TaskTable", (string)null);
                });

            modelBuilder.Entity("backend.Models.Entities.UserWorkSpace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("varchar(3000)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("WorkSpace", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1dc78625-a3ad-4bf8-9730-d1f06e3a2b7b",
                            ConcurrencyStamp = "34025571-1d8e-4d7c-888d-f7ec06285163",
                            Name = "Member"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(150)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "63347488-03b2-44c1-86d7-e1281af868a0",
                            RoleId = "1dc78625-a3ad-4bf8-9730-d1f06e3a2b7b"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Value")
                        .HasColumnType("varchar(150)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("backend.Models.Entities.TaskDetail", b =>
                {
                    b.HasOne("backend.Models.Entities.TaskList", "OfList")
                        .WithMany("GetDetails")
                        .HasForeignKey("ListId")
                        .IsRequired();

                    b.Navigation("OfList");
                });

            modelBuilder.Entity("backend.Models.Entities.TaskList", b =>
                {
                    b.HasOne("backend.Models.Entities.TaskTable", "OfTable")
                        .WithMany("GetLists")
                        .HasForeignKey("TableId")
                        .IsRequired();

                    b.Navigation("OfTable");
                });

            modelBuilder.Entity("backend.Models.Entities.TaskTable", b =>
                {
                    b.HasOne("backend.Models.Entities.UserWorkSpace", "WorkSpace")
                        .WithMany("UserTables")
                        .HasForeignKey("WorkSpaceId")
                        .IsRequired();

                    b.Navigation("WorkSpace");
                });

            modelBuilder.Entity("backend.Models.Entities.UserWorkSpace", b =>
                {
                    b.HasOne("backend.Models.Entities.AppUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("backend.Models.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("backend.Models.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .IsRequired();

                    b.HasOne("backend.Models.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("backend.Models.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired();
                });

            modelBuilder.Entity("backend.Models.Entities.TaskList", b =>
                {
                    b.Navigation("GetDetails");
                });

            modelBuilder.Entity("backend.Models.Entities.TaskTable", b =>
                {
                    b.Navigation("GetLists");
                });

            modelBuilder.Entity("backend.Models.Entities.UserWorkSpace", b =>
                {
                    b.Navigation("UserTables");
                });
#pragma warning restore 612, 618
        }
    }
}