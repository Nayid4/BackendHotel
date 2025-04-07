using Aplicacion.Generos.Comun;
using Aplicacion.Paises.Comun;
using Dominio.Paises;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Paises.ListarTodos
{
    public sealed class ListarTodosLosPaisesQueryHandler : IRequestHandler<ListarTodosLosPaisesQuery, ErrorOr<IReadOnlyList<RespuestaPais>>>
    {
        private readonly IRepositorioPais _repositorioPais;

        public ListarTodosLosPaisesQueryHandler(IRepositorioPais repositorioPais)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaPais>>> Handle(ListarTodosLosPaisesQuery request, CancellationToken cancellationToken)
        {
            var paises = await _repositorioPais.ListarTodos()
                .Select(ge => 
                new RespuestaPais(
                    ge.Id.Valor, 
                    ge.Nombre
                ))
                .ToListAsync(cancellationToken);

            return paises;
        }
    }
}
