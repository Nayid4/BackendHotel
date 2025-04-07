using Aplicacion.comun.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Almacenamiento
{
    public interface IServicioBlob
    {
        Task<ErrorOr<Guid>> SubirArchivoAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);
        Task<ErrorOr<RespuestaDeArchivo>> DescargarArchivoAsync(Guid idArchivo, CancellationToken cancellationToken = default);
        Task<ErrorOr<Unit>> EliminarArchivoAsync(Guid idArchivo, CancellationToken cancellationToken = default);
    }
}
