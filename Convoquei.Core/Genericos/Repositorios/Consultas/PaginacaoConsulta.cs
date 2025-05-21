namespace Convoquei.Core.Genericos.Repositorios.Consultas;

public class PaginacaoConsulta<T>
{
    public IEnumerable<T> Dados { get; }
    public int Total { get; }
    public int Pagina { get; }
    public int TamanhoPagina { get; }
    
    public PaginacaoConsulta(IEnumerable<T> dados, int total, int pagina, int tamanhoPagina)
    {
        Dados = dados;
        Total = total;
        Pagina = pagina;
        TamanhoPagina = tamanhoPagina;
    }
}