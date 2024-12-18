using AutoMapper;
using backend.Models.Dtos.Authentication;
using backend.Models.Dtos.TaskDetail;
using backend.Models.Dtos.TaskList;
using backend.Models.Dtos.UserWorkSpace;
using backend.Models.Entities;

namespace backend.Configurations
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<UserWorkSpace, AddWorkSpace>().ReverseMap();
            CreateMap<UserWorkSpace, EditWorkSpace>().ReverseMap();
            CreateMap<UserWorkSpace, WorkSpaceResult>().ReverseMap();
            CreateMap<TaskList, AddTaskList>().ReverseMap();
            CreateMap<TaskList, EditTaskList>().ReverseMap();
            CreateMap<TaskList, TaskListResult>().ReverseMap();
            CreateMap<TaskDetail, AddTaskDetail>().ReverseMap();
            CreateMap<TaskDetail, EditTaskDetail>().ReverseMap();
            CreateMap<TaskDetail, TaskDetailResult>().ReverseMap();
            CreateMap<AppUser, RegisterModel>().ReverseMap();
        }
    }
}