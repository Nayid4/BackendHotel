using Aplicacion.Resenas.Comun;
using Dominio.Resenas;
using Dominio.Reservas;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Resenas.ListarTodos
{
    public sealed class ListarTodosLosResenaQueryHandler : IRequestHandler<ListarTodosLosResenaQuery, ErrorOr<IReadOnlyList<RespuestaResena>>>
    {
        private readonly IRepositorioResena _repositorioResena;

        public ListarTodosLosResenaQueryHandler(IRepositorioResena repositorioResena)
        {
            _repositorioResena = repositorioResena ?? throw new ArgumentNullException(nameof(repositorioResena));
        }

        public async Task<ErrorOr<IReadOnlyList<RespuestaResena>>> Handle(ListarTodosLosResenaQuery request, CancellationToken cancellationToken)
        {

            var resenas = await _repositorioResena
                .ListarTodasLasResenas()
                .ToListAsync(cancellationToken);


            var respuestaResenas = resenas.Select(resena => new RespuestaResena(
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
                )).ToList();

            return respuestaResenas;
        }

    }
}
