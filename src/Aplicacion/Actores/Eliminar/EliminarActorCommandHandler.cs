
using Dominio.Actores;
using Dominio.Primitivos;

namespace Aplicacion.Actores.Eliminar
{
    public sealed class EliminarActorCommandHandler : IRequestHandler<EliminarActorCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioActor _repositorioActor;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarActorCommandHandler(IRepositorioActor repositorioActor, IUnitOfWork unitOfWork)
        {
            _repositorioActor = repositorioActor ?? throw new ArgumentNullException(nameof(repositorioActor));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarActorCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioActor.ListarPorId(new IdActor(comando.Id)) is not Actor actor)
            {
                return Error.NotFound("Actor.NoEncontrado", "No se encontro el actor.");
            }

            _repositorioActor.Eliminar(actor);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
