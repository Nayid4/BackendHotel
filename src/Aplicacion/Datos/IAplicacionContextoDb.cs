
using Dominio.Contactos;
using Dominio.FormasDePagos;
using Dominio.Habitaciones;
using Dominio.Imagenes;
using Dominio.ImagenesDeHabitaciones;
using Dominio.ImagenesDeResenas;
using Dominio.Resenas;
using Dominio.Reservas;
using Dominio.Servicios;
using Dominio.ServiciosDeHabitacion;
using Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Datos
{
    public interface IAplicacionContextoDb
    {
        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<FormaDePago> FormaDePago { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<ImagenDeHabitacion> ImagenDeHabitacion {  get; set; }
        public DbSet<ImagenDeResena> ImagenDeResena { get; set; }
        public DbSet<Resena> Resena { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<ServicioDeHabitacion> ServicioDeHabitacion { get; set; }
        public DbSet<Habitacion> Habitacion { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
