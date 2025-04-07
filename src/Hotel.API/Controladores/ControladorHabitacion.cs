
using Aplicacion.Habitaciones.Actualizar;
using Aplicacion.Habitaciones.Crear;
using Aplicacion.Habitaciones.Eliminar;
using Aplicacion.Habitaciones.ListarConFiltros;
using Aplicacion.Habitaciones.ListarPorId;
using Aplicacion.Habitaciones.ListarTodos;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.API.Controladores
{
    [Route("habitacion")]
    [Authorize]
    public class ControladorHabitacion : ApiController
    {
        private readonly ISender _mediator;

        public ControladorHabitacion(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new ListarTodosLasHabitacionesQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new ListarPorIdDeHabitacionQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] CrearHabitacionCommand comando)
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
            var resultadoDeEliminar = await _mediator.Send(new EliminarHabitacionCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarHabitacionCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Habitacion.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
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
        public async Task<IActionResult> ListarPorFiltro([FromBody] ListarConFiltrosHabitacionQuery comando)
        {
            var resultadoDeFiltrar = await _mediator.Send(comando);

            return resultadoDeFiltrar.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }
    }
}
