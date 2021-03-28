using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrbitGroup.Api.Students.DataAccess;
using OrbitGroup.Api.Students.Db;
using OrbitGroup.Api.Students.Interfaces;
using OrbitGroup.Api.Students.Profiles;
using OrbitGroup.Api.Students.Providers;
using OrbitGroup.Api.Students.Validators;

namespace OrbitGroup.Api.Students
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
            services.AddScoped<IStudentsProvider, StudentsProvider>();
            services.AddScoped<IStudentsDataAccess, StudentsDataAccess>();
            services.AddSingleton(new StudentsValidator());

            // Automapper configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new StudentsProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Students db context configuration
            services.AddDbContext<StudentsDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("StudentsConectionString"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
