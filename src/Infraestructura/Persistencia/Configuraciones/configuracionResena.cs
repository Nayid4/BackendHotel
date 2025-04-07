
using Dominio.Habitaciones;
using Dominio.Resenas;
using Dominio.Usuarios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class configuracionResena : IEntityTypeConfiguration<Resena>
    {
        public void Configure(EntityTypeBuilder<Resena> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdResena(valor));

            builder.Property(t => t.IdHabitacion).HasConversion(
                act => act.Valor,
                valor => new IdHabitacion(valor))
                .IsRequired();

            builder.Property(t => t.IdUsuario).HasConversion(
                pel => pel.Valor,
                valor => new IdUsuario(valor))
                .IsRequired();

            builder.Property(t => t.Titulo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Calificacion)
                .IsRequired();

            builder.Property(t => t.Descripcion)
                .IsRequired();


            builder.HasOne(t => t.Usuario)
                .WithMany(r => r.Resenas)
                .HasForeignKey(t => t.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Habitacion)
                .WithMany()
                .HasForeignKey(t => t.IdHabitacion)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Imagenes)
                .WithOne()
                .HasForeignKey(t => t.IdResena)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(t => t.FechaDeCreacion);

            builder.Property(t => t.FechaDeActualizacion);
        }
    }
}
