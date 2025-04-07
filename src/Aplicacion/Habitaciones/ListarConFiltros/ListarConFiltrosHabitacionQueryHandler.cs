
using Aplicacion.comun.ListarDatos;
using Aplicacion.Habitaciones.Comun;
using Dominio.Habitaciones;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aplicacion.Habitaciones.ListarConFiltros
{
    public sealed class ListarConFiltrosHabitacionQueryHandler : IRequestHandler<ListarConFiltrosHabitacionQuery, ErrorOr<ListaPaginada<RespuestaHabitacion>>>
    {
        private readonly IRepositorioHabitacion _repositorioHabitacion;

        public ListarConFiltrosHabitacionQueryHandler(IRepositorioHabitacion repositorioHabitacion)
        {
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
        }
        public async Task<ErrorOr<ListaPaginada<RespuestaHabitacion>>> Handle(ListarConFiltrosHabitacionQuery consulta, CancellationToken cancellationToken)
        {
            var habitaciones = _repositorioHabitacion.ListarTodasLasHabitaciones();

            if (!string.IsNullOrWhiteSpace(consulta.TerminoDeBusqueda))
            {
                habitaciones = habitaciones.Where(at => 
                    at.NumeroDeHabitacion.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Descripcion.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Capacidad.ToString().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.PrecioPorNoche.ToString().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Estado.Contains(consulta.TerminoDeBusqueda.ToLower())
                );
            }

            if (consulta.OrdenarLista?.ToLower() == "desc")
            {
                habitaciones = habitaciones.OrderByDescending(ListarOrdenDePropiedad(consulta));
            } else
            {
                habitaciones = habitaciones.OrderBy(ListarOrdenDePropiedad(consulta));
            }



            var resultado = habitaciones.Select(habitacion => new RespuestaHabitacion(
                        habitacion.Id.Valor,
                        habitacion.NumeroDeHabitacion,
                        habitacion.Nombre,
                        habitacion.Descripcion,
                        habitacion.PrecioPorNoche,
                        habitacion.Capacidad,
                        habitacion.ServiciosDeHabitacion.Select(servicio => new RespuestaServicio(
                            servicio.Servicio!.Id.Valor,
                            servicio.Servicio.Nombre
                        )).ToList(),
                        habitacion.ImagenesDeHabitacion.Select(imagen => new RespuestaImagen(
                            imagen.Imagen!.Id.Valor,
                            imagen.Imagen.Url
                        )).ToList(),
                        habitacion.Estado
                    )

                );

            var listaDeHabitaciones = await ListaPaginada<RespuestaHabitacion>.CrearAsync(
                    resultado,
                    consulta.Pagina,
                    consulta.TamanoPagina
                );

            return listaDeHabitaciones;

        }

        private static Expression<Func<Habitacion, object>> ListarOrdenDePropiedad(ListarConFiltrosHabitacionQuery consulta)
        {
            return consulta.OrdenarColumna?.ToLower() switch
            {
                "numeroDeHabitacion" => habitacion => habitacion.NumeroDeHabitacion,
                "nombre" => habitacion => habitacion.Nombre,
                "descripcion" => habitacion => habitacion.Descripcion,
                "capacidad" => habitacion => habitacion.Capacidad,
                "precioPorNoche" => habitacion => habitacion.PrecioPorNoche,
                "estado" => habitacion => habitacion.Estado,
                _ => habitacion => habitacion.Id
            };
        }

    }
}
