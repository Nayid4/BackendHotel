using Aplicacion.Generos.Comun;
using Aplicacion.Paises.Comun;
using Dominio.Generos;
using Dominio.Paises;

namespace Aplicacion.Paises.ListarPorId
{
    public sealed class ListarPorIdDePaisQueryHandler : IRequestHandler<ListarPorIdDePaisQuery, ErrorOr<RespuestaPais>>
    {
        private readonly IRepositorioPais _repositorioPais;

        public ListarPorIdDePaisQueryHandler(IRepositorioPais repositorioPais)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
        }

        public async Task<ErrorOr<RespuestaPais>> Handle(ListarPorIdDePaisQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioPais.ListarPorId(new IdPais(consulta.Id)) is not Pais pais)
            {
                return Error.NotFound("Pais.NoEncontrado", "No se encontro el pais.");
            }

            var respuesta = new RespuestaPais(pais.Id.Valor, pais.Nombre);

            return respuesta;
        }
    }
}
