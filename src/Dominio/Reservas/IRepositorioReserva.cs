using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Reservas
{
    public interface IRepositorioReserva : IRepositorioGenerico<IdReserva, Reserva>
    {
        Task<Reserva?> ListarPorIdReserva(IdReserva idReserva);
        IQueryable<Reserva> ListarTodasLasReservas();
    }
}
