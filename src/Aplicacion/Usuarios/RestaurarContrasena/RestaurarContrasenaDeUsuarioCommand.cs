
namespace Aplicacion.Usuarios.RestaurarContrasena
{
    public record RestaurarContrasenaDeUsuarioCommand(Guid IdUsuario, string ContrasenaUno, string ContrasenaDos) : IRequest<ErrorOr<Unit>>;
}
