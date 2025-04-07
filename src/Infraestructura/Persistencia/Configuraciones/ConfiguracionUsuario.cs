using Dominio.Usuarios;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Persistencia.Configuraciones
{
    public class ConfiguracionUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasConversion(
                gene => gene.Valor,
                valor => new IdUsuario(valor));

            builder.Property(t => t.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Apellido)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Rol)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.NombreDeUsuario)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Correo)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Contrasena)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasMany(u => u.Reservas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Resenas)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(t => t.FechaDeCreacion)
                .IsRequired();

            builder.Property(t => t.FechaDeActualizacion)
                .IsRequired();
        }
    }
}
