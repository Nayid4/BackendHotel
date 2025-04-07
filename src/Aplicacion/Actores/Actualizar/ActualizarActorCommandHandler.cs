using Dominio.Actores;
using Dominio.Directores;
using Dominio.Paises;
using Dominio.Primitivos;

namespace Aplicacion.Actores.Actualizar
{
    public sealed class ActualizarActorCommandHandler : IRequestHandler<ActualizarActorCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioActor _repositorioActor; 
        private readonly IRepositorioPais _repositorioPais;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarActorCommandHandler(IRepositorioPais repositorioPais, IUnitOfWork unitOfWork, IRepositorioActor repositorioActor)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioActor = repositorioActor ?? throw new ArgumentNullException(nameof(repositorioActor));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarActorCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioActor.ListarPorId(new IdActor(comando.Id)) is not Actor actor)
            {
                return Error.NotFound("Actor.NoEncontrado", "No se encontro el actor.");
            }

            if (await _repositorioPais.ListarPorId(new IdPais(comando.Pais.Id)) is not Pais pais)
            {
                return Error.NotFound("Pais.NoEncontrado", "No se encontro el pais.");
            }


            actor.Actualizar(comando.Nombre, comando.Apellido, pais.Id);

            _repositorioActor.Actualizar(actor);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
