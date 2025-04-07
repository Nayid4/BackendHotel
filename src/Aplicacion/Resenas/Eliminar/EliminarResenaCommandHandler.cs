
using Dominio.Resenas;
using Dominio.Primitivos;

namespace Aplicacion.Resenas.Eliminar
{
    public sealed class EliminarResenaCommandHandler : IRequestHandler<EliminarResenaCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioResena _repositorioResena;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarResenaCommandHandler(IRepositorioResena repositorioResena, IUnitOfWork unitOfWork)
        {
            _repositorioResena = repositorioResena ?? throw new ArgumentNullException(nameof(repositorioResena));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarResenaCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioResena.ListarPorId(new IdResena(comando.Id)) is not Resena resena)
            {
                return Error.NotFound("Resena.NoEncontrado", "No se encontro el Resena.");
            }

            _repositorioResena.Eliminar(resena);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
