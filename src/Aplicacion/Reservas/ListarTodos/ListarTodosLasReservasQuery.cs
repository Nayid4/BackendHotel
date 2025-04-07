
using Aplicacion.Reservas.Comun;

namespace Aplicacion.Reservas.ListarTodos
{
    public record ListarTodosLasReservasQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaReserva>>>;
}
