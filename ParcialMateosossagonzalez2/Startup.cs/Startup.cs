using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configuración de la cadena de conexión
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        // Configuración de Entity Framework Core
        services.AddDbContext<ConcertDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Configuración de servicios adicionales
        
        services.AddTransient<TicketRepository>();

        services.AddControllers();
        services.AddSwaggerGen();

        // Otros servicios y configuraciones necesarios

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Concert API V1");
        });

        // Otros middlewares y configuraciones necesarios

    }
}