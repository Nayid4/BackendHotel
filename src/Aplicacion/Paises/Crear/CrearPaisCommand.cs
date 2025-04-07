using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Paises.Crear
{
    public record CrearPaisCommand(string Nombre) : IRequest<ErrorOr<Unit>>;
}
