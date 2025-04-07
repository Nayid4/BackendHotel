
using Dominio.Paises;
using Dominio.Primitivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Paises.Crear
{
    public sealed class CrearPaisCommandHandler : IRequestHandler<CrearPaisCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioPais _repositorioPais;
        private readonly IUnitOfWork _unitOfWork;

        public CrearPaisCommandHandler(IRepositorioPais repositorioPais, IUnitOfWork unitOfWork)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearPaisCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioPais.ListarPorNombre(comando.Nombre) is Pais pais)
            {
                return Error.Conflict("Pais.Encontrado","Ya existe un pais con ese nombre.");
            }

            var nuedoPais = new Pais(
                new IdPais(Guid.NewGuid()),
                comando.Nombre
            );

            _repositorioPais.Crear(nuedoPais);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
