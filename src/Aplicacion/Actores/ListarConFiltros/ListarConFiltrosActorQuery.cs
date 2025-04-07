using Aplicacion.Actores.Comun;
using Aplicacion.comun.ListarDatos;
using Aplicacion.Paises.Comun;

namespace Aplicacion.Actores.ListarConFiltros
{
    public record ListarConFiltrosActorQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaActor>>>;
}
