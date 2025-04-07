
using Aplicacion.Usuarios.Comun;
using Aplicacion.Utilidades;
using Dominio.ObjetosDeValor;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios.RefrescarToken
{
    public sealed class RefrescarTokenQueryHandler : IRequestHandler<RefrescarTokenQuery, ErrorOr<RespuestaIniciarSesion>>
    {
        private readonly IGestionToken _gestionToken;
        private readonly IRepositorioUsuario _repositorioUsuario;

        public RefrescarTokenQueryHandler(IGestionToken gestionToken, IRepositorioUsuario repositorioUsuario)
        {
            _gestionToken = gestionToken ?? throw new ArgumentNullException(nameof(gestionToken));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<ErrorOr<RespuestaIniciarSesion>> Handle(RefrescarTokenQuery consulta, CancellationToken cancellationToken)
        {
            if (_gestionToken.DecodeJWT() is not DatosUsuarioDTO datosUsuario)
            {
                return Error.NotFound("Autenticacion.TokenNoEcontrado", "No se ha encontrado el token del usuario.");
            }

            if (await _repositorioUsuario.ListarPorId(new IdUsuario(datosUsuario.Id)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            var respuesta = new RespuestaIniciarSesion(
                _gestionToken.GenerateJWT(usuario, 1),
                _gestionToken.GenerateJWT(usuario, 2)
            );

            return respuesta;
        }
    }
}
