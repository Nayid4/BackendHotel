using Dominio.Servicios;
using Infraestructura.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Repositorios
{
    public class RepositorioServicio : RepositorioGenerico<IdServicio, Servicio>, IRepositorioServicio
    {
        public RepositorioServicio(AplicacionContextoDb contexto) : base(contexto)
        {
        }

        public Task<Servicio?> ListarPorNombre(string nombre)
        {
            return _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Nombre.ToLower() == nombre.ToLower());
        }
    }
}
