
namespace Aplicacion.Resenas.Eliminar
{
    public record EliminarResenaCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
