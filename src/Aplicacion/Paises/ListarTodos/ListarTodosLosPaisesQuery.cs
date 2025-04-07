
using Aplicacion.Paises.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Paises.ListarTodos
{
    public record ListarTodosLosPaisesQuery() : IRequest<ErrorOr<IReadOnlyList<RespuestaPais>>>;
}
