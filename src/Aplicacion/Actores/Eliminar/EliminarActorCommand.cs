using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Actores.Eliminar
{
    public record EliminarActorCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
