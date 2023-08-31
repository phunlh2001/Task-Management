using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Security.JWT;
using backend.Services;
using backend.Services.Interfaces;

namespace backend.Configurations
{
    public static class ApplicationLayerExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDataSeedingService, DataSeedingService>();
            services.AddScoped<IWorkSpaceService, WorkSpaceService>();
            services.AddScoped<ITaskListService, TaskListService>();
            services.AddScoped<ITaskDetailService, TaskDetailService>();
            
            services.AddScoped<JWTTokenProvider>();


            return services;
        }
    }
}