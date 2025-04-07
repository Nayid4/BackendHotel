using Aplicacion.Habitaciones.Comun;
using Dominio.Habitaciones;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Habitaciones.ListarTodos
{
    public sealed class ListarTodosLasHabitacionesQueryHandler : IRequestHandler<ListarTodosLasHabitacionesQuery, ErrorOr<IReadOnlyList<RespuestaHabitacion>>>
    {
        private readonly IRepositorioHabitacion _repositorioHabitacion;

        public ListarTodosLasHabitacionesQueryHandler(IRepositorioHabitacion repositorioHabitacion)
        {
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaHabitacion>>> Handle(ListarTodosLasHabitacionesQuery request, CancellationToken cancellationToken)
        {
            var peliculas = await _repositorioHabitacion
                .ListarTodasLasHabitaciones()
                .Select(habitacion => new RespuestaHabitacion(
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

                ).ToListAsync(cancellationToken);


            return peliculas;
        }

    }
}
