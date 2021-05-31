using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudyDesck.API.Domain.Persistence.Contexts;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Persistence.Repositories;
using StudyDesck.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // repositories:
            services.AddScoped<IInstituteRepository, InstituteRepository>();
            services.AddScoped<ICareerRepository, CareerRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IPlatformRepository, PlatformRepository>();
            services.AddScoped<ISessionReservationRepository, SessionReservationRepository>();
            services.AddScoped<ITutorRepository, TutorRepository>();
            services.AddScoped<IExpertTopicRepository, ExpertTopicRepository>();
            services.AddScoped<IStudentMaterialRepository, StudentMaterialRepository>();
            services.AddScoped<IStudyMaterialRepository, StudyMaterialRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            // services:
            services.AddScoped<IInstituteService, InstituteService>();
            services.AddScoped<ICareerService, CareerService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IPlatformService, PlatformService>();
            services.AddScoped<ISessionReservationService, SessionReservationService>();
            services.AddScoped<ITutorService, TutorService>();
            services.AddScoped<IExpertTopicService, ExpertTopicService>();
            services.AddScoped<IStudentMaterialService, StudentMaterialService>();
            services.AddScoped<IStudyMaterialService, StudyMaterialService>();
            services.AddScoped<IScheduleService, ScheduleService>();

            // end region
            services.AddRouting(options => options.LowercaseUrls = true); 
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyDesk.API", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyDesk.API v1"));
            }

            //app.UseHttpsRedirection(); // temp
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
