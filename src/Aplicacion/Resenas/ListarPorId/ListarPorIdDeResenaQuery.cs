using Aplicacion.Resenas.Comun;

namespace Aplicacion.Resenas.ListarPorId
{
    public record ListarPorIdDeResenaQuery(Guid Id) : IRequest<ErrorOr<RespuestaResena>>;
}
