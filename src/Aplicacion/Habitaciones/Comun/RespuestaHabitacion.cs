
namespace Aplicacion.Habitaciones.Comun
{
    public record RespuestaHabitacion(
        Guid Id,
        string NumeroDeHabitacion,
        string Nombre,
        string Descripcion,
        double PrecioPorNoche,
        int Capacidad,
        List<RespuestaServicio> Servicios,
        List<RespuestaImagen> Imagenes,
        string Estado
    );

    public record RespuestaServicio(
        Guid Id,
        string Nombre
    );

    public record RespuestaImagen(
        Guid Id,
        string Url
    );


}
