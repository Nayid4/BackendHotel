using Aplicacion.Actores.Comun;
using Dominio.Actores;
using Dominio.Paises;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Actores.ListarTodos
{
    public sealed class ListarTodosLosActoresQueryHandler : IRequestHandler<ListarTodosLosActoresQuery, ErrorOr<IReadOnlyList<RespuestaActor>>>
    {
        private readonly IRepositorioPais _repositorioPais;
        private readonly IRepositorioActor _repositorioActor;

        public ListarTodosLosActoresQueryHandler(IRepositorioPais repositorioPais, IRepositorioActor repositorioActor)
        {
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
            _repositorioActor = repositorioActor ?? throw new ArgumentNullException(nameof(repositorioActor));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaActor>>> Handle(ListarTodosLosActoresQuery request, CancellationToken cancellationToken)
        {

            var actores = await _repositorioActor
                .ListarTodos()
                .Include(a => a.Pais) 
                .ToListAsync(cancellationToken);


            var respuestaActores = actores.Select(actor =>  new RespuestaActor(
                    actor.Id.Valor,
                    actor.Nombre,
                    actor.Apellido,
                    new RespuestaPais(
                        actor.Pais!.Id.Valor,
                        actor.Pais!.Nombre
                    )
            )).ToList();

            return respuestaActores;
        }

    }
}
