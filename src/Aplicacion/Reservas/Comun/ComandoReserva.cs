
namespace Aplicacion.Reservas.Comun
{
    public record ComandoUsuario(
        Guid Id
    );

    public record ComandoHabitacion(
        Guid Id
    );

    public record ComandoContacto(
        string Nombre,
        string Apellido,
        string Correo,
        string Telefono
    );

    public record ComandoFormaDePago(
        string Titular,
        string NumeroTarjeta,
        DateTime FechaDeVencimiento,
        string Cvv
    );
}
