using Dominio.ObjetosDeValor;

namespace Aplicacion.Usuarios.DatosUsuario
{
    public record DatosUsuarioQuery() : IRequest<ErrorOr<DatosUsuarioDTO>>;
}
