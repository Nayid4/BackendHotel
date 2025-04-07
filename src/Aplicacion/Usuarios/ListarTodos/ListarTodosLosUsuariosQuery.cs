
using Aplicacion.Usuarios.Comun;

namespace Aplicacion.Usuarios.ListarTodos
{
    public record ListarTodosLosUsuariosQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaUsuario>>>;
}
