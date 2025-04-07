using Aplicacion.Almacenamiento;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Archivos.Eliminar
{
    public sealed class EliminarArchivoCommandHandler : IRequestHandler<EliminarArchivoCommand, ErrorOr<Unit>>
    {
        private readonly IServicioBlob _servicioBlob;

        public EliminarArchivoCommandHandler(IServicioBlob servicioBlob)
        {
            _servicioBlob = servicioBlob ?? throw new ArgumentNullException(nameof(servicioBlob));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarArchivoCommand consulta, CancellationToken cancellationToken)
        {
            try
            {
                // Intentar eliminar el archivo
                var resultado = await _servicioBlob.EliminarArchivoAsync(consulta.Id);

                // Usar Match para procesar el resultado
                return resultado.Match<ErrorOr<Unit>>(
                    _ => Unit.Value, // Caso exitoso: devolver Unit.Value
                    errores => errores // Caso de error: propagar los errores
                );
            }
            catch (Exception ex)
            {
                // Manejar excepciones inesperadas y devolver un error genérico
                return Error.Failure("ArchivoError", $"Ocurrió un error al intentar eliminar el archivo: {ex.Message}");
            }
        }
    }
}
