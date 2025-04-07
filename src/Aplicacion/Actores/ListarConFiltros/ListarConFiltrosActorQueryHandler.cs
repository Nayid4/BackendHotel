using Aplicacion.Actores.Comun;
using Aplicacion.comun.ListarDatos;
using Aplicacion.Paises.ListarConFiltros;
using Dominio.Actores;
using Dominio.Paises;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aplicacion.Actores.ListarConFiltros
{
    public sealed class ListarConFiltrosActorQueryHandler : IRequestHandler<ListarConFiltrosActorQuery, ErrorOr<ListaPaginada<RespuestaActor>>>
    {
        private readonly IRepositorioActor _repositorioActor;

        public ListarConFiltrosActorQueryHandler(IRepositorioActor repositorioActor)
        {
            _repositorioActor = repositorioActor ?? throw new ArgumentNullException(nameof(repositorioActor));
        }
        public async Task<ErrorOr<ListaPaginada<RespuestaActor>>> Handle(ListarConFiltrosActorQuery consulta, CancellationToken cancellationToken)
        {
            var actores = _repositorioActor.ListarTodosLosActores();

            if (!string.IsNullOrWhiteSpace(consulta.TerminoDeBusqueda))
            {
                actores = actores.Where(at => 
                    at.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Apellido.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Pais!.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower())
                );
            }

            if (consulta.OrdenarLista?.ToLower() == "desc")
            {
                actores = actores.OrderByDescending(ListarOrdenDePropiedad(consulta));
            } else
            {
                actores = actores.OrderBy(ListarOrdenDePropiedad(consulta));
            }



            var resultado = actores.Select(actor => new RespuestaActor(
                    actor.Id.Valor,
                    actor.Nombre,
                    actor.Apellido,
                    new RespuestaPais(
                        actor.Pais!.Id.Valor,
                        actor.Pais!.Nombre
                    )
            ));

            var listaDeActores = await ListaPaginada<RespuestaActor>.CrearAsync(
                    resultado,
                    consulta.Pagina,
                    consulta.TamanoPagina
                );

            return listaDeActores!;

        }

        private static Expression<Func<Actor, object>> ListarOrdenDePropiedad(ListarConFiltrosActorQuery consulta)
        {
            return consulta.OrdenarColumna?.ToLower() switch
            {
                "nombre" => actor => actor.Nombre,
                "apellido" => actor => actor.Apellido,
                "pais" => actor => actor.Pais!.Nombre,
                _ => actor => actor.Id
            };
        }

    }
}
