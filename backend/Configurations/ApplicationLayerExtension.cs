using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Services;
using backend.Services.Interfaces;

namespace backend.Configurations
{
    public static class ApplicationLayerExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services){
            
            services.AddScoped<IWorkSpaceService, WorkSpaceService>();

            return services;
        }
    }
}