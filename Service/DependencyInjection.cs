using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
