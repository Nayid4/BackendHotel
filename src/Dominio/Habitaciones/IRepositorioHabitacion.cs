using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Habitaciones
{
    public interface IRepositorioHabitacion : IRepositorioGenerico<IdHabitacion, Habitacion>
    {
        Task<Habitacion?> ListarPorIdHabitacion(IdHabitacion idHabitacion);
        Task<Habitacion?> ListarPorNumeroDeHabitacion(string numero);
        Task<Habitacion?> ListarPorNombre(string nombre);
        IQueryable<Habitacion> ListarTodasLasHabitaciones();
    }
}
