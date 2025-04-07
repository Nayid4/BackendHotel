using Aplicacion.comun.ListarDatos;
using Aplicacion.Paises.Comun;

namespace Aplicacion.Paises.ListarConFiltros
{
    public record ListarConFiltrosPaisQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaPais>>>;
}
