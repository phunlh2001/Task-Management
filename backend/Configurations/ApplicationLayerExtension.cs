using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using backend.Models.Dtos;
using backend.Security.JWT;
using backend.Services;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace backend.Configurations
{
    public static class ApplicationLayerExtension
    {
        public static IServiceCollection AddControllerExtension(this IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
            })
            .ConfigureApiBehaviorOptions(o =>
            {
                o.InvalidModelStateResponseFactory = context =>
                {
                    var msg = context.ModelState
                    .Where(item => item.Value.Errors.Any())
                    .Select(item => 
                    {
                        var key = item.Key;
                        var errors = item.Value.Errors;
                        var emsg = errors.Select(e=>
                            e.ErrorMessage
                        );
                        
                        return key+ ": "+ string.Join("; ", emsg) ;
                    });
                    var response = new Response<List<string>>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Message = "Invalid request",
                        Data = msg.Any()?msg.ToList():  null
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }

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