
using Aplicacion.comun.ListarDatos;
using Aplicacion.Reservas.Comun;

namespace Aplicacion.Reservas.ListarConFiltros
{
    public record ListarConFiltrosReservaQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaReserva>>>;
}
