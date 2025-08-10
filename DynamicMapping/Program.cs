
using BLL.Common;
using DynamicMapping.Infrastructure;
using Serilog;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using DynamicMapping.CustomMiddlewares;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicMapping
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /// Add services to the container.
            
            /// Serilog - Logging into files
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
                .WriteTo.File(Configuration.GetValue<string>(WebHostDefaults.ContentRootKey) + "ExceptionLogs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            /// Adding the exception handler to the list of services 
            builder.Services.AddExceptionHandler<ExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddDbContext<DAL.Models.DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));
            builder.Services.AddScoped<DAL.Configuration.IUnitOfWork, DAL.Configuration.UnitOfWork>();
            builder.Services.AddTransient<BLL.Services.Interfaces.IBaseService, BLL.Services.BaseService>();
            builder.Services.AddScoped<BLL.Services.Interfaces.IRoomService, BLL.Services.RoomService>();
            builder.Services.AddScoped<BLL.Services.Interfaces.IReservationService, BLL.Services.ReservationService>();
            builder.Services.AddHttpContextAccessor();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            // add caching midleware
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSingleton<BLL.Caching.ICacheService, BLL.Caching.CacheService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            /// Static class registration to allow using IConfiguration in all the app classes
            AppSettingsHelper.AppSettingsConfigure(Configuration);

            /// Register the ExceptionHandler Middleware
            app.UseExceptionHandler();
            /// if builder.Services.AddProblemDetails is not added then we should add the predicate to the UseExceptionHandler like below
            /// app.UseExceptionHandler(ops => { });
            /// End Register ExceptionHandler Middleware

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            // Git changes fix broken email address 
        }
    }
}
