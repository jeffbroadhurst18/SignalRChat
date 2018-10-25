using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuickAPI.Model;

namespace QuickAPI
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddDbContext<ScoreContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("Context")));

			services.AddCors(cfg =>
			{
				cfg.AddPolicy("Wildermuth", bldr =>
				{
					bldr.AllowAnyHeader().
					AllowAnyMethod().
					AllowAnyOrigin();
					//	WithOrigins("http://wildermuth.com");
				}); //this defines the policy but doesn't implement it.
				cfg.AddPolicy("AnyGET", bldr =>
				{
					bldr.AllowAnyHeader().
					WithMethods("GET").
					AllowAnyOrigin();
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
