using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Context;
using backend.Models.Entities;
using backend.Services.Interfaces;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public class DataSeedingService : IDataSeedingService
    {
        private readonly UserManager<AppUser> _useMng;
        private readonly TaskManagerContext _db;

        public DataSeedingService(UserManager<AppUser> useMng, TaskManagerContext db)
        {
            _useMng = useMng;
            _db = db;
        }

        private Faker<UserWorkSpace> GenerateWorSpace()
        {

            var user = _useMng.FindByEmailAsync("SystemAdmin@123").Result;

            return new Faker<UserWorkSpace>()
                .RuleFor(e => e.OwnerId, f => user.Id )
                .RuleFor(e => e.Title, f => f.Name.JobTitle())
                .RuleFor(e => e.Description, f => f.Lorem.Paragraph())
                .RuleFor(e => e.Title, f => f.Name.JobTitle())
                .RuleFor(e => e.GetTaskLists, (f, e) =>
                {
                    return GenerateTaskList(e.Id).Generate(3);
                });
        }

        private Faker<TaskList> GenerateTaskList(Guid workSpaceId)
        {
            return new Faker<TaskList>()
                .RuleFor(e => e.WorkSpaceId, f => workSpaceId)
                .RuleFor(e => e.Title, f => f.Name.JobTitle())
                .RuleFor(e => e.GetDetails, (f, e) =>
                {
                    return GenerateTask(e.Id).Generate(3);
                });
                
        }

        private Faker<TaskDetail> GenerateTask(Guid listId)
        {
            return new Faker<TaskDetail>()
                .RuleFor(e => e.ListId, f => listId)
                .RuleFor(e => e.Detail, f => f.Lorem.Paragraph(10));
                
                
        }

        

        public bool SeedWorSpace(int number = 1)
        {
            if(number < 0) return false;
            var data = GenerateWorSpace().Generate(number);
            _db.AddRange(data);
            _db.SaveChanges();
            return true; 
        }

        public bool SeedTaskList(Guid workSpaceId, int number = 1)
        {
            if(number < 0) return false;
            var data = GenerateTaskList(workSpaceId).Generate(number);
            _db.AddRange(data);
            _db.SaveChanges();
            return true; 
        }

        public bool SeedTaskDetails(Guid listId, int number = 1)
        {
            if(number < 0) return false;
            var data = GenerateTask(listId).Generate(number);
            _db.AddRange(data);
            _db.SaveChanges();
            return true; 
        }
    }
}