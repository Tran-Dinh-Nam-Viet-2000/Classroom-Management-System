using CMSFPTU_WebApi.Entities;
using CMSFPTU_WebApi.Services;
using CMSFPTU_WebApi.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace CMSFPTU_WebApi
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
            //Set connect Db
            services.AddDbContext<CMSFPTUContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add Cors 
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
                });
            });

            //Register api service
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IClassSubjectService, ClassSubjectService>();
            services.AddScoped<IRequestTeacherService, RequestTeacherService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IClassroomManagementService, ClassroomManagementService>();

            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMSFPTU_WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CMSFPTU_WebApi v1"));
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}