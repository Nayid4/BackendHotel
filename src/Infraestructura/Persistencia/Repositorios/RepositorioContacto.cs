using Dominio.Contactos;
using Infraestructura.Persistencia.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Persistencia.Repositorios
{
    public class RepositorioContacto : RepositorioGenerico<IdContacto, Contacto>, IRepositorioContacto
    {
        public RepositorioContacto(AplicacionContextoDb contexto) : base(contexto)
        {
        }

    }
}
