
using Aplicacion.Resenas.Comun;

namespace Aplicacion.Resenas.ListarTodos
{
    public record ListarTodosLosResenaQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaResena>>>;
}
