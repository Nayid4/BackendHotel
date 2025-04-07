using Dominio.ActoresDePeliculas;
using Dominio.Directores;
using Dominio.Generos;
using Dominio.GenerosDePeliculas;
using Dominio.Paises;
using Dominio.Peliculas;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionPelicula : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdPelicula(valor));

            builder.Property(t => t.IdPais).HasConversion(
                gene => gene.Valor,
                valor => new IdPais(valor));

            builder.Property(t => t.IdDirector).HasConversion(
                gene => gene.Valor,
                valor => new IdDirector(valor));

            builder.Property(t => t.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Resena)
                .IsRequired();

            builder.Property(t => t.ImagenDePortada)
                .IsRequired();

            builder.Property(t => t.CodigoDeTrailerEnYoutube)
                .IsRequired();

            builder.HasOne(t => t.Pais)
                .WithMany()
                .HasForeignKey(t => t.IdPais)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(t => t.Director)
                .WithMany()
                .HasForeignKey(t => t.IdDirector)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.Generos)
                .WithOne()
                .HasForeignKey(t => t.IdPelicula)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Actores)
                .WithOne()
                .HasForeignKey(t => t.IdPelicula)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
