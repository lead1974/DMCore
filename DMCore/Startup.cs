using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DMCore.Services;
using DMCore.Data.Core.Domain;
using Microsoft.AspNetCore.Identity;
using DMCore.Data.Persistance;
using DMCore.Data.Core;
using DMCore.Data;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;

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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Home/ErrorForbidden";
                    options.LoginPath = "/Home/ErrorNotLoggedIn";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(SD.PolicyCanManageSite, p => p.RequireAuthenticatedUser().RequireRole(SD.CanManageSite));
            });

            services.AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.Filters.Add(new RequireHttpsAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddKendo();
            //services.AddAntiforgery();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            //email settings
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            // Add application services.
            services.AddSingleton(_config);
            services.AddTransient<GlobalService, GlobalService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<DMSeedData>();

            //services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = "325549147934171";
            //    facebookOptions.AppSecret = "138842076f04812382f9f7c57505720e";
            //});

            //services.AddAuthentication().AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = "609302538982-l559fa4rrjtkhlr642sn8k5gdhomu22q.apps.googleusercontent.com";
            //    googleOptions.ClientSecret = "Q5twNVtCLCvYmNiygwG7LDsJ";
            //});

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();

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

            //security: http to https forwarding
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            //security: https forwarding
            app.UseHsts(options => options.MaxAge(days: 365).IncludeSubdomains());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXContentTypeOptions(); //prevent attack coming back with different data than it was submitted for


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
