
using Aplicacion.comun.ListarDatos;
using Aplicacion.Usuarios.Comun;

namespace Aplicacion.Usuarios.ListarConFiltros
{
    public record ListarConFiltrosUsuarioQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaUsuario>>>;
}
