using Aplicacion.Resenas.Comun;

namespace Aplicacion.Resenas.Actualizar
{
    public record ActualizarResenaCommand(
        Guid Id,
        ComandoHabitacion Habitacion,
        ComandoUsuario Usuario,
        string Titulo,
        int Calificacion,
        string Descripcion,
        List<ComandoImagen> Imagenes
    ) : IRequest<ErrorOr<Unit>>;
}
