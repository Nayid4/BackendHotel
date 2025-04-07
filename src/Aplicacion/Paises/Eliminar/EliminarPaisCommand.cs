using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Paises.Eliminar
{
    public record EliminarPaisCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
