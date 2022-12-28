using Aumy.Devices.NestThermostat;
using Aumy.Devices.NestThermostat.Models;
using Aumy.Devices.Tapo;
using Aumy.Devices.Tapo.Models;
using Aumy.Devices.Tuya;
using Aumy.Devices.Tuya.Devices;
using Aumy.Devices.Tv;
using Aumy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Aumy;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	private IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddOptions<GoogleNestConfiguration>().Bind(Configuration.GetSection("GoogleNestConfiguration"));
		services.AddOptions<TapoConfiguration>().Bind(Configuration.GetSection("TapoConfiguration"));
		services.AddOptions<TuyaConfiguration>().Bind(Configuration.GetSection("TuyaConfiguration"));

		services.AddSingleton<NestThermostat>();
		services.AddSingleton<HeatMode>();
		services.AddSingleton<EcoMode>();
		services.AddScoped<TapoService>();
		services.AddScoped<TvService>();

		services.AddSingleton<TuyaService>();

		services.AddScoped<MoesDimmerSwitch>();
		services.AddScoped<DeviceFactory>();
		
		services.AddHostedService<ScanTuyaDevices>();

		services.AddControllers();
		services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aumy", Version = "v1" }); });
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aumy"));
		}

		app.UseHttpsRedirection();

		app.UseRouting();

		app.UseAuthorization();

		app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
	}
}