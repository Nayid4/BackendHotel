using Aplicacion.ServiciosHotel.Comun;

namespace Aplicacion.ServiciosHotel.ListarTodos
{
    public record ListarTodosLosServiciosQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaServicio>>>;
}
