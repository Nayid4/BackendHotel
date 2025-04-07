using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Imagenes
{
    public interface IRepositorioImagen : IRepositorioGenerico<IdImagen, Imagen>
    {
    }
}
