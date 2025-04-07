using Aplicacion.Generos.Comun;
using Aplicacion.Paises.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Paises.ListarPorId
{
    public record ListarPorIdDePaisQuery(Guid Id) : IRequest<ErrorOr<RespuestaPais>>;
}
