using backend.Data.Context;
using backend.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace backend.Configurations
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<TaskManagerContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<TaskManagerContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkSpaceRepository, WorkSpaceRepository>();
            services.AddScoped<ITaskListRepository, TaskListRepository>();
            services.AddScoped<ITaskDetailRepository, TaskDetailRepository>();
            return services;
        }

        public static async Task<WebApplication> EnsureDataInit(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TaskManagerContext>();
                // await db.Database.MigrateAsync();
                await db.Database.EnsureCreatedAsync();
            }
            return app;
        }
    }


}