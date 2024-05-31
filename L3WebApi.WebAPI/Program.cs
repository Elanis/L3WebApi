using L3WebApi.Business.Implementations;
using L3WebApi.Business.Interfaces;
using L3WebApi.Common;
using L3WebApi.DataAccess;
using L3WebApi.DataAccess.Implementations;
using L3WebApi.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

namespace L3WebApi.WebAPI;

public class Program {
	public static void Main(string[] args) {

		var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
		logger.Info("Starting Application ...");

		try {
			var builder = WebApplication.CreateBuilder(args);

			var rawConfig = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddEnvironmentVariables()
				.AddJsonFile("appsettings.json")
				.AddUserSecrets<Program>()
				.Build();

			var appSettingsSection = rawConfig.GetSection("AppSettings");
			builder.Services.Configure<AppSettings>(appSettingsSection);

			// Context
			builder.Services.AddTransient<GameContext>();

			// Add services to the container.
			builder.Services.AddTransient<IGamesDataAccess, GamesDataAccess>();
			builder.Services.AddTransient<IGameService, GameService>();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			// NLog: Setup NLog for Dependency injection
			builder.Logging.ClearProviders();
			builder.Host.UseNLog();

			var app = builder.Build();

			using (var scope = app.Services.CreateScope()) {
				var dbContext = scope.ServiceProvider.GetRequiredService<GameContext>();

				// Here is the migration executed
				dbContext.Database.Migrate();
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		} catch (Exception e) {
			logger.Error(e);
			throw;
		} finally {
			// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
			NLog.LogManager.Shutdown();
		}
	}
}
