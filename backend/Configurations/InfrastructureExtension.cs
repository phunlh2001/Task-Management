using backend.Data.Context;
using backend.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace backend.Configurations
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration){
            
            services.AddDbContext<TaskManagerContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkSpaceRepository, WorkSpaceRepository>();
            services.AddScoped<ITaskListRepository, TaskListRepository>();
            services.AddScoped<ITaskDetailRepository, TaskDetailRepository>();
            return services;
        }
    }
}