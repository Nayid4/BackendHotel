using Aplicacion.Actores.Comun;
using Aplicacion.comun.ListarDatos;
using Aplicacion.Directores.Comun;
using Aplicacion.Paises.Comun;

namespace Aplicacion.Directores.ListarConFiltros
{
    public record ListarConFiltrosDirectorQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaDirector>>>;
}
