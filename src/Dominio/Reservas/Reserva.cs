using Dominio.Contactos;
using Dominio.FormasDePagos;
using Dominio.Genericos;
using Dominio.Habitaciones;
using Dominio.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Reservas
{
    public sealed class Reserva : EntidadGenerica<IdReserva>
    {
        public IdUsuario IdUsuario { get; private set; } = default!;
        public IdHabitacion IdHabitacion { get; private set; } = default!;
        public DateTime FechaIngreso { get; private set; }
        public DateTime FechaSalida { get; private set; }
        public int CantidadAdultos { get; private set; }
        public int CantidadNinos { get; private set; }
        public IdContacto IdContacto { get; private set; } = default!;
        public IdFormaDePago IdFormaDePago { get; private set; } = default!;
        public double PrecioTotal => CalcularPrecioTotal();


        public Usuario? Usuario { get; private set; } = default!;
        public Habitacion? Habitacion { get; private set; } = default!;
        public Contacto? Contacto { get; private set; } = default!;
        public FormaDePago? FormaDePago { get; private set; } = default!;

        public Reserva() { }

        public Reserva(IdReserva id, IdUsuario idUsuario, IdHabitacion idHabitacion, DateTime fechaIngreso, DateTime fechaSalida, int cantidadAdultos, int cantidadNinos, IdContacto idContacto, IdFormaDePago idFormaDePago) 
            : base(id)
        {
            IdUsuario = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            FechaIngreso = fechaIngreso;
            FechaSalida = fechaSalida;
            CantidadAdultos = cantidadAdultos;
            CantidadNinos = cantidadNinos;
            IdContacto = idContacto ?? throw new ArgumentNullException(nameof(idContacto));
            IdFormaDePago = idFormaDePago ?? throw new ArgumentNullException(nameof(idFormaDePago));
        }

        public void Actualizar(IdUsuario idUsuario, IdHabitacion idHabitacion, DateTime fechaIngreso, DateTime fechaSalida, int cantidadAdultos, int cantidadNinos, IdContacto idContacto, IdFormaDePago idFormaDePago)
        {
            IdUsuario = idUsuario ?? throw new ArgumentNullException(nameof(idUsuario));
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            FechaIngreso = fechaIngreso;
            FechaSalida = fechaSalida;
            CantidadAdultos = cantidadAdultos;
            CantidadNinos = cantidadNinos;
            IdContacto = idContacto ?? throw new ArgumentNullException(nameof(idContacto));
            IdFormaDePago = idFormaDePago ?? throw new ArgumentNullException(nameof(idFormaDePago));
            FechaDeActualizacion = DateTime.Now;
        }

        public double CalcularPrecioTotal()
        {
            var diferenciaFechas = (FechaSalida - FechaIngreso).TotalDays;
            var precioHabitacion = Habitacion?.PrecioPorNoche ?? 0;

            var precioTotal = diferenciaFechas * precioHabitacion;

            return precioTotal;
        }
    }
}
