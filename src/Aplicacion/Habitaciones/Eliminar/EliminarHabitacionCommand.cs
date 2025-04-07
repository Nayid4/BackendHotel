

namespace Aplicacion.Habitaciones.Eliminar
{
    public record EliminarHabitacionCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
