using Aplicacion.Usuarios.Comun;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios.ListarPorId
{
    public sealed class ListarPorIdDeUsuarioQueryHandler : IRequestHandler<ListarPorIdDeUsuarioQuery, ErrorOr<RespuestaUsuario>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ListarPorIdDeUsuarioQueryHandler(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<ErrorOr<RespuestaUsuario>> Handle(ListarPorIdDeUsuarioQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(consulta.Id)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "No se encontro el usuario.");
            }


            var respuesta = new RespuestaUsuario(
                usuario.Id.Valor, 
                usuario.Nombre, 
                usuario.Apellido,
                usuario.Rol,
                usuario.NombreDeUsuario,
                usuario.Correo

            );

            return respuesta;
        }
    }
}
