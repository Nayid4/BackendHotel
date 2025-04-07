
namespace Aplicacion.Reservas.Eliminar
{
    public record EliminarReservaCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
