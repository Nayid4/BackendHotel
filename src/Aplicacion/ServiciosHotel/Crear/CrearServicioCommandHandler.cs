
using Dominio.Primitivos;
using Dominio.Servicios;

namespace Aplicacion.ServiciosHotel.Crear
{
    public sealed class CrearServicioCommandHandler : IRequestHandler<CrearServicioCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioServicio _repositorioServicio;
        private readonly IUnitOfWork _unitOfWork;

        public CrearServicioCommandHandler(IRepositorioServicio repositorioServicio, IUnitOfWork unitOfWork)
        {
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearServicioCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioServicio.ListarPorNombre(comando.Nombre) is Servicio servicio)
            {
                return Error.Conflict("Servicio.Encontrado","Ya existe un servicio con ese nombre.");
            }

            var nuevoServicio = new Servicio(
                new IdServicio(Guid.NewGuid()),
                comando.Nombre
            );

            _repositorioServicio.Crear(nuevoServicio);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
