using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Paises.Comun
{
    public record RespuestaPais(
        Guid Id,
        string Nombre
    );
}
