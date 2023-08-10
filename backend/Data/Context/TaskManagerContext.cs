using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Defaults;
using backend.Models.Entities;
using backend.Models.Entities.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Context
{
    public class TaskManagerContext: IdentityDbContext<AppUser>
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {

        }

        public TaskManagerContext()
        {
        }

        public override DbSet<AppUser> Users { get; set; }
        public DbSet<UserWorkSpace> WorkSpaces { get; set; }
        public DbSet<TaskTable> TaskTables { get; set; }
        public DbSet<TaskList> TaskLists { get; set; }
        public DbSet<TaskDetail> Details { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            EntitiesSetting(modelBuilder);
            GeneralSetting(modelBuilder);
            DataInit(modelBuilder);

        }

        private void EntitiesSetting(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserWorkSpace>()
            .ToTable("WorkSpace");

            modelBuilder.Entity<TaskTable>()
            .ToTable("TaskTable");

            modelBuilder.Entity<TaskList>()
            .ToTable("TaskList");

            modelBuilder.Entity<TaskDetail>()
            .ToTable("TaskDetail");

        }

        private void GeneralSetting(ModelBuilder modelBuilder)
        {
            //common string field lenght
            foreach(var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(150)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagerContext).Assembly);

            //set delete behavior
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) { 

                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            //name setting
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();

                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    tableName = tableName.Substring(6);

                }
                if (tableName != null && tableName[^1] == 's' && tableName[^2] != 's')
                {
                    tableName = tableName.Substring(0, tableName.Length - 1);
                }

                entity.SetTableName(tableName);
            }
        }

        private void DataInit(ModelBuilder modelBuilder)
        {
            var roles = RoleName.Roles;
            var dbroles = new List<IdentityRole>();
            var userroles = new List<IdentityUserRole<string>>();

            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                UserName = "AdminSystem",
                Email = "SystemAdmin@123",
                EmailConfirmed = true,
                FullName = "Admin system 0"

            };
            user.PasswordHash = hasher.HashPassword(user, "@123456");

            foreach (var role in roles)
            {
                dbroles.Add(new IdentityRole
                {
                    Name = role,
                });
            }


            foreach (var role in dbroles)
            {
                userroles.Add(
                    new IdentityUserRole<string>
                    {
                        RoleId = role.Id,
                        UserId = user.Id,
                    }
                );
            }

            modelBuilder.Entity<AppUser>().HasData(user);
            modelBuilder.Entity<IdentityRole>().HasData(
                dbroles
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                userroles
            );
        }
    }
}