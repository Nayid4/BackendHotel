using Aplicacion.Usuarios.Actualizar;
using Aplicacion.Usuarios.DatosUsuario;
using Aplicacion.Usuarios.IniciarSesion;
using Aplicacion.Usuarios.RefrescarToken;
using Aplicacion.Usuarios.RestaurarContrasena;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.API.Controladores
{
    [Route("autenticacion")]
    
    public class ControladorAutenticacion : ApiController
    {
        private readonly ISender _mediator;

        public ControladorAutenticacion(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("iniciar-sesion")]
        [AllowAnonymous]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionQuery comando)
        {
            var resultadoDeIniciarSesion = await _mediator.Send(comando);

            return resultadoDeIniciarSesion.Match(
                auth => Ok(auth),
                errores => Problem(errores)
            );
        }

        [HttpGet("refrescar-token")]
        [Authorize]
        public async Task<IActionResult> RefescarToken()
        {
            var resultado = await _mediator.Send(new RefrescarTokenQuery());

            return resultado.Match(
                auth => Ok(auth),
                errores => Problem(errores)
            );
        }

        [HttpGet("datos-usuario")]
        [Authorize]
        public async Task<IActionResult> ListarDatosUsuario()
        {
            var resultadoDeListarDatosUsuario = await _mediator.Send(new DatosUsuarioQuery());

            return resultadoDeListarDatosUsuario.Match(
                usuarioid => Ok(usuarioid),
                errores => Problem(errores)
            );
        }

        [HttpPut("restaurar-contrasena/{id}")]
        public async Task<IActionResult> Actualizar(Guid id, [FromBody] RestaurarContrasenaDeUsuarioCommand comando)
        {
            if (comando.IdUsuario != id)
            {
                List<Error> errores = new()
                {
                    Error.Validation("Usuario.ActualizacionInvalida","El Id de la consulta no es igual al que esta en la solicitud.")
                };

                return Problem(errores);
            }

            var resultadoDeActualizarListaTarea = await _mediator.Send(comando);

            return resultadoDeActualizarListaTarea.Match(
                resp => NoContent(),
                errores => Problem(errores)
            );
        }
    }
}
