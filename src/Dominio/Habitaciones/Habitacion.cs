using Dominio.Genericos;
using Dominio.ImagenesDeHabitaciones;
using Dominio.ImagenesDeResenas;
using Dominio.Resenas;
using Dominio.ServiciosDeHabitacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Habitaciones
{
    public sealed class Habitacion : EntidadGenerica<IdHabitacion>
    {
        private readonly HashSet<ImagenDeHabitacion> _imagenesDeHabitacion = new();
        private readonly HashSet<ServicioDeHabitacion> _serviciosDeHabitacion = new();
        private readonly HashSet<Resena> _resenas = new();

        public string NumeroDeHabitacion { get; private set; } = string.Empty;
        public string Nombre { get; private set; } = string.Empty;
        public string Descripcion { get; private set; } = string.Empty;
        public double PrecioPorNoche { get; private set; }
        public int Capacidad { get; private set; }
        public IReadOnlyCollection<ImagenDeHabitacion> ImagenesDeHabitacion => _imagenesDeHabitacion.ToList();
        public IReadOnlyCollection<ServicioDeHabitacion> ServiciosDeHabitacion => _serviciosDeHabitacion.ToList();
        public IReadOnlyCollection<Resena> Resenas => _resenas.ToList();
        public string Estado { get; private set; } = string.Empty;

        public Habitacion() { }

        public Habitacion(IdHabitacion id, string numeroDeHabitacion, string nombre, string descripcion, double precioPorNoche, int capacidad, string estado) : base(id)
        {
            NumeroDeHabitacion = numeroDeHabitacion ?? throw new ArgumentNullException(nameof(numeroDeHabitacion));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            PrecioPorNoche = precioPorNoche;
            Capacidad = capacidad;
            Estado = estado ?? throw new ArgumentNullException(nameof(estado));
        }

        public void Actualizar(string numeroDeHabitacion, string nombre, string descripcion, double precioPorNoche, int capacidad, string estado)
        {
            NumeroDeHabitacion = numeroDeHabitacion ?? throw new ArgumentNullException(nameof(numeroDeHabitacion));
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
            PrecioPorNoche = precioPorNoche;
            Capacidad = capacidad;
            Estado = estado ?? throw new ArgumentNullException(nameof(estado));
            FechaDeActualizacion = DateTime.Now;
        }

        public void AgregarImagen(ImagenDeHabitacion imagenDeHabitacion)
        {
            if (imagenDeHabitacion is null)
            {
                throw new ArgumentNullException(nameof(imagenDeHabitacion));
            }
            _imagenesDeHabitacion.Add(imagenDeHabitacion);
        }

        public void AgregarServicio(ServicioDeHabitacion servicioDeHabitacion)
        {
            if (servicioDeHabitacion is null)
            {
                throw new ArgumentNullException(nameof(servicioDeHabitacion));
            }
            _serviciosDeHabitacion.Add(servicioDeHabitacion);
        }

        public void AgregarResena(Resena resena)
        {
            if (resena is null)
            {
                throw new ArgumentNullException(nameof(resena));
            }
            _resenas.Add(resena);
        }

        public void EliminarImagen(ImagenDeHabitacion imagenDeHabitacion)
        {
            if (imagenDeHabitacion is null)
            {
                throw new ArgumentNullException(nameof(imagenDeHabitacion));
            }
            _imagenesDeHabitacion.Remove(imagenDeHabitacion);
        }

        public void EliminarServicio(ServicioDeHabitacion servicioDeHabitacion)
        {
            if (servicioDeHabitacion is null)
            {
                throw new ArgumentNullException(nameof(servicioDeHabitacion));
            }
            _serviciosDeHabitacion.Remove(servicioDeHabitacion);
        }

        public void EliminarResena(Resena resena)
        {
            if (resena is null)
            {
                throw new ArgumentNullException(nameof(resena));
            }
            _resenas.Remove(resena);
        }

        public void CambiarEstado(string estado)
        {
            Estado = estado ?? throw new ArgumentNullException(nameof(estado));
            FechaDeActualizacion = DateTime.Now;
        }

        public void LimpiarImagenes()
        {
            _imagenesDeHabitacion.Clear();
            FechaDeActualizacion = DateTime.Now;
        }

        public void ActualizarImagenes(List<ImagenDeHabitacion> nuevasImagenes)
        {
            _imagenesDeHabitacion.Clear();
            _imagenesDeHabitacion.UnionWith(nuevasImagenes);
            FechaDeActualizacion = DateTime.Now;
        }

        public void LimpiarServicios()
        {
            _serviciosDeHabitacion.Clear();
            FechaDeActualizacion = DateTime.Now;
        }

        public void ActualizarServicios(List<ServicioDeHabitacion> nuevosServicios)
        {
            _serviciosDeHabitacion.Clear();
            _serviciosDeHabitacion.UnionWith(nuevosServicios);
            FechaDeActualizacion = DateTime.Now;
        }

        public void LimpiarResenas()
        {
            _resenas.Clear();
            FechaDeActualizacion = DateTime.Now;
        }

        public void ActualizarResenas(List<Resena> nuevasResenas)
        {
            _resenas.Clear();
            _resenas.UnionWith(nuevasResenas);
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
