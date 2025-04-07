
using Dominio.Reservas;
using Dominio.Primitivos;

namespace Aplicacion.Reservas.Eliminar
{
    public sealed class EliminarReservaCommandHandler : IRequestHandler<EliminarReservaCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioReserva _repositorioReserva;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarReservaCommandHandler(IRepositorioReserva repositorioReserva, IUnitOfWork unitOfWork)
        {
            _repositorioReserva = repositorioReserva ?? throw new ArgumentNullException(nameof(repositorioReserva));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarReservaCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioReserva.ListarPorId(new IdReserva(comando.Id)) is not Reserva reserva)
            {
                return Error.NotFound("Reserva.NoEncontrado", "No se encontro el reserva.");
            }

            _repositorioReserva.Eliminar(reserva);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
