
using Dominio.Habitaciones;
using Dominio.Servicios;
using Dominio.ServiciosDeHabitacion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionServicioDeHabitacion : IEntityTypeConfiguration<ServicioDeHabitacion>
    {
        public void Configure(EntityTypeBuilder<ServicioDeHabitacion> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdServicioDeHabitacion(valor));

            builder.Property(t => t.IdHabitacion).HasConversion(
                act => act.Valor,
                valor => new IdHabitacion(valor))
                .IsRequired();

            builder.Property(t => t.IdServicio).HasConversion(
                pel => pel.Valor,
                valor => new IdServicio(valor))
                .IsRequired();

            builder.HasOne(t => t.Servicio)
                .WithMany()
                .HasForeignKey(t => t.IdServicio)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Habitacion)
                .WithMany()
                .HasForeignKey(t => t.IdHabitacion)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(t => t.FechaDeCreacion);

            builder.Property(t => t.FechaDeActualizacion);
        }
    }
}
