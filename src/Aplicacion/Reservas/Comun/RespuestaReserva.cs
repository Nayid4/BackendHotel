
namespace Aplicacion.Reservas.Comun
{
    public record RespuestaReserva(
        Guid Id,
        RespuestaUsuario Usuario,
        RespuestaHabitacion Habitacion,
        DateTime FechaIngreso,
        DateTime FechaSalida,
        int CantidadAdultos,
        int CantidadNinos,
        RespuestaContacto Contacto,
        RespuestaFormaDePago FormaDePago,
        double PrecioTotal,
        DateTime FechaCreacion
    );

    public record RespuestaUsuario(
        Guid Id,
        string Nombre
    );

    public record RespuestaHabitacion(
        Guid Id,
        string Nombre
    );

    public record RespuestaContacto(
        Guid Id,
        string Nombre,
        string Apellido,
        string Correo,
        string Telefono
    );

    public record RespuestaFormaDePago(
        Guid Id,
        string Titular,
        string NumeroTarjeta,
        DateTime FechaDeVencimiento,
        string Cvv
    );
}
