

using Dominio.FormasDePagos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionFormaDePago : IEntityTypeConfiguration<FormaDePago>
    {
        public void Configure(EntityTypeBuilder<FormaDePago> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdFormaDePago(valor));

            builder.Property(t => t.Titular)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.NumeroTarjeta)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.FechaDeVencimiento)
                .IsRequired();

            builder.Property(t => t.Cvv)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
