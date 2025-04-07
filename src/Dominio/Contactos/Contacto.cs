using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Contactos
{
    public sealed class Contacto : EntidadGenerica<IdContacto>
    {
        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string Correo { get; private set; } = string.Empty;
        public string Telefono { get; private set; } = string.Empty;
        public Contacto() { }
        public Contacto(IdContacto id, string nombre, string apellido, string correo, string telefono) : base(id)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            Telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
        }
        public void Actualizar(string nombre, string apellido, string correo, string telefono)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            Correo = correo ?? throw new ArgumentNullException(nameof(correo));
            Telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
