using Aplicacion.Directores.Comun;
using Dominio.Directores;
using Dominio.Paises;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Directores.ListarTodos
{
    public sealed class ListarTodosLosDirectoresQueryHandler : IRequestHandler<ListarTodosLosDirectoresQuery, ErrorOr<IReadOnlyList<RespuestaDirector>>>
    {
        private readonly IRepositorioPais _repositorioPais;
        private readonly IRepositorioDirector _repositorioDirector;

        public ListarTodosLosDirectoresQueryHandler(IRepositorioPais repositorioPais, IRepositorioDirector repositorioDirector)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
            _repositorioDirector = repositorioDirector ?? throw new ArgumentNullException(nameof(repositorioDirector));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaDirector>>> Handle(ListarTodosLosDirectoresQuery request, CancellationToken cancellationToken)
        {
            var directores = await _repositorioDirector
                .ListarTodos()
                .Include(a => a.Pais)
                .ToListAsync(cancellationToken);


            var respuestaDirectores = directores.Select(director => new RespuestaDirector(
                    director.Id.Valor,
                    director.Nombre,
                    director.Apellido,
                    new RespuestaPais(
                        director.Pais!.Id.Valor,
                        director.Pais!.Nombre
                    )
            )).ToList();

            return respuestaDirectores;
        }
    }
}
