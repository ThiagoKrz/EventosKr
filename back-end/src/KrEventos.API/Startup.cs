using KrEventos.Application;
using KrEventos.Application.Contratos;
using KrEventos.Persistence.bin;
using KrEventos.Persistence.Contextos;
using KrEventos.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace KrEventos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Esse método chama o runtime. Use para adicionar serviços ao container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<KrEventosContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );
            services.AddControllers()
                .AddNewtonsoftJson( x => x.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddScoped<IEventoService, EventoService>();
            services.AddScoped<IGeralPersist, GeralPersist>();
            services.AddScoped<IEventoPersist, EventoPersist>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowAccess_To_API", options => options.WithOrigins("*"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KrEventos.API", Version = "v1" });
            });
        }

        // Esse método chama o runtime. Use para configurar a pipeline de requests HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KrEventos.API v1")
                );
            }
            app.UseRouting();
            app.UseCors("AllowAccess_To_API");
            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}