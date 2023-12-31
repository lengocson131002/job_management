using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repository;
using Repository.Repositories;
using Repository.Repositories.interfaces;
using JobApplicationManagement.Services;

namespace JobApplicationManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "JobApplication";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<JobManagementDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Define DI for repository
            builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
            builder.Services.AddScoped(typeof(IStorageService), typeof(StorageService));
            builder.Services.AddScoped(typeof(ICompanyRepository), typeof(CompanyRepository));
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddTransient(typeof(JobDescriptionRepository), typeof(JobDescriptionRepository));
            builder.Services.AddTransient(typeof(ResumeRepository), typeof(ResumeRepository));
            builder.Services.AddTransient(typeof(ContractRepository), typeof(ContractRepository));

            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "areaRoute",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}