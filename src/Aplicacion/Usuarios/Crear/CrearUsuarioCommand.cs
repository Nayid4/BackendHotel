
namespace Aplicacion.Usuarios.Crear
{
    public record CrearUsuarioCommand(
        string Nombre, 
        string Apellido,
        string Rol,
        string NombreDeUsuario,
        string Correo,
        string Contrasena
    ) : IRequest<ErrorOr<Unit>>;
}
