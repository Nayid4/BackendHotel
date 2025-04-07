using Dominio.Genericos;
using Dominio.Habitaciones;
using Dominio.ImagenesDeResenas;
using Dominio.Usuarios;

namespace Dominio.Resenas
{
    public sealed class Resena : EntidadGenerica<IdResena>
    {
        private readonly HashSet<ImagenDeResena> _imagenes = new();

        public IdHabitacion IdHabitacion { get; private set; } = default!;
        public IdUsuario IdUsuario { get; private set; } = default!;
        public string Titulo { get; private set; } = string.Empty;
        public int Calificacion { get; private set; } = 0;
        public string Descripcion { get; private set; } = string.Empty;
        public IReadOnlyCollection<ImagenDeResena> Imagenes => _imagenes.ToList();

        public Habitacion? Habitacion { get; private set; } = default!;
        public Usuario? Usuario { get; private set; } = default!;

        public Resena(IdResena idResena, IdHabitacion idHabitacion, IdUsuario idUsuario, string titulo, int calificacion, string descripcion)
            : base(idResena)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdUsuario = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Calificacion = calificacion;
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
        }

        public void Actualizar(IdHabitacion idHabitacion, IdUsuario idUsuario, string titulo, int calificacion, string descripcion)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdUsuario = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Calificacion = calificacion;
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            FechaDeActualizacion = DateTime.Now;
        }

        public void AgregarImagen(ImagenDeResena imagen)
        {
            _imagenes.Add(imagen);
            FechaDeActualizacion = DateTime.Now;
        }

        public void EliminarImagen(ImagenDeResena imagen)
        {
            _imagenes.Remove(imagen);
            FechaDeActualizacion = DateTime.Now;
        }

        public ImagenDeResena ObtenerImagenPorId(IdImagenDeResena idImagen)
        {
            return _imagenes.FirstOrDefault(imagen => imagen.Id.Equals(idImagen)) ?? throw new ArgumentNullException(nameof(idImagen));
        }

        public void LimpiarImagenes()
        {
            _imagenes.Clear();
            FechaDeActualizacion = DateTime.Now;
        }

        public void ActualizarImagenes(List<ImagenDeResena> nuevasImagenes)
        {
            _imagenes.Clear();
            _imagenes.UnionWith(nuevasImagenes);
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
