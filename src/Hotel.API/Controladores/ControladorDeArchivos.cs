using Aplicacion.Archivos.Descargar;
using Aplicacion.Archivos.Eliminar;
using Aplicacion.Archivos.Subir;

namespace Hotel.API.Controladores
{
    [Route("archivo")]
    public class ControladorDeArchivos : ApiController
    {
        private readonly ISender _mediador;

        public ControladorDeArchivos(ISender mediador)
        {
            _mediador = mediador ?? throw new ArgumentNullException(nameof(mediador));
        }

        [HttpPost("subir")]
        public async Task<IActionResult> SubirArchivo(IFormFile archivo)
        {
            var resultado = await _mediador.Send(new SubirArchivoCommand(archivo));

            return resultado.Match(
                idArchivo => Ok(new { Id = idArchivo }),
                error => Problem(error)
            );
        }

        [HttpGet("descargar/{id}")]
        public async Task<IActionResult> DescargarArchivo(Guid id)
        {
            var resultado = await _mediador.Send(new DesargarArchivoQuery(id));

            return resultado.Match(
                resp => File(resp.Stream, resp.ContentType),
                error => Problem(error)
            );
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarArchivo(Guid id)
        {
            var resultado = await _mediador.Send(new EliminarArchivoCommand(id));

            return resultado.Match(
                _ => NoContent(),
                error => Problem(error)
            );
        }
    }
}
