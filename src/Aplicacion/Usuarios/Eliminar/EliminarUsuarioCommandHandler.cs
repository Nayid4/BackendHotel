
using Dominio.Primitivos;
using Dominio.Usuarios;

namespace Aplicacion.Usuarios.Eliminar
{
    public sealed class EliminarUsuarioCommandHandler : IRequestHandler<EliminarUsuarioCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarUsuarioCommandHandler(IRepositorioUsuario repositorioUsuario, IUnitOfWork unitOfWork)
        {
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarUsuarioCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.Id)) is not Usuario usuario)
            {
                return Error.NotFound("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            _repositorioUsuario.Eliminar(usuario);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
