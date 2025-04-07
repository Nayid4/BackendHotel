using Dominio.Usuarios;
using Infraestructura.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Repositorios
{
    public class RepositorioUsuario : RepositorioGenerico<IdUsuario, Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(AplicacionContextoDb contexto) : base(contexto)
        {
        }

        public async Task<Usuario?> IniciarSesion(string nombreDeUsuario, string contrasena)
        {
            return await _dbSet
                .Include(u => u.Reservas)
                .Include(u => u.Resenas)
                .FirstOrDefaultAsync(u => u.NombreDeUsuario == nombreDeUsuario && u.Contrasena == contrasena);
        }

        public async Task<Usuario?> ListarPorCorreo(string correo)
        {
            return await _dbSet
                .Include(u => u.Reservas)
                .Include(u => u.Resenas)
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario?> ListarPorIdUsuario(IdUsuario id)
        {
            return await _dbSet
                .Include(u => u.Reservas)
                .Include(u => u.Resenas)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> ListarPorNombreDeUsuario(string nombreDeUsuario)
        {
            return await _dbSet
                .Include(u => u.Reservas)
                .Include(u => u.Resenas)
                .FirstOrDefaultAsync(u => u.NombreDeUsuario == nombreDeUsuario);
        }

        public IQueryable<Usuario> ListarTodosLosUsuarios()
        {
            return _dbSet
                .Include(u => u.Resenas)
                .Include(u => u.Reservas);
        }
    }
}
