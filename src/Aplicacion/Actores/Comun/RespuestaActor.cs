using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Actores.Comun
{
    public record RespuestaActor(
        Guid Id,
        string Nombre,
        string Apellido,
        RespuestaPais Pais
    );

    public record RespuestaPais(
        Guid Id,
        string Nombre
    );
}
