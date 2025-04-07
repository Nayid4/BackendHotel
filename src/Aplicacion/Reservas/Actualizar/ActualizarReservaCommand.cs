using Aplicacion.Reservas.Comun;

namespace Aplicacion.Reservas.Actualizar
{
    public record ActualizarReservaCommand(
        Guid Id,
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
