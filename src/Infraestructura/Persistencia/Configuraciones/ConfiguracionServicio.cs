using Dominio.Servicios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructura.Persistencia.Configuraciones
{
    public class ConfiguracionServicio : IEntityTypeConfiguration<Servicio>
    {
        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdServicio(valor));

            builder.Property(t => t.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();

        }
    }
}
