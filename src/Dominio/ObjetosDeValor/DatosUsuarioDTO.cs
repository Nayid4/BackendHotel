using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ObjetosDeValor
{
    public record DatosUsuarioDTO(Guid Id, string Nombre, string Rol, string NombreDeUsuario);
}
