using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicios
{
    public sealed class Servicio : EntidadGenerica<IdServicio>
    {
        public string Nombre { get; private set; } = string.Empty;

        public Servicio()
        {
        }

        public Servicio(IdServicio id, string Nombre) : base(id)
        {
            Nombre = Nombre ?? throw new ArgumentNullException(nameof(Nombre));
        }

        public void Actualizar(string nombre)
        {
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            FechaDeActualizacion = DateTime.Now;
        }

    }
}
