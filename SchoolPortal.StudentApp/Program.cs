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

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<StudentDbContext>();
            for (var i = 0; i < 30; i++)
            {
                try
                {
                    db.Database.Migrate();
                    break;
                }
                catch when (i < 29)
                {
                    Thread.Sleep(3000);
                }
            }
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllers();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Student}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
