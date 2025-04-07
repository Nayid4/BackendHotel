
using Aplicacion.Reservas.Actualizar;
using Aplicacion.Reservas.Crear;
using Aplicacion.Reservas.Eliminar;
using Aplicacion.Reservas.ListarConFiltros;
using Aplicacion.Reservas.ListarPorId;
using Aplicacion.Reservas.ListarTodos;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.API.Controladores
{
    [Route("reserva")]
    [Authorize]
    public class ControladorReserva : ApiController
    {
        private readonly ISender _mediator;

        public ControladorReserva(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var resultadosDeListarTodos = await _mediator.Send(new ListarTodosLasReservasQuery());

            return resultadosDeListarTodos.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(Guid id)
        {
            var resultadosDeListarPorId = await _mediator.Send(new ListarPorIdDeReservaQuery(id));

            return resultadosDeListarPorId.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearReservaCommand comando)
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
            var resultadoDeEliminar = await _mediator.Send(new EliminarReservaCommand(id));

            return resultadoDeEliminar.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarReservaCommand comando)
        {
            if (comando.Id != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Reserva.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
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
        public async Task<IActionResult> ListarPorFiltro([FromBody] ListarConFiltrosReservaQuery comando)
        {
            var resultadoDeFiltrar = await _mediator.Send(comando);

            return resultadoDeFiltrar.Match(
                resp => Ok(resp),
                errores => Problem(errores)
            );
        }
    }
}
