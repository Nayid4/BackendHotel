

namespace Aplicacion.Usuarios.Comun
{
    public record RespuestaIniciarSesion(
        string Token,
        string RefreshToken
    );
}
