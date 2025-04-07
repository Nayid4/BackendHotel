using Dominio.Actores;
using Dominio.ActoresDePeliculas;
using Dominio.Generos;
using Dominio.GenerosDePeliculas;
using Dominio.Peliculas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionGeneroDePelicula : IEntityTypeConfiguration<GeneroDePelicula>
    {
        public void Configure(EntityTypeBuilder<GeneroDePelicula> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdGeneroDePelicula(valor));

            builder.Property(t => t.IdGenero).HasConversion(
                act => act.Valor,
                valor => new IdGenero(valor))
                .IsRequired();

            builder.Property(t => t.IdPelicula).HasConversion(
                pel => pel.Valor,
                valor => new IdPelicula(valor))
                .IsRequired();

            builder.HasOne(t => t.Genero)
                .WithMany()
                .HasForeignKey(t => t.IdGenero)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Pelicula>()
                .WithMany()
                .HasForeignKey(t => t.IdPelicula)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(t => t.FechaDeCreacion);

            builder.Property(t => t.FechaDeActualizacion);
        }
    }
}
