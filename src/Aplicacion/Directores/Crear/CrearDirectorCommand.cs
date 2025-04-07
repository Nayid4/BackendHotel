using Aplicacion.Directores.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Directores.Crear
{
    public record CrearDirectorCommand(string Nombre, string Apellido, ComandoPais Pais) : IRequest<ErrorOr<Unit>>;
}
