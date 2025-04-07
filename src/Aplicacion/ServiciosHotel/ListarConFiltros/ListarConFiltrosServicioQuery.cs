using Aplicacion.comun.ListarDatos;
using Aplicacion.ServiciosHotel.Comun;

namespace Aplicacion.Generos.ListarConFiltros
{
    public record ListarConFiltrosServicioQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaServicio>>>;
}
