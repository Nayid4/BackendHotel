
using Aplicacion.Directores.Comun;

namespace Aplicacion.Directores.ListarTodos
{
    public record ListarTodosLosDirectoresQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaDirector>>>;
}
