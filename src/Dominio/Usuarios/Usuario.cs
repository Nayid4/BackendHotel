using Dominio.Genericos;
using Dominio.Resenas;
using Dominio.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Usuarios
{
    public sealed class Usuario : EntidadGenerica<IdUsuario>
    {
        private readonly HashSet<Reserva> _reservas = new();

        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string Rol { get; private set; } = string.Empty;
        public string NombreDeUsuario { get; private set; } = string.Empty;
        public string Correo { get; private set; } = string.Empty;
        public string Contrasena { get; private set; } = string.Empty;

        public IReadOnlyCollection<Reserva> Reservas => _reservas.ToList();

        public Usuario() { }

        public Usuario(IdUsuario id, string nombre, string apellido, string rol, string nombreDeUsuario, string correo, string contrasena) : base(id)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Rol = rol ?? throw new ArgumentNullException(nameof(rol));
            NombreDeUsuario = nombreDeUsuario ?? throw new ArgumentNullException(nameof(nombreDeUsuario));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            Contrasena = contrasena ?? throw new ArgumentNullException(nameof(contrasena));
        }

        public void Actualizar(string nombre, string apellido, string rol, string nombreDeUsuario, string correo, string contrasena)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Rol = rol ?? throw new ArgumentNullException(nameof(rol));
            NombreDeUsuario = nombreDeUsuario ?? throw new ArgumentNullException(nameof(nombreDeUsuario));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            Contrasena = contrasena ?? throw new ArgumentNullException(nameof(contrasena));
            FechaDeActualizacion = DateTime.Now;
        }

        public void AgregarReserva(Reserva reserva)
        {
            if (reserva == null) throw new ArgumentNullException(nameof(reserva));
            _reservas.Add(reserva);
        }

        public void EliminarReserva(Reserva reserva)
        {
            if (reserva == null) throw new ArgumentNullException(nameof(reserva));
            _reservas.Remove(reserva);
        }

        public void LimpiarReservas()
        {
            _reservas.Clear();
        }

        public void ActualizarReservas(List<Reserva> nuevasReservas)
        {
            _reservas.Clear();
            _reservas.UnionWith(nuevasReservas);
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
