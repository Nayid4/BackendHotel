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
        public string Numero { get; private set; } = string.Empty;
        public string FechaDeVencimiento { get; private set; } = string.Empty;
        public string Cvv { get; private set; } = string.Empty;

        public FormaDePago(IdFormaDePago idFormaDePago, string titular, string numero, string fechaDeVencimiento, string cvv)
            : base(idFormaDePago)
        {
            Titular = titular ?? throw new ArgumentNullException(nameof(titular));
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            FechaDeVencimiento = fechaDeVencimiento;
            Cvv = cvv ?? throw new ArgumentNullException(nameof(cvv));
        }

        public void Actualizar(string titular, string numero, string fechaDeVencimiento, string cvv)
        {
            Titular = titular ?? throw new ArgumentNullException(nameof(titular));
            Numero = numero ?? throw new ArgumentNullException(nameof(numero));
            FechaDeVencimiento = fechaDeVencimiento;
            Cvv = cvv ?? throw new ArgumentNullException(nameof(cvv));
            FechaDeActualizacion = DateTime.Now;
        }
    }
}
