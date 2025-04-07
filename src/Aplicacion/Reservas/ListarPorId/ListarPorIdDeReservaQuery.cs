using Aplicacion.Reservas.Comun;

namespace Aplicacion.Reservas.ListarPorId
{
    public record ListarPorIdDeReservaQuery(Guid Id) : IRequest<ErrorOr<RespuestaReserva>>;
}
