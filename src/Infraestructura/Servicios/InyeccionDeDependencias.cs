using Aplicacion.Datos;
using Dominio.Primitivos;
using Infraestructura.Persistencia.Repositorios;
using Infraestructura.Persistencia;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dominio.Usuarios;
using Dominio.Contactos;
using Dominio.FormasDePagos;
using Dominio.Servicios;
using Dominio.Imagenes;
using Dominio.Resenas;
using Dominio.Reservas;
using Dominio.Habitaciones;
using Aplicacion.Almacenamiento;
using Infraestructura.Persistencia.Almacenamiento;

namespace Infraestructura.Servicios
{
    public static class InyeccionDeDependencias
    {
        public static IServiceCollection AddInfraestructura(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AgregarPersistencias(configuracion);
            return servicios;
        }

        public static IServiceCollection AgregarPersistencias(this IServiceCollection servicios, IConfiguration configuracion)
        {
            servicios.AddDbContext<AplicacionContextoDb>(options =>
                options.UseSqlServer(configuracion.GetConnectionString("Database"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                }));

            servicios.AddScoped<IAplicacionContextoDb>(sp =>
                sp.GetRequiredService<AplicacionContextoDb>());

            servicios.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<AplicacionContextoDb>());

            servicios.AddScoped<IRepositorioContacto, RepositorioContacto>();
            servicios.AddScoped<IRepositorioFormaDePago, RepositorioFormaDePago>();
            servicios.AddScoped<IRepositorioServicio, RepositorioServicio>();
            servicios.AddScoped<IRepositorioImagen, RepositorioImagen>();
            servicios.AddScoped<IRepositorioResena, RepositorioResena>();
            servicios.AddScoped<IRepositorioReserva, RepositorioReserva>();
            servicios.AddScoped<IRepositorioHabitacion, RepositorioHabitacion>();
            servicios.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

            // Calcular la ruta completa de wwwroot
            var rutaWwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documentos");

            // Registrar el servicio con la ruta
            servicios.AddSingleton<IServicioBlob>(new ServicioBlobLocal(rutaWwwroot));

            return servicios;
        }

    }
}
