
using Aplicacion.comun.ListarDatos;
using Aplicacion.Habitaciones.Comun;

namespace Aplicacion.Habitaciones.ListarConFiltros
{
    public record ListarConFiltrosHabitacionQuery(
        string? TerminoDeBusqueda,
        string? OrdenarColumna,
        string? OrdenarLista,
        int Pagina,
        int TamanoPagina
    ) : IRequest<ErrorOr<ListaPaginada<RespuestaHabitacion>>>;
}
