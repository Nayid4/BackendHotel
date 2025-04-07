using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.comun.Dtos
{
    public record RespuestaDeArchivo(Stream Stream, string ContentType);
}
