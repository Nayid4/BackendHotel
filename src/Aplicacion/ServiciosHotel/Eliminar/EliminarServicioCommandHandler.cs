
using Dominio.Primitivos;
using Dominio.Servicios;

namespace Aplicacion.ServiciosHotel.Eliminar
{
    public sealed class EliminarServicioCommandHandler : IRequestHandler<EliminarServicioCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioServicio _repositorioServicio;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarServicioCommandHandler(IRepositorioServicio repositorioServicio, IUnitOfWork unitOfWork)
        {
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarServicioCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioServicio.ListarPorId(new IdServicio(comando.Id)) is not Servicio servicio)
            {
                return Error.NotFound("Servicio.NoEncontrado", "No se encontro el servicio.");
            }

            _repositorioServicio.Eliminar(servicio);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
