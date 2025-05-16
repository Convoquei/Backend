namespace Convoquei.Core.Eventos.Enumeradores
{
    [Flags]
    public enum DiasDaSemanaEnum
    {
        Nenhum = 0,
        Domingo = 1 << 0,
        Segunda = 1 << 1,
        Terca = 1 << 2,
        Quarta = 1 << 3,
        Quinta = 1 << 4,
        Sexta = 1 << 5,
        Sabado = 1 << 6
    }
}
