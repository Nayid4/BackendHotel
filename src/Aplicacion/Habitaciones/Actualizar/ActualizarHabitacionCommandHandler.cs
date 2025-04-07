
using Dominio.Habitaciones;
using Dominio.Imagenes;
using Dominio.ImagenesDeHabitaciones;
using Dominio.Primitivos;
using Dominio.Servicios;
using Dominio.ServiciosDeHabitacion;

namespace Aplicacion.Habitaciones.Actualizar
{
    public sealed class ActualizarHabitacionCommandHandler : IRequestHandler<ActualizarHabitacionCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioHabitacion _repositorioHabitacion;
        private readonly IRepositorioServicio _repositorioServicio;
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarHabitacionCommandHandler(IRepositorioHabitacion repositorioHabitacion, IRepositorioServicio repositorioServicio, IUnitOfWork unitOfWork)
        {
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<ErrorOr<Unit>> Handle(ActualizarHabitacionCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioHabitacion.ListarPorIdHabitacion(new IdHabitacion(comando.Id)) is not Habitacion habitacion)
            {
                return Error.NotFound("Habitacion.NoEncontrada", "No se econtró la habitacion.");
            }


            if (await _repositorioHabitacion.ListarPorNumeroDeHabitacion(comando.NumeroDeHabitacion) is Habitacion habitacion1 && !habitacion.NumeroDeHabitacion.Equals(habitacion1.NumeroDeHabitacion))
            {
                return Error.Conflict("Habitacion.Encontrada", "Ya existe una habitación con ese numero.");
            }

            if (await _repositorioHabitacion.ListarPorNombre(comando.Nombre) is Habitacion habitacion2 && !habitacion.Nombre.Equals(habitacion2.Nombre))
            {
                return Error.Conflict("Habitacion.Encontrada", "Ya existe una habitación con ese nombre.");
            }

            
            var listaDeServicios = new List<ServicioDeHabitacion>();

            foreach (var servicio in comando.Servicios)
            {
                if (await _repositorioServicio.ListarPorId(new IdServicio(servicio.Id)) is not Servicio servicio2)
                {
                    return Error.NotFound("Servicio.NoEncontrado", "No se encontro el servicio.");
                }

                var servicioDeHabitacion = new ServicioDeHabitacion(
                    new IdServicioDeHabitacion(Guid.NewGuid()),
                    habitacion.Id,
                    servicio2.Id
                );

                listaDeServicios.Add(servicioDeHabitacion);

            }

            var listaDeImagenes = new List<ImagenDeHabitacion>();

            foreach (var imagen in comando.Imagenes)
            {
                var imagenNueva = new Imagen(
                    new IdImagen(Guid.NewGuid()),
                    imagen.Url
                );

                var imagenDeHabitacion = new ImagenDeHabitacion(
                    new IdImagenDeHabitacion(Guid.NewGuid()),
                    habitacion.Id,
                    imagenNueva.Id  
                );

                listaDeImagenes.Add(imagenDeHabitacion);

            }

            habitacion.Actualizar(
                comando.NumeroDeHabitacion,
                comando.Nombre,
                comando.Descripcion,
                comando.PrecioPorNoche,
                comando.Capacidad,
                comando.Estado
            );

            habitacion.ActualizarServicios(listaDeServicios);
            habitacion.ActualizarImagenes(listaDeImagenes);


            _repositorioHabitacion.Actualizar(habitacion);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
