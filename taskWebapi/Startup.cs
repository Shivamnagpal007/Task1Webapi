using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using taskWebapi.Data;
using taskWebapi.DTOMapping;
using taskWebapi.Models;
using taskWebapi.Repository;
using taskWebapi.Repository.IRepository;

namespace taskWebapi
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
            services.AddScoped<IDsgRepository,DsgRepository>();
            services.AddScoped<IDepRepository, DepRepository>();
            services.AddScoped<IEmployeRepository, EmployeeRepository>();
            services.AddScoped<IempDepRepository, EmpdepRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "taskWebapi", Version = "v1" });
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            var appSetting = appSettingSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSetting.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddCookie()
          .AddJwtBearer(x =>
          {
              x.RequireHttpsMetadata = false;
              x.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(key),
                  ValidateIssuer = false,
                  ValidateAudience = false
              };
          });
            services.AddDbContext<ApplicationDbcontext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
         
            //builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "taskWebapi v1"));
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();        
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
