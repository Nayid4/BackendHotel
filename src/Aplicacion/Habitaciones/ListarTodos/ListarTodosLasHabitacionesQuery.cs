using Aplicacion.Habitaciones.Comun;

namespace Aplicacion.Habitaciones.ListarTodos
{
    public record ListarTodosLasHabitacionesQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaHabitacion>>>;
}
