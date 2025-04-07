using Aplicacion.Reservas.Comun;
using Dominio.Reservas;

namespace Aplicacion.Reservas.ListarPorId
{
    public sealed class ListarPorIdDeReservaQueryHandler : IRequestHandler<ListarPorIdDeReservaQuery, ErrorOr<RespuestaReserva>>
    {
        private readonly IRepositorioReserva _repositorioReserva;

        public ListarPorIdDeReservaQueryHandler(IRepositorioReserva repositorioReserva)
        {
            _repositorioReserva = repositorioReserva ?? throw new ArgumentNullException(nameof(repositorioReserva));
        }

        public async Task<ErrorOr<RespuestaReserva>> Handle(ListarPorIdDeReservaQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioReserva.ListarPorId(new IdReserva(consulta.Id)) is not Reserva reserva)
            {
                return Error.NotFound("Reserva.NoEncontrado", "No se encontro el reserva.");
            }

            var respuesta = new RespuestaReserva(
                reserva.Id.Valor,
                new RespuestaUsuario(
                    reserva.Usuario!.Id.Valor,
                    reserva.Usuario.Nombre
                ),
                new RespuestaHabitacion(
                    reserva.Habitacion!.Id.Valor,
                    reserva.Habitacion.Nombre
                ),
                reserva.FechaIngreso,
                reserva.FechaSalida,
                reserva.CantidadAdultos,
                reserva.CantidadNinos,
                new RespuestaContacto(
                    reserva.Contacto!.Id.Valor,
                    reserva.Contacto!.Nombre,
                    reserva.Contacto.Apellido,
                    reserva.Contacto.Correo,
                    reserva.Contacto.Telefono
                ),
                new RespuestaFormaDePago(
                    reserva.FormaDePago!.Id.Valor,
                    reserva.FormaDePago.Titular,
                    reserva.FormaDePago.NumeroTarjeta,
                    reserva.FormaDePago.FechaDeVencimiento,
                    reserva.FormaDePago.Cvv
                ),
                reserva.PrecioTotal,
                reserva.FechaDeCreacion
            );

            return respuesta;
        }
    }
}
