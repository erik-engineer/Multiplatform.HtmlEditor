
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MultiPlatform.HtmlEditor
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(option => option.EnableEndpointRouting = false);
			//services.AddControllersWithViews();
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			//app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});

			// Open the Electron-Window here
			//Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
			if (HybridSupport.IsElectronActive)
			{
				ElectronBootstrap();
			}
		}


		public async void ElectronBootstrap()
		{
			var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
			{
				Width = 1000,
				Height = 800,
				Show = false,
				//Icon = "Assets/text_editor.png" 
			});
			
			browserWindow.OnReadyToShow += () => browserWindow.Show();
			browserWindow.SetTitle("HTMLEditor 2020");
		}
	}
}
