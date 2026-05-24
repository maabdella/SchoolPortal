using Microsoft.EntityFrameworkCore;
using SchoolPortal.GradeApp.Data.Audit;
using SchoolPortal.GradeApp.Data.Context;

namespace SchoolPortal.GradeApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<GradeAppDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("GradeConnection"));
            options.AddInterceptors(
              sp.GetRequiredService<AuditInterceptor>());
        });

        builder.Services.AddScoped<AuditInterceptor>();

        builder.Services.AddHttpClient

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
            pattern: "{controller=Grade}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
