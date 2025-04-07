using Dominio.Genericos;
using Infraestructura.Persistencia;
using Microsoft.AspNetCore.Authentication;

namespace Infraestructura.Persistencia.Repositorios
{
    public class RepositorioGenerico<TID, T> : IRepositorioGenerico<TID, T>
        where TID : IIdGenerico
        where T : EntidadGenerica<TID>
    {
        protected readonly AplicacionContextoDb _contexto;
        protected readonly DbSet<T> _dbSet;

        public RepositorioGenerico(AplicacionContextoDb contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _dbSet = _contexto.Set<T>();
        }

        public void Actualizar(T entidad) => _dbSet.Update(entidad);

        public async void Crear(T entidad) => await _dbSet.AddAsync(entidad);

        public void Eliminar(T id) => _dbSet.Remove(id);

        public async Task<T?> ListarPorId(TID id) => await _dbSet.FindAsync(id);

        public IQueryable<T> ListarTodos() => _dbSet.OrderBy(t => t.FechaDeCreacion);
    }
}
