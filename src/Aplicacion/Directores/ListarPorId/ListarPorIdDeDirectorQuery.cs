using Aplicacion.Directores.Comun;

namespace Aplicacion.Directores.ListarPorId
{
    public record ListarPorIdDeDirectorQuery(Guid Id) : IRequest<ErrorOr<RespuestaDirector>>;
}
