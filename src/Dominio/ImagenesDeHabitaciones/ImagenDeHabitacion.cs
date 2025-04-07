using Dominio.Genericos;
using Dominio.Habitaciones;
using Dominio.Imagenes;

namespace Dominio.ImagenesDeHabitaciones
{
    public sealed class ImagenDeHabitacion : EntidadGenerica<IdImagenDeHabitacion>
    {
        public IdHabitacion IdHabitacion { get; private set; } = default!;
        public IdImagen IdImagen { get; private set; } = default!;

        public Imagen? Imagen { get; private set; } = default!;
        public Habitacion? Habitacion { get; private set; } = default!;

        public ImagenDeHabitacion()
        {
        }

        public ImagenDeHabitacion(IdImagenDeHabitacion id, IdHabitacion idHabitacion, IdImagen idImagen) : base(id)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdImagen = idImagen ?? throw new ArgumentNullException(nameof(idImagen));
        }

        public void Actualizar(IdHabitacion idHabitacion, IdImagen idImagen)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdImagen = idImagen ?? throw new ArgumentNullException(nameof(idImagen));
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
