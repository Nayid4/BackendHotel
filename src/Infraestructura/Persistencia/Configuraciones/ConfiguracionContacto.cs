
using Dominio.Contactos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionPais : IEntityTypeConfiguration<Contacto>
    {
        public void Configure(EntityTypeBuilder<Contacto> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdContacto(valor));

            builder.Property(t => t.Nombre)
                .HasMaxLength(50)
                .IsRequired();

			builder.Property(t => t.Apellido)
                .HasMaxLength(50)
				.IsRequired();

			builder.Property(t => t.Correo)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(t => t.Telefono)
				.HasMaxLength(20)
				.IsRequired();


			builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
