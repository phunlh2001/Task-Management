using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.Context;
using backend.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace backend.Configurations
{
    public static class AuthenticationPoliciesExtension
    {
        public static IServiceCollection AddAuthenticationPolicies(this IServiceCollection services){
            services.AddIdentity<AppUser, IdentityRole>(options=>{
                
                options.Password.RequireDigit = false; 
                options.Password.RequireLowercase = false; 
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireUppercase = false; 
                options.Password.RequiredLength = 3; 
                options.Password.RequiredUniqueChars = 1;

                

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 10;

                
                options.User.AllowedUserNameCharacters = 
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true; 

                
                options.SignIn.RequireConfirmedEmail = false;           
                options.SignIn.RequireConfirmedPhoneNumber = false;     
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<TaskManagerContext>()
            .AddDefaultTokenProviders();

            return services;


        }
    }
}