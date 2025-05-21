using Convoquei.Core.Genericos.Repositorios.Consultas;

namespace Convoquei.DataTransfer.Genericos;

public class PaginacaoResponse<T>
{
    public IEnumerable<T> Dados { get; }
    public int Total { get; }
    public int Pagina { get; }
    public int TamanhoPagina { get; }
    
    public PaginacaoResponse(IEnumerable<T> dados, int total, int pagina, int tamanhoPagina)
    {
        Dados = dados;
        Total = total;
        Pagina = pagina;
        TamanhoPagina = tamanhoPagina;
    }
    
    public static explicit operator PaginacaoResponse<T>(PaginacaoConsulta<T> consulta)
    {
        return new PaginacaoResponse<T>(consulta.Dados, consulta.Total, consulta.Pagina, consulta.TamanhoPagina);
    }
}