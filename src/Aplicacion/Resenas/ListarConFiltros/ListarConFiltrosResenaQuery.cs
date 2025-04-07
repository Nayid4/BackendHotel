

using Aplicacion.comun.ListarDatos;
using Aplicacion.Resenas.Comun;

namespace Aplicacion.Resenas.ListarConFiltros
{
    public record ListarConFiltrosResenaQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaResena>>>;
}
