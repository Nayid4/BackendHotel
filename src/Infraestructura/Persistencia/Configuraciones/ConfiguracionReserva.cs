
using Dominio.Contactos;
using Dominio.FormasDePagos;
using Dominio.Habitaciones;
using Dominio.Reservas;
using Dominio.Usuarios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionReserva : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdReserva(valor));

            builder.Property(t => t.IdHabitacion).HasConversion(
                act => act.Valor,
                valor => new IdHabitacion(valor))
                .IsRequired();

            builder.Property(t => t.IdUsuario).HasConversion(
                pel => pel.Valor,
                valor => new IdUsuario(valor))
                .IsRequired();

            builder.Property(t => t.FechaIngreso)
                .IsRequired();

            builder.Property(t => t.FechaSalida)
                .IsRequired();

            builder.Property(t => t.CantidadAdultos)
                .IsRequired();

            builder.Property(t => t.CantidadNinos)
                .IsRequired();

            builder.Property(t => t.IdContacto).HasConversion(
                act => act.Valor,
                valor => new IdContacto(valor))
                .IsRequired();

            builder.Property(t => t.IdFormaDePago).HasConversion(
                pel => pel.Valor,
                valor => new IdFormaDePago(valor))
                .IsRequired();


            builder.HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Habitacion)
                .WithMany()
                .HasForeignKey(t => t.IdHabitacion)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Contacto)
                .WithMany()
                .HasForeignKey(t => t.IdContacto)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.FormaDePago)
                .WithMany()
                .HasForeignKey(t => t.IdFormaDePago)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
