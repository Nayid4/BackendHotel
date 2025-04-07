using Aplicacion.comun.ListarDatos;
using Aplicacion.Paises.Comun;
using Aplicacion.Paises.ListarConFiltros;
using Dominio.Paises;
using System.Linq.Expressions;

namespace Aplicacion.Paises.ListarConFiltros
{
    public sealed class ListarConFiltrosPaisQueryHandler : IRequestHandler<ListarConFiltrosPaisQuery, ErrorOr<ListaPaginada<RespuestaPais>>>
    {
        private readonly IRepositorioPais _repositorioPais;

        public ListarConFiltrosPaisQueryHandler(IRepositorioPais repositorioPais)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
        }
        public async Task<ErrorOr<ListaPaginada<RespuestaPais>>> Handle(ListarConFiltrosPaisQuery consulta, CancellationToken cancellationToken)
        {
            var paises = _repositorioPais.ListarTodos();

            if (!string.IsNullOrWhiteSpace(consulta.TerminoDeBusqueda))
            {
                paises = paises.Where(g => 
                    g.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower())
                );
            }

            if (consulta.OrdenarLista?.ToLower() == "desc")
            {
                paises = paises.OrderByDescending(ListarOrdenDePropiedad(consulta));
            } else
            {
                paises = paises.OrderBy(ListarOrdenDePropiedad(consulta));
            }

            var resultado = paises
                .Select(ge =>
                new RespuestaPais(
                    ge.Id.Valor,
                    ge.Nombre
                ));

            var listaDePaises = await ListaPaginada<RespuestaPais>.CrearAsync(
                    resultado,
                    consulta.Pagina,
                    consulta.TamanoPagina
                );

            return listaDePaises;

        }

        private static Expression<Func<Pais, object>> ListarOrdenDePropiedad(ListarConFiltrosPaisQuery consulta)
        {
            return consulta.OrdenarColumna?.ToLower() switch
            {
                "nombre" => pais => pais.Nombre,
                _ => pais => pais.Id
            };

        }
    }
}
