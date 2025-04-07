
namespace Aplicacion.Usuarios.Actualizar
{
    public record ActualizarUsuarioCommand(
        Guid Id, 
        string Nombre, 
        string Apellido,
        string Rol,
        string NombreDeUsuario,
        string Correo,
        string Contrasena
    ) : IRequest<ErrorOr<Unit>>;
}
