namespace Convoquei.Core.Genericos.Entidades
{
    public abstract class EntidadeBase
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAlteracao { get; private set; }

        public EntidadeBase()
        {
            DataCriacao = DateTime.UtcNow;
            DataAlteracao = null;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not EntidadeBase outra) return false;
            if (ReferenceEquals(this, outra)) return true;
            if (GetType() != outra.GetType()) return false;

            return Id == outra.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntidadeBase? a, EntidadeBase? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntidadeBase? a, EntidadeBase? b) => !(a == b);
    }
}
