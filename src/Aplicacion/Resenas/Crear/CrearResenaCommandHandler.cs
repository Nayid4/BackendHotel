
using Dominio.Habitaciones;
using Dominio.Imagenes;
using Dominio.ImagenesDeResenas;
using Dominio.Primitivos;
using Dominio.Resenas;
using Dominio.Usuarios;

namespace Aplicacion.Resenas.Crear
{
    public sealed class CrearAutorCommandHandler : IRequestHandler<CrearResenaCommand, ErrorOr<Unit>>
    {
        private readonly IRepositorioResena _repositorioResena;
        private readonly IRepositorioHabitacion _repositorioHabitacion;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IUnitOfWork _unitOfWork;

        public CrearAutorCommandHandler(IRepositorioResena repositorioResena, IUnitOfWork unitOfWork, IRepositorioHabitacion repositorioHabitacion, IRepositorioUsuario repositorioUsuario)
        {
            _repositorioResena = repositorioResena ?? throw new ArgumentNullException(nameof(repositorioResena));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repositorioHabitacion = repositorioHabitacion ?? throw new ArgumentNullException(nameof(repositorioHabitacion));
            _repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<ErrorOr<Unit>> Handle(CrearResenaCommand comando, CancellationToken cancellationToken)
        {

            if (await _repositorioUsuario.ListarPorId(new IdUsuario(comando.Usuario.Id)) is not Usuario usuario)
            {
                return Error.Conflict("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            if (await _repositorioHabitacion.ListarPorId(new IdHabitacion(comando.Habitacion.Id)) is not Habitacion habitacion)
            {
                return Error.Conflict("Usuario.NoEncontrado", "No se encontro el usuario.");
            }

            var nuevaResena = new Resena(
                new IdResena(Guid.NewGuid()),
                habitacion.Id,
                usuario.Id,
                comando.Titulo,
                comando.Calificacion,
                comando.Descripcion
            );

            var listaDeImagenes = new List<ImagenDeResena>();

            foreach (var imagen in comando.Imagenes)
            {
                var imagenNueva = new Imagen(
                    new IdImagen(Guid.NewGuid()),
                    imagen.Url
                );

                var imagenDeHabitacion = new ImagenDeResena(
                    new IdImagenDeResena(Guid.NewGuid()),
                    nuevaResena.Id,
                    imagenNueva.Id
                );

                listaDeImagenes.Add(imagenDeHabitacion);

            }

            nuevaResena.ActualizarImagenes(listaDeImagenes);

            _repositorioResena.Crear(nuevaResena);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
