using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Whoops.DataLayer;
using Whoops.Services;
using Whoops.ViewModels;
using Whoops.ViewModels.Index;

namespace Whoops
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json");
            _config = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(
                config =>
                    {
                        config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    }
                );

            if (_environment.IsDevelopment())
                services.AddScoped<IContactSender, MockContactSender>();


            services.AddIdentity<User, IdentityRole>(config =>
             {
                 config.User.RequireUniqueEmail = true;
                 config.Password.RequireLowercase = false;
                 config.Password.RequireNonAlphanumeric = false;
                 config.Password.RequireDigit = false;
                 config.Password.RequiredLength = 4;
             }
            );
            services.AddTransient<GeoCoordServices>();
            
           
            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddSingleton<IConfigurationRoot>(_config);
            services.AddDbContext<WorldContext>();
            services.AddTransient<WorldContextSeedData>();
            services.AddLogging();
            services.ConfigureApplicationCookie(opt => opt.LoginPath = "/auth/login");
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,WorldContextSeedData seeder,ILoggerFactory factory)
        {
            Mapper.Initialize(config =>
                {
                    config.CreateMap<TripViewModel, Trip>().ForMember(dest => dest.DateCreated,
                        opt => opt.MapFrom(src => src.Created)).ReverseMap();
                    config.CreateMap<StopViewModel, Stop>().ReverseMap();

                    config.CreateMap<CreateUserViewModel, User>()
                          .ForMember(d => d.IsNotificationsAllowed, opt => opt.MapFrom(src => src.AllowNotifications))
                          .ReverseMap();

                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                factory.AddDebug(LogLevel.Information);
                
            }
            else
            {
                factory.AddDebug(LogLevel.Error);
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(config=>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                );
                
            });
            seeder.SeedUsers().Wait();
            seeder.EnsureSeedData().Wait();
        }
    }
}
