using Dominio.Resenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Repositorios
{
    public sealed class RepositorioResena : RepositorioGenerico<IdResena, Resena>, IRepositorioResena
    {
        public RepositorioResena(AplicacionContextoDb contexto) : base(contexto)
        {
        }

        public async Task<Resena?> ListarPorIdResena(IdResena idResena)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(r => r.Usuario)
                .Include(r => r.Habitacion)
                .Include(r => r.Imagenes)
                .FirstOrDefaultAsync(r => r.Id == idResena);
        }

        public IQueryable<Resena> ListarTodasLasResenas()
        {
            return _dbSet
                .AsNoTracking()
                .Include(r => r.Imagenes)
                .Include(r => r.Habitacion)
                .Include(r => r.Usuario);
        }
    }
}
