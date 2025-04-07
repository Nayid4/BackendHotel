using Aplicacion.Usuarios.Comun;

namespace Aplicacion.Usuarios.RefrescarToken
{
    public record RefrescarTokenQuery() : IRequest<ErrorOr<RespuestaIniciarSesion>>;
}
