using Dominio.Directores;
using Dominio.Paises;
using Dominio.Primitivos;

namespace Aplicacion.Directores.Actualizar
{
    public sealed class ActualizarDirectorCommandHandler : IRequestHandler<ActualizarDirectorCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioDirector _repositorioDirector; 
        private readonly IRepositorioPais _repositorioPais;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarDirectorCommandHandler(IRepositorioPais repositorioPais, IUnitOfWork unitOfWork, IRepositorioDirector repositorioDirector)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioDirector = repositorioDirector ?? throw new ArgumentNullException(nameof(repositorioDirector));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarDirectorCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioDirector.ListarPorId(new IdDirector(comando.Id)) is not Director director)
            {
                return Error.NotFound("Director.NoEncontrado", "No se encontro el director.");
            }

            if (await _repositorioPais.ListarPorId(new IdPais(comando.Pais.Id)) is not Pais pais)
            {
                return Error.NotFound("Pais.NoEncontrado", "No se encontro el pais.");
            }


            director.Actualizar(comando.Nombre, comando.Apellido, pais.Id);

            _repositorioDirector.Actualizar(director);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
