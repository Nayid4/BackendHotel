using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Habitaciones.Comun
{
    public record ComandoServicio(
        Guid Id
    );

    public record ComandoImagen(
        Guid Id
    );
}
