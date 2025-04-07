using Aplicacion.Actores.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Actores.Actualizar
{
    public record ActualizarActorCommand(Guid Id, string Nombre, string Apellido, ComandoPais Pais) : IRequest<ErrorOr<Unit>>;
}
