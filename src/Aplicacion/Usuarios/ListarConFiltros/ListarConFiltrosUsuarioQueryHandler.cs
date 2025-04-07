
using Aplicacion.comun.ListarDatos;
using Aplicacion.Usuarios.Comun;
using Dominio.Usuarios;
using System.Linq.Expressions;

namespace Aplicacion.Usuarios.ListarConFiltros
{
    public sealed class ListarConFiltrosUsuarioQueryHandler : IRequestHandler<ListarConFiltrosUsuarioQuery, ErrorOr<ListaPaginada<RespuestaUsuario>>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ListarConFiltrosUsuarioQueryHandler(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }
        public async Task<ErrorOr<ListaPaginada<RespuestaUsuario>>> Handle(ListarConFiltrosUsuarioQuery consulta, CancellationToken cancellationToken)
        {
            var usuarios = _repositorioUsuario.ListarTodos();

            if (!string.IsNullOrWhiteSpace(consulta.TerminoDeBusqueda))
            {
                usuarios = usuarios.Where(at => 
                    at.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Apellido.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.NombreDeUsuario.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower())
                );
            }

            if (consulta.OrdenarLista?.ToLower() == "desc")
            {
                usuarios = usuarios.OrderByDescending(ListarOrdenDePropiedad(consulta));
            } else
            {
                usuarios = usuarios.OrderBy(ListarOrdenDePropiedad(consulta));
            }



            var resultado = usuarios.Select(usuario => new RespuestaUsuario(
                    usuario.Id.Valor,
                    usuario.Nombre,
                    usuario.Apellido,
                    usuario.Rol,
                    usuario.NombreDeUsuario,
                    usuario.Correo
            ));

            var listaDeUsuarios = await ListaPaginada<RespuestaUsuario>.CrearAsync(
                    resultado,
                    consulta.Pagina,
                    consulta.TamanoPagina
                );

            return listaDeUsuarios;

        }

        private static Expression<Func<Usuario, object>> ListarOrdenDePropiedad(ListarConFiltrosUsuarioQuery consulta)
        {
            return consulta.OrdenarColumna?.ToLower() switch
            {
                "nombre" => usuario => usuario.Nombre,
                "apellido" => usuario => usuario.Apellido,
                "rol" => usuario => usuario.Rol,
                "nombreDeUsuario" => usuario => usuario.NombreDeUsuario,
                "correo" => usuario => usuario.Correo,
                _ => usuario => usuario.Id
            };
        }

    }
}
