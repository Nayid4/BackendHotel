﻿using Dominio.Genericos;
using Dominio.Imagenes;
using Dominio.Resenas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ImagenesDeResenas
{
    public sealed class ImagenDeResena : EntidadGenerica<IdImagenDeResena>
    {
        public IdResena IdResena { get; private set; } = default!;
        public IdImagen IdImagen { get; private set; } = default!;

        public Imagen? Imagen { get; private set; } = default!;

        public ImagenDeResena(IdImagenDeResena idImagenDeResena, IdResena idResena, IdImagen idImagen)
            : base(idImagenDeResena)
        {
            IdResena = idResena ?? throw new ArgumentNullException(nameof(idResena));
            IdImagen = idImagen ?? throw new ArgumentNullException(nameof(idImagen));
        }

        public void Actualizar(IdResena idResena, IdImagen idImagen)
        {
            IdResena = idResena ?? throw new ArgumentNullException(nameof(idResena));
            IdImagen = idImagen ?? throw new ArgumentNullException(nameof(idImagen));
            FechaDeActualizacion = DateTime.Now;
        }

    }
}
