using Aplicacion.ServiciosHotel.Comun;
using Dominio.Servicios;

namespace Aplicacion.ServiciosHotel.ListarPorId
{
    public sealed class ListarPorIdDeServicioQueryHandler : IRequestHandler<ListarPorIdDeServicioQuery, ErrorOr<RespuestaServicio>>
    {
        private readonly IRepositorioServicio _repositorioServicio;

        public ListarPorIdDeServicioQueryHandler(IRepositorioServicio repositorioServicio)
        {
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
        }

        public async Task<ErrorOr<RespuestaServicio>> Handle(ListarPorIdDeServicioQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioServicio.ListarPorId(new IdServicio(consulta.Id)) is not Servicio servicio)
            {
                return Error.NotFound("Servicio.NoEncontrado", "No se encontro el servicio.");
            }

            var respuesta = new RespuestaServicio(servicio.Id.Valor, servicio.Nombre);

            return respuesta;
        }
    }
}
