using Aplicacion.Almacenamiento;
using Aplicacion.comun.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Archivos.Descargar
{
    public record DesargarArchivoQuery(Guid Id) : IRequest<ErrorOr<RespuestaDeArchivo>>;
}
