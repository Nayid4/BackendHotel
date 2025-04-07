using Microsoft.AspNetCore.Http;

namespace Aplicacion.Archivos.Subir
{
    public record SubirArchivoCommand(IFormFile File) : IRequest<ErrorOr<Guid>>;
}
