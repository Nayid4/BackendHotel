using Aplicacion.Usuarios.Comun;
using Aplicacion.Utilidades;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios.IniciarSesion
{
    public sealed class IniciarSesionQueryHandler : IRequestHandler<IniciarSesionQuery, ErrorOr<RespuestaIniciarSesion>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IGestionToken _gestionToken;

        public IniciarSesionQueryHandler(IRepositorioUsuario repositorioUsuario, IGestionToken gestionToken)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _gestionToken = gestionToken ?? throw new ArgumentNullException(nameof(gestionToken));
        }

        public async Task<ErrorOr<RespuestaIniciarSesion>> Handle(IniciarSesionQuery consulta, CancellationToken cancellationToken)
        {
            string contrasena = _gestionToken.EncryptSHA256(consulta.Contrasena);

            if (await _repositorioUsuario.IniciarSesion(consulta.NombreDeUsuario, contrasena) is not Usuario usuario)
            {
                return Error.NotFound("Autenticacion.NoEncontrado", "Usuario o Contraseña incorrectos.");
            }

            var respuesta = new RespuestaIniciarSesion(
                _gestionToken.GenerateJWT(usuario, 1),
                _gestionToken.GenerateJWT(usuario, 2)

            );

            return respuesta;
        }
    }
}
