
using Aplicacion.Actores.Comun;

namespace Aplicacion.Actores.ListarTodos
{
    public record ListarTodosLosActoresQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaActor>>>;
}
