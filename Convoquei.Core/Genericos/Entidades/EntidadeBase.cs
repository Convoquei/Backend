namespace Convoquei.Core.Genericos.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataAtualizacao { get; protected set; }

        protected EntidadeBase(Guid id, DateTime? criadoEm = null)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            DataCriacao = criadoEm ?? DateTime.UtcNow;
            DataAtualizacao = DataCriacao;
        }

        protected EntidadeBase() { }

        public override bool Equals(object? obj)
            => obj is EntidadeBase other && Id == other.Id;

        public override int GetHashCode() => Id.GetHashCode();
    }
}
