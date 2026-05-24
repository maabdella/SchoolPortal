using Microsoft.EntityFrameworkCore;
using SchoolPortal.StudentApp.Data.Audit;
using SchoolPortal.StudentApp.Data.Context;

namespace SchoolPortal.StudentApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<StudentDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("StudentConnection"));
            options.AddInterceptors(
              sp.GetRequiredService<AuditInterceptor>());
        });

        builder.Services.AddScoped<AuditInterceptor>();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
