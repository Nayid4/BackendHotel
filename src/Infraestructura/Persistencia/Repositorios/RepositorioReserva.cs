using Dominio.Resenas;
using Dominio.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Repositorios
{
    public sealed class RepositorioReserva : RepositorioGenerico<IdReserva, Reserva>, IRepositorioReserva
    {
        public RepositorioReserva(AplicacionContextoDb contexto) : base(contexto)
        {
        }

        public async Task<Reserva?> ListarPorIdReserva(IdReserva idReserva)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(r => r.Habitacion)
                .Include(r => r.Usuario)
                .Include(r => r.Contacto)
                .Include(r => r.FormaDePago)
                .FirstOrDefaultAsync(r => r.Id == idReserva);
        }

        public IQueryable<Reserva> ListarTodasLasReservas()
        {
            return _dbSet
                .AsNoTracking()
                .Include(r => r.Habitacion)
                .Include(r => r.Usuario)
                .Include(r => r.Contacto)
                .Include(r => r.FormaDePago);
        }
    }
}
