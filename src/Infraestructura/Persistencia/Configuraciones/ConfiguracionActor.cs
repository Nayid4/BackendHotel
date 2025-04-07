using Dominio.Actores;
using Dominio.Generos;
using Dominio.Paises;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionActor : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdActor(valor));

            builder.Property(t => t.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Apellido)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.IdPais).HasConversion(
                pais => pais.Valor,
                valor => new IdPais(valor))
                .IsRequired();

            builder.HasOne(a => a.Pais)
                .WithMany()
                .HasForeignKey(a => a.IdPais)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
