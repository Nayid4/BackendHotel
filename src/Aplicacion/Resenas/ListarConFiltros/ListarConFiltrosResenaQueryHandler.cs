using Aplicacion.Resenas.Comun;
using Aplicacion.comun.ListarDatos;
using System.Linq.Expressions;
using Dominio.Resenas;

namespace Aplicacion.Resenas.ListarConFiltros
{
    public sealed class ListarConFiltrosResenaQueryHandler : IRequestHandler<ListarConFiltrosResenaQuery, ErrorOr<ListaPaginada<RespuestaResena>>>
    {
        private readonly IRepositorioResena _repositorioResena;

        public ListarConFiltrosResenaQueryHandler(IRepositorioResena repositorioResena)
        {
            _repositorioResena = repositorioResena ?? throw new ArgumentNullException(nameof(repositorioResena));
        }
        public async Task<ErrorOr<ListaPaginada<RespuestaResena>>> Handle(ListarConFiltrosResenaQuery consulta, CancellationToken cancellationToken)
        {
            var resenas = _repositorioResena.ListarTodasLasResenas();

            if (!string.IsNullOrWhiteSpace(consulta.TerminoDeBusqueda))
            {
                resenas = resenas.Where(at => 
                    at.Titulo.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Habitacion!.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Usuario!.NombreDeUsuario.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Usuario!.Nombre.ToLower().Contains(consulta.TerminoDeBusqueda.ToLower()) ||
                    at.Calificacion.ToString().ToLower().Contains(consulta.TerminoDeBusqueda.ToLower())
                );
            }

            if (consulta.OrdenarLista?.ToLower() == "desc")
            {
                resenas = resenas.OrderByDescending(ListarOrdenDePropiedad(consulta));
            } else
            {
                resenas = resenas.OrderBy(ListarOrdenDePropiedad(consulta));
            }



            var resultado = resenas.Select(resena => new RespuestaResena(
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
                ));

            var listaDeActores = await ListaPaginada<RespuestaResena>.CrearAsync(
                    resultado,
                    consulta.Pagina,
                    consulta.TamanoPagina
                );

            return listaDeActores!;

        }

        private static Expression<Func<Resena, object>> ListarOrdenDePropiedad(ListarConFiltrosResenaQuery consulta)
        {
            return consulta.OrdenarColumna?.ToLower() switch
            {
                "titulo" => resena => resena.Titulo,
                "habitacion" => resena => resena.Habitacion!.Nombre,
                "nombreDeUsuario" => resena => resena.Usuario!.NombreDeUsuario,
                "usuario" => resena => resena.Usuario!.Nombre,
                "calificaion" => resena => resena.Calificacion,
                _ => resena => resena.Id
            };
        }

    }
}
