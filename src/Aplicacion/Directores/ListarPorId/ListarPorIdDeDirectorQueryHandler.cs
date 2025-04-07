using Aplicacion.Directores.Comun;
using Dominio.Directores;
using Dominio.Paises;

namespace Aplicacion.Directores.ListarPorId
{
    public sealed class ListarPorIdDeDirectorQueryHandler : IRequestHandler<ListarPorIdDeDirectorQuery, ErrorOr<RespuestaDirector>>
    {
        private readonly IRepositorioDirector _repositorioDirector;
        private readonly IRepositorioPais _repositorioPais;

        public ListarPorIdDeDirectorQueryHandler(IRepositorioDirector repositorioDirector, IRepositorioPais repositorioPais)
        {
            _repositorioDirector = repositorioDirector ?? throw new ArgumentNullException(nameof(repositorioDirector));
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
        }

        public async Task<ErrorOr<RespuestaDirector>> Handle(ListarPorIdDeDirectorQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioDirector.ListarPorId(new IdDirector(consulta.Id)) is not Director director)
            {
                return Error.NotFound("Director.NoEncontrado", "No se encontro el director.");
            }

            if (await _repositorioPais.ListarPorId(director.IdPais) is not Pais pais)
            {
                return Error.NotFound("Pais.NoEncontrado", "No se encontro el pais.");
            }

            var respuesta = new RespuestaDirector(
                director.Id.Valor, 
                director.Nombre, 
                director.Apellido,
                new RespuestaPais(
                    director.Pais!.Id.Valor,
                    director.Pais!.Nombre
                )
            );

            return respuesta;
        }
    }
}
