using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.comun.ListarDatos
{
    public class ListaPaginada<T>
    {
        public List<T> Elementos { get; }
        public int Pagina { get; }
        public int TamanoPagina { get; }
        public int CantidadTotal { get; }
        public bool TieneSiguientePagina => (Pagina * TamanoPagina) < CantidadTotal;
        public bool TienePaginaAnterior => Pagina > 1;

        public ListaPaginada(List<T> elementos, int pagina, int tamanoPagina, int cantidadTotal)
        {
            Elementos = elementos;
            Pagina = pagina;
            TamanoPagina = tamanoPagina;
            CantidadTotal = cantidadTotal;
        }

        public static async Task<ListaPaginada<T>> CrearAsync(IQueryable<T> consulta, int pagina, int tamanoPagina)
        {
            var cantidadTotal = await consulta.CountAsync();
            var elementos = await consulta.Skip((pagina - 1) * tamanoPagina).ToListAsync();

            return new(elementos, pagina, tamanoPagina, cantidadTotal);
        }
    }
}
