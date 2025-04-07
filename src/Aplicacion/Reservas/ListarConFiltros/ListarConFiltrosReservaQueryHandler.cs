
using Aplicacion.comun.ListarDatos;
using Aplicacion.Reservas.Comun;
using Dominio.Reservas;
using System.Linq.Expressions;

namespace Aplicacion.Reservas.ListarConFiltros
{
    public sealed class ListarConFiltrosReservaQueryHandler : IRequestHandler<ListarConFiltrosReservaQuery, ErrorOr<ListaPaginada<RespuestaReserva>>>
    {
        private readonly IRepositorioReserva _repositorioReserva;

        public ListarConFiltrosReservaQueryHandler(IRepositorioReserva repositorioReserva)
        {
            _repositorioReserva = repositorioReserva ?? throw new ArgumentNullException(nameof(repositorioReserva));
        }
        public async Task<ErrorOr<ListaPaginada<RespuestaReserva>>> Handle(ListarConFiltrosReservaQuery consulta, CancellationToken cancellationToken)
        {
            var reservas = _repositorioReserva.ListarTodasLasReservas();

            if (!string.IsNullOrWhiteSpace(consulta.TerminoDeBusqueda))
            {
                reservas = reservas.Where(at =>
                    at.Usuario!.NombreDeUsuario.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Usuario!.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Habitacion!.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.CantidadAdultos.ToString().ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.CantidadNinos.ToString().ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.FechaIngreso.ToShortDateString().ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.FechaSalida.ToShortDateString().ToLower().Contains(consulta.TerminoDeBusqueda.ToLower())
                );
            }

            if (consulta.OrdenarLista?.ToLower() == "desc")
            {
                reservas = reservas.OrderByDescending(ListarOrdenDePropiedad(consulta));
            } else
            {
                reservas = reservas.OrderBy(ListarOrdenDePropiedad(consulta));
            }



            var resultado = reservas.Select(reserva => new RespuestaReserva(
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
            ));

            var listaDeReservas = await ListaPaginada<RespuestaReserva>.CrearAsync(
                    resultado,
                    consulta.Pagina,
                    consulta.TamanoPagina
                );

            return listaDeReservas!;

        }

        private static Expression<Func<Reserva, object>> ListarOrdenDePropiedad(ListarConFiltrosReservaQuery consulta)
        {
            return consulta.OrdenarColumna?.ToLower() switch
            {
                "usuario" => reserva => reserva.Usuario!.Nombre,
                "nombreDeUsuario" => reserva => reserva.Usuario!.NombreDeUsuario,
                "habitacion" => reserva => reserva.Habitacion!.Nombre,
                "cantidadAdultos" => reserva => reserva.CantidadAdultos,
                "cantidadNinos" => reserva => reserva.CantidadNinos,
                "fechaIngreso" => reserva => reserva.FechaIngreso,
                "fechaSalida" => reserva => reserva.FechaSalida,
                _ => actor => actor.Id
            };
        }

    }
}
