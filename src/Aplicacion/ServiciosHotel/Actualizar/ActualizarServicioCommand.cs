
namespace Aplicacion.ServiciosHotel.Actualizar
{
    public record ActualizarServicioCommand(Guid Id, string Nombre) : IRequest<ErrorOr<Unit>>;
}
