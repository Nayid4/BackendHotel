
using Dominio.Habitaciones;
using Dominio.Imagenes;
using Dominio.ImagenesDeHabitaciones;
using Dominio.Primitivos;
using Dominio.Servicios;
using Dominio.ServiciosDeHabitacion;

namespace Aplicacion.Habitaciones.Crear
{
    public sealed class CrearHabitacionCommandHandler : IRequestHandler<CrearHabitacionCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioHabitacion _repositorioHabitacion;
        private readonly IRepositorioServicio _repositorioServicio;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositorioImagen _repositorioImagen;

        public CrearHabitacionCommandHandler(IRepositorioHabitacion repositorioHabitacion, IUnitOfWork unitOfWork, IRepositorioServicio repositorioServicio, IRepositorioImagen repositorioImagen)
        {
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioServicio = repositorioServicio ?? throw new ArgumentNullException(nameof(repositorioServicio));
            _repositorioImagen = repositorioImagen ?? throw new ArgumentNullException(nameof(repositorioImagen));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearHabitacionCommand comando, CancellationToken cancellationToken)
        {
            if (await _repositorioHabitacion.ListarPorNumeroDeHabitacion(comando.NumeroDeHabitacion) is Habitacion habitacion)
            {
                return Error.Conflict("Habitacion.Encontrada", "Ya existe una habitación con ese numero.");
            }

            if (await _repositorioHabitacion.ListarPorNombre(comando.Nombre) is Habitacion habitacion2)
            {
                return Error.Conflict("Habitacion.Encontrada", "Ya existe una habitación con ese nombre.");
            }

            var nuevaHabitacion = new Habitacion(
                new IdHabitacion(Guid.NewGuid()),
                comando.NumeroDeHabitacion,
                comando.Nombre,
                comando.Descripcion,
                comando.PrecioPorNoche,
                comando.Capacidad,
                comando.Estado
            );

            var listaDeServicios = new List<ServicioDeHabitacion>();

            foreach (var servicio in comando.Servicios)
            {
                if (await _repositorioServicio.ListarPorId(new IdServicio(servicio.Id)) is not Servicio servicio2)
                {
                    return Error.NotFound("Servicio.NoEncontrado", "No se encontro el servicio.");
                }

                var servicioDeHabitacion = new ServicioDeHabitacion(
                    new IdServicioDeHabitacion(Guid.NewGuid()),
                    nuevaHabitacion.Id,
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
                    nuevaHabitacion.Id,
                    imagenNueva.Id
                );

                listaDeImagenes.Add(imagenDeHabitacion);
                _repositorioImagen.Crear(imagenNueva);

            }

            nuevaHabitacion.ActualizarServicios(listaDeServicios);
            nuevaHabitacion.ActualizarImagenes(listaDeImagenes);

            _repositorioHabitacion.Crear(nuevaHabitacion);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
