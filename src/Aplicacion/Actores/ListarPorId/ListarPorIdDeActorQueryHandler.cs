using Aplicacion.Actores.Comun;
using Dominio.Actores;
using Dominio.Directores;
using Dominio.Paises;

namespace Aplicacion.Actores.ListarPorId
{
    public sealed class ListarPorIdDeActorQueryHandler : IRequestHandler<ListarPorIdDeActorQuery, ErrorOr<RespuestaActor>>
    {
        private readonly IRepositorioActor _repositorioActor;
        private readonly IRepositorioPais _repositorioPais;

        public ListarPorIdDeActorQueryHandler(IRepositorioActor repositorioActor, IRepositorioPais repositorioPais)
        {
            _repositorioActor = repositorioActor ?? throw new ArgumentNullException(nameof(repositorioActor));
            _repositorioPais = repositorioPais ?? throw new ArgumentNullException(nameof(repositorioPais));
        }

        public async Task<ErrorOr<RespuestaActor>> Handle(ListarPorIdDeActorQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioActor.ListarPorId(new IdActor(consulta.Id)) is not Actor actor)
            {
                return Error.NotFound("Actor.NoEncontrado", "No se encontro el actor.");
            }

            if (await _repositorioPais.ListarPorId(actor.IdPais) is not Pais pais)
            {
                return Error.NotFound("Pais.NoEncontrado", "No se encontro el pais.");
            }

            var respuesta = new RespuestaActor(
                actor.Id.Valor, 
                actor.Nombre, 
                actor.Apellido,
                new RespuestaPais(
                    actor.Pais!.Id.Valor,
                    actor.Pais!.Nombre
                )
            );

            return respuesta;
        }
    }
}
