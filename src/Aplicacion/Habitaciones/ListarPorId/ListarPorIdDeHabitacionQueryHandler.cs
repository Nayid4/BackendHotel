using Aplicacion.Habitaciones.Comun;
using Dominio.Habitaciones;
using Dominio.Primitivos;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Aplicacion.Habitaciones.ListarPorId
{
    public sealed class ListarPorIdDeHabitacionQueryHandler : IRequestHandler<ListarPorIdDeHabitacionQuery, ErrorOr<RespuestaHabitacion>>
    {
        private readonly IRepositorioHabitacion _repositorioHabitacion;

        public ListarPorIdDeHabitacionQueryHandler(IRepositorioHabitacion repositorioHabitacion)
        {
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
        }

        public async Task<ErrorOr<RespuestaHabitacion>> Handle(ListarPorIdDeHabitacionQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioHabitacion.ListarPorIdHabitacion(new IdHabitacion(consulta.Id)) is not Habitacion habitacion)
            {
                return Error.NotFound("Habitacion.NoEncontrada", "No se econtró la habitacion.");
            }

            var respuesta = new RespuestaHabitacion(
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
            );

            return respuesta;
        }
    }
}
