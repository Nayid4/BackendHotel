using Aplicacion.Almacenamiento;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Archivos.Subir
{
    public sealed class SubirArchivoCommandHandler : IRequestHandler<SubirArchivoCommand, ErrorOr<Guid>>
    {
        private readonly IServicioBlob _servicioBlob;

        public SubirArchivoCommandHandler(IServicioBlob servicioBlob)
        {
            _servicioBlob = servicioBlob ?? throw new ArgumentNullException(nameof(servicioBlob));
        }

        public async Task<ErrorOr<Guid>> Handle(SubirArchivoCommand comando, CancellationToken cancellationToken)
        {
            // Validar que el archivo no sea nulo o vacío
            if (comando.File == null || comando.File.Length == 0)
                return Error.Failure("ArchivoError", "El archivo está vacío o es nulo.");

            // Validar el tipo de archivo (ContentType)
            var tiposPermitidos = new[]
            {
                "application/pdf",
                "image/jpeg",
                "image/png",
                "application/msword",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            };

            if (!tiposPermitidos.Contains(comando.File.ContentType))
                return Error.Failure("ArchivoError", $"El tipo de archivo '{comando.File.ContentType}' no está permitido.");

            // Validar tamaño del archivo (máximo 10 MB)
            const long tamanoMaximo = 10 * 1024 * 1024; // 10 MB
            if (comando.File.Length > tamanoMaximo)
                return Error.Failure("ArchivoError", "El tamaño del archivo excede el límite permitido de 10 MB.");

            // Leer el archivo como stream y subirlo al servicio
            using Stream stream = comando.File.OpenReadStream();
            var resultado = await _servicioBlob.SubirArchivoAsync(stream, comando.File.FileName, cancellationToken);

            // Retornar el resultado directamente con tipos explícitos
            return resultado.Match<ErrorOr<Guid>>(
                idArchivo => idArchivo, // Caso exitoso: devolver el GUID del archivo
                errores => errores // Caso de error: propagar los errores
            );
        }
    }
}
