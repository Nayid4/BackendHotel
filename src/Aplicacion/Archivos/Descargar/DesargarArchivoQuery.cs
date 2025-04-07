
using Aplicacion.comun.Dtos;

namespace Aplicacion.Archivos.Descargar
{
    public record DesargarArchivoQuery(Guid Id) : IRequest<ErrorOr<RespuestaDeArchivo>>;
}
