using Aplicacion.Resenas.Comun;

namespace Aplicacion.Resenas.Crear
{
    public record CrearResenaCommand(
        ComandoHabitacion Habitacion,
        ComandoUsuario Usuario,
        string Titulo,
        int Calificacion,
        string Descripcion,
        List<ComandoImagen> Imagenes
    ) : IRequest<ErrorOr<Unit>>;
}
