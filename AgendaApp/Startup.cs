using AgendaApp.Data;
using AgendaApp.Data.Entities;
using AgendaApp.Services;
using AgendaApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AgendaApp
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Enable MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // DB
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AgendaDbContext>(options => options.UseSqlServer(connection));

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AgendaDbContext>()
                .AddDefaultTokenProviders();

            // Identity Options
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            // Change default login path
            services.ConfigureApplicationCookie(options => options.LoginPath = "/Home/Index");

            // AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                // ApplicationUser
                cfg.CreateMap<ApplicationUser, EditProfileVM>();
                // Agenda
                cfg.CreateMap<Agenda, AgendaVM>()
                    .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(source => source.Items.Count));
                cfg.CreateMap<Agenda, ArchivesVM>()
                    .ForMember(dest => dest.CompletedItemsCount, opt => opt.MapFrom(source => source.Items.Where(o => o.Completed).Count()))
                    .ForMember(dest => dest.TotalItemsCount, opt => opt.MapFrom(source => source.Items.Count()))
                    .ForMember(dest => dest.Ratio, opt => opt.MapFrom(source =>
                        ((double)source.Items.Where(o => o.Completed).Count() / (double)source.Items.Count()) * 100));
                cfg.CreateMap<CreateAgendaVM, Agenda>();
                // Category
                cfg.CreateMap<CreateCategoryVM, Category>();
                // Item
                cfg.CreateMap<Item, ItemVM>();
                cfg.CreateMap<CreateItemVM, Item>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // DI for services
            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IItemService, ItemService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/Home/Error");
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }

    }
}