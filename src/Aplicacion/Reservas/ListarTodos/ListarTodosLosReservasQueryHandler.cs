using Aplicacion.Reservas.Comun;
using Dominio.Resenas;
using Dominio.Reservas;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Reservas.ListarTodos
{
    public sealed class ListarTodosLosReservasQueryHandler : IRequestHandler<ListarTodosLasReservasQuery, ErrorOr<IReadOnlyList<RespuestaReserva>>>
    {
        private readonly IRepositorioReserva _repositorioReserva;

        public ListarTodosLosReservasQueryHandler(IRepositorioReserva repositorioReserva)
        {
            _repositorioReserva = repositorioReserva ?? throw new ArgumentNullException(nameof(repositorioReserva));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaReserva>>> Handle(ListarTodosLasReservasQuery request, CancellationToken cancellationToken)
        {
            var reservas = await _repositorioReserva
                .ListarTodasLasReservas()
                .Select(reserva => new RespuestaReserva(
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
                ))
                .ToListAsync(cancellationToken);



            return reservas;
        }
    }
}
