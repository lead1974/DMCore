using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DMCore.Data.Core.Repositories;
using DMCore.Services;
using DMCore.Data.Core.Domain;
using Microsoft.AspNetCore.Identity;
using DMCore.Data.Persistance;
using DMCore.Data.Persistance.Repositories;
using DMCore.Data.Core;
using DMCore.Data;
using System;

namespace DMCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;

            var builder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                              //.AddJsonFile("config.json");

            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<DMDbContext>(options =>
                   options.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = DMDB; Trusted_Connection = True; MultipleActiveResultSets = true"));
            //options.UseSqlServer(Configuration.GetSection("ConnectionString")["DefaultConnection"]));

            services.AddIdentity<AuthUser, AuthRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
            })
              .AddEntityFrameworkStores<DMDbContext>()
              .AddDefaultTokenProviders();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddKendo();
            //services.AddAntiforgery();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // Add application services.
            services.AddSingleton(_config);
            services.AddTransient<GlobalService, GlobalService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<DMSeedData>();

            //services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DMSeedData seedData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseCors("Cors");
            app.UseMvc();

            app.UseFileServer();

            seedData.EnsureSeedData().Wait();
        }
    }
}
