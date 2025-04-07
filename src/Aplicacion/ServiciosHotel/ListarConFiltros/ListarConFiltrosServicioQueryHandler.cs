using Aplicacion.comun.ListarDatos;
using Aplicacion.ServiciosHotel.Comun;
using Dominio.Servicios;
using System.Linq.Expressions;

namespace Aplicacion.Generos.ListarConFiltros
{
    public sealed class ListarConFiltrosServicioQueryHandler : IRequestHandler<ListarConFiltrosServicioQuery, ErrorOr<ListaPaginada<RespuestaServicio>>>
    {
        private readonly IRepositorioServicio _repositorioServicio;

        public ListarConFiltrosServicioQueryHandler(IRepositorioServicio repositorioServicio)
        {
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
        }
        public async Task<ErrorOr<ListaPaginada<RespuestaServicio>>> Handle(ListarConFiltrosServicioQuery consulta, CancellationToken cancellationToken)
        {
            var servicios = _repositorioServicio.ListarTodos();

            if (!string.IsNullOrWhiteSpace(consulta.TerminoDeBusqueda))
            {
                servicios = servicios.Where(g => 
                    g.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower())
                );
            }

            if (consulta.OrdenarLista?.ToLower() == "desc")
            {
                servicios = servicios.OrderByDescending(ListarOrdenDePropiedad(consulta));
            } else
            {
                servicios = servicios.OrderBy(ListarOrdenDePropiedad(consulta));
            }

            var resultado = servicios
                .Select(ge =>
                new RespuestaServicio(
                    ge.Id.Valor,
                    ge.Nombre
                ));

            var listaDeServicios = await ListaPaginada<RespuestaServicio>.CrearAsync(
                    resultado,
                    consulta.Pagina,
                    consulta.TamanoPagina
                );

            return listaDeServicios;

        }

        private static Expression<Func<Servicio, object>> ListarOrdenDePropiedad(ListarConFiltrosServicioQuery consulta)
        {
            return consulta.OrdenarColumna?.ToLower() switch
            {
                "nombre" => servicio => servicio.Nombre,
                _ => servicio => servicio.Id
            };

        }
    }
}
