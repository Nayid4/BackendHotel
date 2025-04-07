
using Aplicacion.Utilidades;
using Dominio.Primitivos;
using Dominio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Usuarios.Crear
{
    public sealed class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IGestionToken _gestionToken;
        private readonly IUnitOfWork _unitOfWork;

        public CrearUsuarioCommandHandler(IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork, IGestionToken gestionToken)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _gestionToken = gestionToken ?? throw new ArgumentNullException(nameof(gestionToken));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearUsuarioCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorNombreDeUsuario(comando.NombreDeUsuario) is Usuario usuario)
            {
                return Error.Conflict("Usuario.Encontrado", "Ya esta en uso ese nombre de usuario.");
            }

            var nuevoUsuario = new Usuario(
                new IdUsuario(Guid.NewGuid()),
                comando.Nombre,
                comando.Apellido,
                comando.Rol,
                comando.NombreDeUsuario,
                comando.Correo,
                _gestionToken.EncryptSHA256(comando.Contrasena)
            );

            _repositorioUsuario.Crear(nuevoUsuario);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
