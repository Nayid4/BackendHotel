using Aplicacion.ServiciosHotel.Comun;

namespace Aplicacion.ServiciosHotel.ListarPorId
{
    public record ListarPorIdDeServicioQuery(Guid Id) : IRequest<ErrorOr<RespuestaServicio>>;
}
