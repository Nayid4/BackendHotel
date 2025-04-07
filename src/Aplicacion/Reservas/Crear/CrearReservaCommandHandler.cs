
using Dominio.Reservas;
using Dominio.Primitivos;
using Dominio.Habitaciones;
using Dominio.Usuarios;
using Dominio.Contactos;
using Dominio.FormasDePagos;

namespace Aplicacion.Reservas.Crear
{
    public sealed class CrearReservaCommandHandler : IRequestHandler<CrearReservaCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioReserva _repositorioReserva;
        private readonly IRepositorioHabitacion _repositorioHabitacion;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioContacto _repositorioContacto;
        private readonly IRepositorioFormaDePago _repositorioFormaDePago;
        private readonly IUnitOfWork _unitOfWork;

        public CrearReservaCommandHandler(IRepositorioReserva repositorioReserva, IUnitOfWork unitOfWork, IRepositorioHabitacion repositorioHabitacion, IRepositorioUsuario repositorioUsuario, IRepositorioContacto repositorioContacto, IRepositorioFormaDePago repositorioFormaDePago)
        {
            _repositorioReserva = repositorioReserva ?? throw new ArgumentNullException(nameof(repositorioReserva));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
            _repositorioContacto = repositorioContacto ?? throw new ArgumentNullException(nameof(repositorioContacto));
            _repositorioFormaDePago = repositorioFormaDePago ?? throw new ArgumentNullException(nameof(repositorioFormaDePago));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearReservaCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.Usuario.Id)) is not Usuario usuario)
            {
                return Error.Conflict("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            if (await _repositorioHabitacion.ListarPorId(new IdHabitacion(comando.Habitacion.Id)) is not Habitacion habitacion)
            {
                return Error.Conflict("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            var contacto = new Contacto(
                new IdContacto(Guid.NewGuid()),
                comando.Contacto.Nombre,
                comando.Contacto.Apellido,
                comando.Contacto.Correo,
                comando.Contacto.Telefono
            );

            var formaDePago = new FormaDePago(
                new IdFormaDePago(Guid.NewGuid()),
                comando.FormaDePago.Titular,
                comando.FormaDePago.NumeroTarjeta,
                comando.FormaDePago.FechaDeVencimiento,
                comando.FormaDePago.Cvv
            );

            var nuedoReserva = new Reserva(
                new IdReserva(Guid.NewGuid()),
                usuario.Id,
                habitacion.Id,
                comando.FechaIngreso,
                comando.FechaSalida,
                comando.CantidadAdultos,
                comando.CantidadNinos,
                contacto.Id,
                formaDePago.Id
            );

            _repositorioContacto.Crear(contacto);
            _repositorioFormaDePago.Crear(formaDePago);
            _repositorioReserva.Crear(nuedoReserva);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
