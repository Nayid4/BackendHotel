using Aplicacion.ServiciosHotel.Comun;
using Dominio.Servicios;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.ServiciosHotel.ListarTodos
{
    public sealed class ListarTodosLosServiciosQueryHandler : IRequestHandler<ListarTodosLosServiciosQuery, ErrorOr<IReadOnlyList<RespuestaServicio>>>
    {
        private readonly IRepositorioServicio _repositorioServicio;

        public ListarTodosLosServiciosQueryHandler(IRepositorioServicio repositorioServicio)
        {
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaServicio>>> Handle(ListarTodosLosServiciosQuery request, CancellationToken cancellationToken)
        {
            var servicios = await _repositorioServicio.ListarTodos()
                .Select(ge => 
                new RespuestaServicio(
                    ge.Id.Valor, 
                    ge.Nombre
                )).ToListAsync(cancellationToken);

            return servicios;
        }
    }
}
