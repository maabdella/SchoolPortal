using Microsoft.EntityFrameworkCore;
using SchoolPortal.GradeApp.Data.Audit;
using SchoolPortal.GradeApp.Data.Context;
using SchoolPortal.GradeApp.Services;

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

        var studentsBaseUrl = builder.Configuration["StudentsService:BaseUrl"]
            ?? "http://localhost:5166/";

        builder.Services.AddHttpClient<StudentsServiceClient>(client =>
        {
            client.BaseAddress = new Uri(studentsBaseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<GradeAppDbContext>();
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
            pattern: "{controller=Grade}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
