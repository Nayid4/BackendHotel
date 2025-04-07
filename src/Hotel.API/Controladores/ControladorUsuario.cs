
using Aplicacion.Paises.ListarConFiltros;
using Aplicacion.Usuarios.Actualizar;
using Aplicacion.Usuarios.Crear;
using Aplicacion.Usuarios.Eliminar;
using Aplicacion.Usuarios.ListarConFiltros;
using Aplicacion.Usuarios.ListarPorId;
using Aplicacion.Usuarios.ListarTodos;
using GestionDeSeriesAnimadas.API.Controladores;
using Microsoft.AspNetCore.Authorization;

namespace GestionDeSeriesAnimadas.API.Controladores
{
    [Route("usuario")]
    [Authorize]
    public class ControladorUsuario : ApiController
    {
        private readonly ISender _mediator;

        public ControladorUsuario(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new ListarTodosLosUsuariosQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new ListarPorIdDeUsuarioQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] CrearUsuarioCommand comando)
        {
            var resultadoDeCrear = await _mediator.Send(comando);

            return resultadoDeCrear.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var resultadoDeEliminar = await _mediator.Send(new EliminarUsuarioCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarUsuarioCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Genero.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizarListaTarea = await _mediator.Send(comando);

            return resultadoDeActualizarListaTarea.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPost("lista-paginada")]
        public async Task<IActionResult> ListarPorFiltro([FromBody] ListarConFiltrosUsuarioQuery comando)
        {
            var resultadoDeFiltrar = await _mediator.Send(comando);

            return resultadoDeFiltrar.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }
    }
}
