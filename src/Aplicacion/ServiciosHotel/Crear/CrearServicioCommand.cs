

namespace Aplicacion.ServiciosHotel.Crear
{
    public record CrearServicioCommand(string Nombre) : IRequest<ErrorOr<Unit>>;
}
