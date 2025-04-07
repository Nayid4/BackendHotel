using Dominio.Actores;
using Dominio.ActoresDePeliculas;
using Dominio.Directores;
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
    public class ConfiguracionActorDePelicula : IEntityTypeConfiguration<ActorDePelicula>
    {
        public void Configure(EntityTypeBuilder<ActorDePelicula> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdActorDePelicula(valor));

            builder.Property(t => t.IdActor).HasConversion(
                act => act.Valor,
                valor => new IdActor(valor))
                .IsRequired();

            builder.Property(t => t.IdPelicula).HasConversion(
                pel => pel.Valor,
                valor => new IdPelicula(valor))
                .IsRequired();

            builder.HasOne(t => t.Actor)
                .WithMany()
                .HasForeignKey(t => t.IdActor)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Pelicula>()
                .WithMany()
                .HasForeignKey(t => t.IdPelicula)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
