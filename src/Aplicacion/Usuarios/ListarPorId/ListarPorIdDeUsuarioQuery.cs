
using Aplicacion.Usuarios.Comun;

namespace Aplicacion.Usuarios.ListarPorId
{
    public record ListarPorIdDeUsuarioQuery(Guid Id) : IRequest<ErrorOr<RespuestaUsuario>>;
}
