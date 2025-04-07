using Aplicacion.Utilidades;
using Dominio.Primitivos;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios.Actualizar
{
    public sealed class ActualizarUsuarioCommandHandler : IRequestHandler<ActualizarUsuarioCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario; 
        private readonly IGestionToken _gestionToken;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarUsuarioCommandHandler(IGestionToken gestionToken, IUnitOfWork unitOfWork, IRepositorioUsuario repositorioUsuario)
        {
            _gestionToken = gestionToken ?? throw new ArgumentNullException(nameof(gestionToken));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarUsuarioCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.Id)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            if (await _repositorioUsuario.ListarPorNombreDeUsuario(comando.NombreDeUsuario) is Usuario usuario2 && !usuario.NombreDeUsuario.Equals(comando.NombreDeUsuario))
            {
                return Error.Conflict("Usuario.Encontrado", "Ya esta en uso ese nombre de usuario.");
            }

            usuario.Actualizar(comando.Nombre, comando.Apellido, comando.Rol, comando.NombreDeUsuario, comando.Correo, _gestionToken.EncryptSHA256(comando.Contrasena));

            _repositorioUsuario.Actualizar(usuario);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
