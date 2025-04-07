using Dominio.Imagenes;
using Infraestructura.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Repositorios
{
    public class RepositorioImagen : RepositorioGenerico<IdImagen, Imagen>, IRepositorioImagen
    {
        public RepositorioImagen(AplicacionContextoDb contexto) : base(contexto)
        {
        }

    }
}
