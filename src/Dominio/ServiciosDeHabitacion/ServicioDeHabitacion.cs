using Dominio.Genericos;
using Dominio.Habitaciones;
using Dominio.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ServiciosDeHabitacion
{
    public sealed class ServicioDeHabitacion : EntidadGenerica<IdServicioDeHabitacion>
    {
        public IdHabitacion IdHabitacion { get; private set; } = default!;
        public IdServicio IdServicio { get; private set; } = default!;

        public Servicio? Servicio { get; private set; } = default!;
        public Habitacion? Habitacion { get; private set; } = default!;

        public ServicioDeHabitacion() { }
        public ServicioDeHabitacion(IdServicioDeHabitacion id, IdHabitacion idHabitacion, IdServicio idServicio) : base(id)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdServicio = idServicio ?? throw new ArgumentNullException(nameof(idServicio));
        }

        public void Actualizar(IdHabitacion idHabitacion, IdServicio idServicio)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdServicio = idServicio ?? throw new ArgumentNullException(nameof(idServicio));
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
