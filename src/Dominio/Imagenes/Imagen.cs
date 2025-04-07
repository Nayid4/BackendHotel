using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Imagenes
{
    public sealed class Imagen : EntidadGenerica<IdImagen>
    {

        public string Url { get; private set; } = string.Empty;

        public Imagen()
        {
        }

        public Imagen(IdImagen id, string url) : base(id)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
        }

        public void Actualizar(string url)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
