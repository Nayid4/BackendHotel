using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Usuarios
{
    public interface IRepositorioUsuario : IRepositorioGenerico<IdUsuario, Usuario>
    {
        Task<Usuario?> IniciarSesion(string nombreDeUsuario, string contrasena);
        Task<Usuario?> ListarPorNombreDeUsuario(string nombreDeUsuario);
        Task<Usuario?> ListarPorCorreo(string correo);
        Task<Usuario?> ListarPorIdUsuario(IdUsuario id);
        IQueryable<Usuario> ListarTodosLosUsuarios();
    }
}
