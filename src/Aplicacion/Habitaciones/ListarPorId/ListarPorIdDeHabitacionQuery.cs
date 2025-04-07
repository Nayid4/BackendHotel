using Aplicacion.Habitaciones.Comun;

namespace Aplicacion.Habitaciones.ListarPorId
{
    public record ListarPorIdDeHabitacionQuery(Guid Id) : IRequest<ErrorOr<RespuestaHabitacion>>;
}
