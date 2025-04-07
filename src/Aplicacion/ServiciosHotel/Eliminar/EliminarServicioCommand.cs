

namespace Aplicacion.ServiciosHotel.Eliminar
{
    public record EliminarServicioCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
