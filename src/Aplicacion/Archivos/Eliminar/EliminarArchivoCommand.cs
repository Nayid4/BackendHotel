

namespace Aplicacion.Archivos.Eliminar
{
    public record EliminarArchivoCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
