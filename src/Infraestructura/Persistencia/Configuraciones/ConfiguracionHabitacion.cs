using Dominio.Habitaciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionHabitacion : IEntityTypeConfiguration<Habitacion>
    {
        public void Configure(EntityTypeBuilder<Habitacion> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdHabitacion(valor));

            builder.Property(t => t.NumeroDeHabitacion)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Descripcion)
                .IsRequired();

            builder.Property(t => t.PrecioPorNoche)
                .IsRequired();

            builder.Property(t => t.Capacidad)
                .IsRequired();

            builder.Property(t => t.Estado)
                .IsRequired();

            builder.HasMany(t => t.ImagenesDeHabitacion)
                .WithOne()
                .HasForeignKey(t => t.IdHabitacion)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.ServiciosDeHabitacion)
                .WithOne()
                .HasForeignKey(t => t.IdHabitacion)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Resenas)
                .WithOne()
                .HasForeignKey(t => t.IdHabitacion)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(t => t.FechaDeCreacion);

            builder.Property(t => t.FechaDeActualizacion);
        }
    }
}
