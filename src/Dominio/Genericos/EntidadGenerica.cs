using Dominio.Primitivos;

namespace Dominio.Genericos
{
    public abstract class EntidadGenerica<TID> : AggregateRoot
        where TID : IIdGenerico
    {
        
        public TID Id { get; protected set; } = default!;
        public DateTime FechaDeCreacion { get; protected set; }
        public DateTime FechaDeActualizacion { get; protected set; }

        public EntidadGenerica()
        {
        }

        public EntidadGenerica(TID id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            FechaDeCreacion = DateTime.Now;
            FechaDeActualizacion = DateTime.Now;
        }

        
    }
}
