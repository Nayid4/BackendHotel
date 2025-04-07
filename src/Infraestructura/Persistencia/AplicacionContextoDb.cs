
using Aplicacion.Datos;
using Dominio.Contactos;
using Dominio.FormasDePagos;
using Dominio.Habitaciones;
using Dominio.Imagenes;
using Dominio.ImagenesDeHabitaciones;
using Dominio.ImagenesDeResenas;
using Dominio.Primitivos;
using Dominio.Resenas;
using Dominio.Reservas;
using Dominio.Servicios;
using Dominio.ServiciosDeHabitacion;
using Dominio.Usuarios;

namespace Infraestructura.Persistencia
{
    public class AplicacionContextoDb : DbContext, IAplicacionContextoDb, IUnitOfWork
    {

        private readonly IPublisher _publisher;

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Contacto> Contacto { get; set; }
        public DbSet<FormaDePago> FormaDePago { get; set; }
        public DbSet<Imagen> Imagen { get; set; }
        public DbSet<ImagenDeHabitacion> ImagenDeHabitacion { get; set; }
        public DbSet<ImagenDeResena> ImagenDeResena { get; set; }
        public DbSet<Resena> Resena { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<ServicioDeHabitacion> ServicioDeHabitacion { get; set; }
        public DbSet<Habitacion> Habitacion { get; set; }
        public DbSet<Reserva> Reserva { get; set; }

        public AplicacionContextoDb(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicacionContextoDb).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var eventosDeDominio = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var resultado = await base.SaveChangesAsync(cancellationToken);

            foreach (var evento in eventosDeDominio)
            {
                await _publisher.Publish(evento, cancellationToken);
            }

            return resultado;
        }
    }
}
