

using Aplicacion.Utilidades;
using Dominio.Primitivos;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios.RestaurarContrasena
{
    public sealed class RestaurarContrasenaDeUsuarioCommandHandler : IRequestHandler<RestaurarContrasenaDeUsuarioCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGestionToken _gestionToken;

        public RestaurarContrasenaDeUsuarioCommandHandler(IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork, IGestionToken gestionToken)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _gestionToken = gestionToken ?? throw new ArgumentNullException(nameof(gestionToken));
        }

        public async Task<ErrorOr<Unit>> Handle(RestaurarContrasenaDeUsuarioCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.IdUsuario)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            if (!comando.ContrasenaUno.Equals(comando.ContrasenaDos))
            {
                return Error.Conflict("Usuario.ContrasenaNoCoincide", "Las contraseñas no coinciden.");
            }

            usuario.RestaurarContrasena(_gestionToken.EncryptSHA256(comando.ContrasenaUno));

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
