
using Dominio.Imagenes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionImagen : IEntityTypeConfiguration<Imagen>
    {
        public void Configure(EntityTypeBuilder<Imagen> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdImagen(valor));

            builder.Property(t => t.Url)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
