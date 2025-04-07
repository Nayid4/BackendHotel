using Dominio.Habitaciones;
using Dominio.Primitivos;

namespace Aplicacion.Habitaciones.Eliminar
{
    public sealed class EliminarHabitacionCommandHandler : IRequestHandler<EliminarHabitacionCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioHabitacion _repositorioHabitacion;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarHabitacionCommandHandler(IRepositorioHabitacion repositorioHabitacion, IUnitOfWork unitOfWork)
        {
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<Unit>> Handle(EliminarHabitacionCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioHabitacion.ListarPorId(new IdHabitacion(comando.Id)) is not Habitacion habitacion)
            {
                return Error.NotFound("Habitacion.NoEncontrada", "No se econtró la habitacion.");
            }

            _repositorioHabitacion.Eliminar(habitacion);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
