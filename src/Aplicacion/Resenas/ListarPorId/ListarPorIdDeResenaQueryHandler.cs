using Aplicacion.Resenas.Comun;
using Dominio.Resenas;

namespace Aplicacion.Resenas.ListarPorId
{
    public sealed class ListarPorIdDeResenaQueryHandler : IRequestHandler<ListarPorIdDeResenaQuery, ErrorOr<RespuestaResena>>
    {
        private readonly IRepositorioResena _repositorioResena;

        public ListarPorIdDeResenaQueryHandler(IRepositorioResena repositorioResena)
        {
            _repositorioResena = repositorioResena ?? throw new ArgumentNullException(nameof(repositorioResena));
        }

        public async Task<ErrorOr<RespuestaResena>> Handle(ListarPorIdDeResenaQuery consulta, CancellationToken cancellationToken)
        {
            if (await _repositorioResena.ListarPorIdResena(new IdResena(consulta.Id)) is not Resena resena)
            {
                return Error.NotFound("Resena.NoEncontrado", "No se encontro el resena.");
            }


            var respuesta = new RespuestaResena(
                resena.Id.Valor,
                new RespuestaHabitacion(
                    resena.Habitacion!.Id.Valor,
                    resena.Habitacion.Nombre
                ),
                new RespuestaUsuario(
                    resena.Usuario!.Id.Valor,
                    resena.Usuario.Nombre
                ),
                resena.Titulo,
                resena.Calificacion,
                resena.Descripcion,
                resena.Imagenes.Select(imagen => new RespuestaImagen(
                    imagen.Imagen!.Id.Valor,
                    imagen.Imagen.Url
                )).ToList(),
                resena.FechaDeCreacion
            );

            return respuesta;
        }
    }
}
