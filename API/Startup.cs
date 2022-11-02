
using Infra.Persistencia;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Util.IoC;

namespace API.Exemplo
{
    public class Startup
    {
        public IWebHostEnvironment _environment { get; }
        public IConfiguration _configuration { get; } 

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment; 
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DependencyResolver.Resolve(services);
            var sqlConnection = _configuration.GetConnectionString("SqlConnection:" + _environment.EnvironmentName.ToString());

            services.AddDbContext<Contexto>(options => options.UseSqlServer(sqlConnection));
            services.AddCors(o => o.AddPolicy("PolicyOpenAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
              

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.Usuario", Version = "v1" });
         
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.Usuario v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("PolicyOpenAll");
            app.UseRouting(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
