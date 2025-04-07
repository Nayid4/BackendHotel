using Aplicacion.Reservas.Comun;

namespace Aplicacion.Reservas.Crear
{
    public record CrearReservaCommand(
        ComandoUsuario Usuario,
        ComandoHabitacion Habitacion,
        DateTime FechaIngreso,
        DateTime FechaSalida,
        int CantidadAdultos,
        int CantidadNinos,
        ComandoContacto Contacto,
        ComandoFormaDePago FormaDePago
    ) : IRequest<ErrorOr<Unit>>;
}
