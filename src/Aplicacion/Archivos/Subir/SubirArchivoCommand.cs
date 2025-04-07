using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Archivos.Subir
{
    public record SubirArchivoCommand(IFormFile File) : IRequest<ErrorOr<Guid>>;
}
