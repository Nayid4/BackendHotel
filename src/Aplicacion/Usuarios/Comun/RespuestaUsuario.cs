
namespace Aplicacion.Usuarios.Comun
{
    public record RespuestaUsuario(
        Guid Id,
        string Nombre,
        string Apellido,
        string Rol,
        string NombreDeUsuario,
        string Correo
    );

}
