using Dominio.Reservas;
using Dominio.Habitaciones;
using Dominio.Primitivos;
using Dominio.Usuarios;
using Dominio.Contactos;
using Dominio.FormasDePagos;

namespace Aplicacion.Reservas.Actualizar
{
    public sealed class ActualizarReservaCommandHandler : IRequestHandler<ActualizarReservaCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioReserva _repositorioReserva;
        private readonly IRepositorioHabitacion _repositorioHabitacion;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioContacto _repositorioContacto;
        private readonly IRepositorioFormaDePago _repositorioFormaDePago;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarReservaCommandHandler(IRepositorioReserva repositorioReserva, IUnitOfWork unitOfWork, IRepositorioHabitacion repositorioHabitacion, IRepositorioUsuario repositorioUsuario, IRepositorioContacto repositorioContacto, IRepositorioFormaDePago repositorioFormaDePago)
        {
            _repositorioReserva = repositorioReserva ?? throw new ArgumentNullException(nameof(repositorioReserva));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _repositorioContacto = repositorioContacto ?? throw new ArgumentNullException(nameof(repositorioContacto));
            _repositorioFormaDePago = repositorioFormaDePago ?? throw new ArgumentNullException(nameof(repositorioFormaDePago));
        }

        public async Task<ErrorOr<Unit>> Handle(ActualizarReservaCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioReserva.ListarPorId(new IdReserva(comando.Id)) is not Reserva reserva)
            {
                return Error.NotFound("Reserva.NoEncontrado", "No se encontro el reserva.");
            }

            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.Usuario.Id)) is not Usuario usuario)
            {
                return Error.Conflict("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            if (await _repositorioHabitacion.ListarPorId(new IdHabitacion(comando.Habitacion.Id)) is not Habitacion habitacion)
            {
                return Error.Conflict("Usuario.NoEncontrado", "No se encontro el usuario.");
            }


            reserva.Contacto!.Actualizar(
                comando.Contacto.Nombre,
                comando.Contacto.Apellido,
                comando.Contacto.Correo,
                comando.Contacto.Telefono
            );

            reserva.FormaDePago!.Actualizar(
                comando.FormaDePago.Titular,
                comando.FormaDePago.NumeroTarjeta,
                comando.FormaDePago.FechaDeVencimiento,
                comando.FormaDePago.Cvv
            );

            reserva.Actualizar(
                usuario.Id,
                habitacion.Id,
                comando.FechaIngreso,
                comando.FechaSalida,
                comando.CantidadAdultos,
                comando.CantidadNinos,
                reserva.Contacto.Id,
                reserva.FormaDePago.Id
            );

            _repositorioContacto.Actualizar(reserva.Contacto);
            _repositorioFormaDePago.Actualizar(reserva.FormaDePago);
            _repositorioReserva.Actualizar(reserva);


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
