using Dominio.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.FormasDePagos
{
    public sealed class FormaDePago : EntidadGenerica<IdFormaDePago>
    {
        public string Titular { get; private set; } = string.Empty;
        public string NumeroTarjeta { get; private set; } = string.Empty;
        public DateTime FechaDeVencimiento { get; private set; }
        public string Cvv { get; private set; } = string.Empty;

        public FormaDePago(IdFormaDePago id, string titular, string numeroTarjeta, DateTime fechaDeVencimiento, string cvv)
            : base(id)
        {
            Titular = titular ?? throw new ArgumentNullException(nameof(titular));
            NumeroTarjeta = numeroTarjeta ?? throw new ArgumentNullException(nameof(numeroTarjeta));
            FechaDeVencimiento = fechaDeVencimiento;
            Cvv = cvv ?? throw new ArgumentNullException(nameof(cvv));
        }


        public void Actualizar(string titular, string numeroTarjeta, DateTime fechaDeVencimiento, string cvv)
        {
            Titular = titular ?? throw new ArgumentNullException(nameof(titular));
            NumeroTarjeta = numeroTarjeta ?? throw new ArgumentNullException(nameof(numeroTarjeta));
            FechaDeVencimiento = fechaDeVencimiento;
            Cvv = cvv ?? throw new ArgumentNullException(nameof(cvv));
            FechaDeActualizacion = DateTime.Now;
        }

    }
}
