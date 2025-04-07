using Dominio.Habitaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Repositorios
{
    public sealed class RepositorioHabitacion : RepositorioGenerico<IdHabitacion, Habitacion>, IRepositorioHabitacion
    {
        public RepositorioHabitacion(AplicacionContextoDb contexto) : base(contexto)
        {
        }

        public async Task<Habitacion?> ListarPorIdHabitacion(IdHabitacion idHabitacion)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(h => h.ImagenesDeHabitacion)
                .Include(h => h.Resenas)
                .Include(h => h.ServiciosDeHabitacion)
                .FirstOrDefaultAsync(h => h.Id == idHabitacion);
        }

        public Task<Habitacion?> ListarPorNombre(string nombre)
        {
            return _dbSet
                .AsNoTracking()
                .Include(h => h.ImagenesDeHabitacion)
                .Include(h => h.Resenas)
                .Include(h => h.ServiciosDeHabitacion)
                .FirstOrDefaultAsync(h => h.Nombre == nombre);
        }

        public Task<Habitacion?> ListarPorNumeroDeHabitacion(string numero)
        {
            return _dbSet
                .AsNoTracking()
                .Include(h => h.ImagenesDeHabitacion)
                .Include(h => h.Resenas)
                .Include(h => h.ServiciosDeHabitacion)
                .FirstOrDefaultAsync(h => h.NumeroDeHabitacion == numero);
        }

        public IQueryable<Habitacion> ListarTodasLasHabitaciones()
        {
            return _dbSet
                .AsNoTracking()
                .Include(h => h.ImagenesDeHabitacion)
                .Include(h => h.Resenas)
                .Include(h => h.ServiciosDeHabitacion);
        }
    }
}
