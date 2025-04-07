using Aplicacion.Actores.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Actores.Crear
{
    public record CrearActorCommand(string Nombre, string Apellido, ComandoPais Pais) : IRequest<ErrorOr<Unit>>;
}
