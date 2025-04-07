
namespace Aplicacion.Usuarios.Eliminar
{
    public record EliminarUsuarioCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
