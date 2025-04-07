using Aplicacion.Habitaciones.Comun;

namespace Aplicacion.Habitaciones.Crear
{
    public record CrearHabitacionCommand(
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
