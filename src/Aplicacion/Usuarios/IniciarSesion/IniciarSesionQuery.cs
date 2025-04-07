using Aplicacion.Usuarios.Comun;

namespace Aplicacion.Usuarios.IniciarSesion
{
    public record IniciarSesionQuery(string NombreDeUsuario, string Contrasena) : IRequest<ErrorOr<RespuestaIniciarSesion>>;
}
