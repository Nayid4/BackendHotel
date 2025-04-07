using Aplicacion.Almacenamiento;
using Aplicacion.comun.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Archivos.Descargar
{
    public sealed class DescargarArchivoQueryHandler : IRequestHandler<DesargarArchivoQuery, ErrorOr<RespuestaDeArchivo>>
    {
        private readonly IServicioBlob _servicioBlob;

        public DescargarArchivoQueryHandler(IServicioBlob servicioBlob)
        {
            _servicioBlob = servicioBlob ?? throw new ArgumentNullException(nameof(servicioBlob));
        }

        public async Task<ErrorOr<RespuestaDeArchivo>> Handle(DesargarArchivoQuery consulta, CancellationToken cancellationToken)
        {
            try
            {
                // Intentar descargar el archivo desde el servicio
                var resultado = await _servicioBlob.DescargarArchivoAsync(consulta.Id);

                // Usar Match para procesar el resultado
                return resultado.Match<ErrorOr<RespuestaDeArchivo>>(
                    archivo => archivo, // Caso exitoso: devolver el archivo descargado
                    errores => errores // Caso de error: propagar los errores
                );
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y devolver un error genérico
                return Error.Failure("ArchivoError", $"Ocurrió un error al intentar descargar el archivo: {ex.Message}");
            }
        }
    }
}
