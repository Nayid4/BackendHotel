﻿using Dominio.Genericos;
using Dominio.Habitaciones;
using Dominio.Imagenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ImagenesDeHabitaciones
{
    public sealed class ImagenDeHabitacion : EntidadGenerica<IdImagenDeHabitacion>
    {
        public IdHabitacion IdHabitacion { get; private set; } = default!;
        public IdImagen IdImagen { get; private set; } = default!;

        public Imagen? Imagen { get; private set; } = default!;

        public ImagenDeHabitacion()
        {
        }

        public ImagenDeHabitacion(IdImagenDeHabitacion id, IdHabitacion idHabitacion, IdImagen idImagen) : base(id)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdImagen = idImagen ?? throw new ArgumentNullException(nameof(idImagen));
        }

        public void Actualizar(IdHabitacion idHabitacion, IdImagen idImagen)
        {
            IdHabitacion = idHabitacion ?? throw new ArgumentNullException(nameof(idHabitacion));
            IdImagen = idImagen ?? throw new ArgumentNullException(nameof(idImagen));
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
