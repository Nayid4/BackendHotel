using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Directores.Eliminar
{
    public record EliminarDirectorCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
