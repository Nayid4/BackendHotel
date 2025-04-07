using Aplicacion.Usuarios.Comun;
using Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Usuarios.ListarTodos
{
    public sealed class ListarTodosLosUsuariosQueryHandler : IRequestHandler<ListarTodosLosUsuariosQuery, ErrorOr<IReadOnlyList<RespuestaUsuario>>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ListarTodosLosUsuariosQueryHandler(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaUsuario>>> Handle(ListarTodosLosUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _repositorioUsuario.ListarTodosLosUsuarios()
                .Select(usuario => new RespuestaUsuario(
                    usuario.Id.Valor,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Rol,
                    usuario.NombreDeUsuario,
                    usuario.Correo
                )).ToListAsync(cancellationToken);

            return usuarios;
        }
    }
}
