
using Dominio.Paises;
using Dominio.Primitivos;

namespace Aplicacion.Paises.Eliminar
{
    public sealed class EliminarPaisCommandHandler : IRequestHandler<EliminarPaisCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioPais _repositorioPais;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarPaisCommandHandler(IRepositorioPais repositorioPais, IUnitOfWork unitOfWork)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarPaisCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioPais.ListarPorId(new IdPais(comando.Id)) is not Pais pais)
            {
                return Error.NotFound("Pais.NoEncontrado", "No se encontro el pais.");
            }

            _repositorioPais.Eliminar(pais);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
