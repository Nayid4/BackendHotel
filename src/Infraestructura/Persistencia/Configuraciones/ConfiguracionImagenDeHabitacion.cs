using Dominio.Habitaciones;
using Dominio.Imagenes;
using Dominio.ImagenesDeHabitaciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionImagenDeHabitacion : IEntityTypeConfiguration<ImagenDeHabitacion>
    {
        public void Configure(EntityTypeBuilder<ImagenDeHabitacion> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdImagenDeHabitacion(valor));

            builder.Property(t => t.IdHabitacion).HasConversion(
                act => act.Valor,
                valor => new IdHabitacion(valor))
                .IsRequired();

            builder.Property(t => t.IdImagen).HasConversion(
                pel => pel.Valor,
                valor => new IdImagen(valor))
                .IsRequired();

            builder.HasOne(t => t.Imagen)
                .WithMany()
                .HasForeignKey(t => t.IdImagen)
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
