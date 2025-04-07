using Dominio.FormasDePagos;
using Infraestructura.Persistencia.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia.Repositorios
{
    public class RepositorioFormaDePago : RepositorioGenerico<IdFormaDePago, FormaDePago>, IRepositorioFormaDePago
    {
        public RepositorioFormaDePago(AplicacionContextoDb contexto) : base(contexto)
        {
        }
    }
}
