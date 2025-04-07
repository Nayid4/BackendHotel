
using Dominio.Directores;
using Dominio.Paises;
using Dominio.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Directores.Crear
{
    public sealed class CrearDirectorCommandHandler : IRequestHandler<CrearDirectorCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioDirector _repositorioDirector;
        private readonly IRepositorioPais _repositorioPais;
        private readonly IUnitOfWork _unitOfWork;

        public CrearDirectorCommandHandler(IRepositorioDirector repositorioDirector, IUnitOfWork unitOfWork, IRepositorioPais repositorioPais)
        {
            _repositorioDirector = repositorioDirector ?? throw new ArgumentNullException(nameof(repositorioDirector));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearDirectorCommand comando, CancellationToken cancellationToken)
        {

            if (await _repositorioPais.ListarPorId(new IdPais(comando.Pais.Id)) is not Pais pais)
            {
                return Error.Conflict("Pais.NoEncontrado", "No se encontro el pais.");
            }

            var nuedoDirector = new Director(
                new IdDirector(Guid.NewGuid()),
                comando.Nombre,
                comando.Apellido,
                pais.Id
            );

            _repositorioDirector.Crear(nuedoDirector);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
