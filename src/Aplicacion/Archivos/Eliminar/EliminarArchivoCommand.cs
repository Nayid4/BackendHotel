using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Archivos.Eliminar
{
    public record EliminarArchivoCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
