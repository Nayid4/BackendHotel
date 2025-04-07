
using Dominio.Imagenes;
using Dominio.ImagenesDeResenas;
using Dominio.Resenas;
using Dominio.ServiciosDeHabitacion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionImagenDeResena : IEntityTypeConfiguration<ImagenDeResena>
    {
        public void Configure(EntityTypeBuilder<ImagenDeResena> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdImagenDeResena(valor));

            builder.Property(t => t.IdResena).HasConversion(
                act => act.Valor,
                valor => new IdResena(valor))
                .IsRequired();

            builder.Property(t => t.IdImagen).HasConversion(
                pel => pel.Valor,
                valor => new IdImagen(valor))
                .IsRequired();

            builder.HasOne(t => t.Imagen)
                .WithMany()
                .HasForeignKey(t => t.IdImagen)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Resena)
                .WithMany()
                .HasForeignKey(t => t.IdResena)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(t => t.FechaDeCreacion);

            builder.Property(t => t.FechaDeActualizacion);
        }
    }
}
