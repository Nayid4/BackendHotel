using Dominio.Generos;
using Dominio.Paises;
using Dominio.Primitivos;

namespace Aplicacion.Paises.Actualizar
{
    public sealed class ActualizarPaisCommandHandler : IRequestHandler<ActualizarPaisCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioPais _repositorioPais;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarPaisCommandHandler(IRepositorioPais repositorioPais, IUnitOfWork unitOfWork)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarPaisCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioPais.ListarPorId(new IdPais(comando.Id)) is not Pais pais)
            {
                return Error.NotFound("Pais.NoEncontrado", "No se encontro el pais.");
            }

            if (await _repositorioPais.ListarPorNombre(comando.Nombre) is Pais pais2 && !pais.Nombre.Equals(pais.Nombre))
            {
                return Error.Conflict("Pais.Encontrado", "Ya existe un pais con ese nombre.");
            }

            pais.Actualizar(comando.Nombre);

            _repositorioPais.Actualizar(pais);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
