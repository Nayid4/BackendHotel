using Dominio.Generos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructura.Persistencia.Configuraciones
{
    public class ConfiguracionGenero : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdGenero(valor));

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
