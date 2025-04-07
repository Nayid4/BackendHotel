
using Dominio.Actores;
using Dominio.Directores;
using Dominio.Paises;
using Dominio.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Actores.Crear
{
    public sealed class CrearAutorCommandHandler : IRequestHandler<CrearActorCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioActor _repositorioActor;
        private readonly IRepositorioPais _repositorioPais;
        private readonly IUnitOfWork _unitOfWork;

        public CrearAutorCommandHandler(IRepositorioActor repositorioActor, IUnitOfWork unitOfWork, IRepositorioPais repositorioPais)
        {
            _repositorioActor = repositorioActor ?? throw new ArgumentNullException(nameof(repositorioActor));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearActorCommand comando, CancellationToken cancellationToken)
        {

            if (await _repositorioPais.ListarPorId(new IdPais(comando.Pais.Id)) is not Pais pais)
            {
                return Error.Conflict("Pais.NoEncontrado", "No se encontro el pais.");
            }
             
            var nuevoActor = new Actor(
                new IdActor(Guid.NewGuid()),
                comando.Nombre,
                comando.Apellido,
                pais.Id
            );

            _repositorioActor.Crear(nuevoActor);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
