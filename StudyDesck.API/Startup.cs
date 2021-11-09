using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudyDesck.API.Domain.Persistence.Contexts;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Persistence.Repositories;
using StudyDesck.API.Services;
using StudyDesck.API.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyDesck.API.Exceptions;


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
            services.AddCors();
            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => 
                {
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // repositories:
            services.AddScoped<IUniversityRepository, universityRepository>();
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
            services.AddScoped<ITutorReservationRepository, TutorReservationRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISessionMaterialRepository, SessionMaterialRepository>();

            // services:
            services.AddScoped<IUniversityService, universityService>();
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
            services.AddScoped<ITutorReservationService, TutorReservationService>();
            services.AddScoped<ISessionMaterialService, SessisonMaterialService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IUserService, UserService>(); // 

            // end region
            services.AddRouting(options => options.LowercaseUrls = true); 
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyDesk.API", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nEnter your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyDesk.API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            //app.UseHttpsRedirection(); // temp
            app.UseRouting();

            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
