
using Dominio.Directores;
using Dominio.Paises;
using Dominio.Primitivos;

namespace Aplicacion.Directores.Eliminar
{
    public sealed class EliminarDirectorCommandHandler : IRequestHandler<EliminarDirectorCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioDirector _repositorioDirector;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarDirectorCommandHandler(IRepositorioDirector repositorioDirector, IUnitOfWork unitOfWork)
        {
            _repositorioDirector = repositorioDirector ?? throw new ArgumentNullException(nameof(repositorioDirector));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarDirectorCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioDirector.ListarPorId(new IdDirector(comando.Id)) is not Director director)
            {
                return Error.NotFound("Director.NoEncontrado", "No se encontro el director.");
            }

            _repositorioDirector.Eliminar(director);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
