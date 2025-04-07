using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicios
{
    public interface IRepositorioServicio : IRepositorioGenerico<IdServicio, Servicio>
    {
        Task<Servicio?> ListarPorNombre(string nombre);
    }
}
