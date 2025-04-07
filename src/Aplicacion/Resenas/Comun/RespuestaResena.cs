using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Resenas.Comun
{
    public record RespuestaResena(
        Guid Id,
        RespuestaHabitacion Habitacion,
        RespuestaUsuario Usuario,
        string Titulo,
        int Calificacion,
        string Descripcion,
        List<RespuestaImagen> Imagenes,
        DateTime FechaCreacion
    );

    public record RespuestaHabitacion(
        Guid Id,
        string Nombre
    );

    public record RespuestaUsuario(
        Guid Id,
        string Nombre
    );

    public record RespuestaImagen(
        Guid Id,
        string Url
    );
}
