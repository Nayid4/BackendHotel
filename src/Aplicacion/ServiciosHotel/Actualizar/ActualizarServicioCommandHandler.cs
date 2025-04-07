
using Dominio.Primitivos;
using Dominio.Servicios;

namespace Aplicacion.ServiciosHotel.Actualizar
{
    public sealed class ActualizarServicioCommandHandler : IRequestHandler<ActualizarServicioCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioServicio _repositorioServicio;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarServicioCommandHandler(IRepositorioServicio repositorioServicio, IUnitOfWork unitOfWork)
        {
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarServicioCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioServicio.ListarPorId(new IdServicio(comando.Id)) is not Servicio servicio)
            {
                return Error.NotFound("Servicio.NoEncontrado", "No se encontro el servicio.");
            }

            if (await _repositorioServicio.ListarPorNombre(comando.Nombre) is Servicio servicio2 && !servicio.Nombre.Equals(servicio2.Nombre))
            {
                return Error.Conflict("Servicio.Encontrado", "Ya existe un servicio con ese nombre.");
            }

            servicio.Actualizar(comando.Nombre);

            _repositorioServicio.Actualizar(servicio);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
