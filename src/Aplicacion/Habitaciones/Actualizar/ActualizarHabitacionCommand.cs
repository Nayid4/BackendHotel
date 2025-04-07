using Aplicacion.Habitaciones.Comun;

namespace Aplicacion.Habitaciones.Actualizar
{
    public record ActualizarHabitacionCommand(
        Guid Id,
        string NumeroDeHabitacion,
        string Nombre,
        string Descripcion,
        double PrecioPorNoche,
        int Capacidad,
        List<ComandoServicio> Servicios,
        List<ComandoImagen> Imagenes,
        string Estado
    ) : IRequest<ErrorOr<Unit>>;
}
