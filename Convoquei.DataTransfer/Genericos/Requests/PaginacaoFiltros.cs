namespace Convoquei.DataTransfer.Genericos.Requests;

public class PaginacaoFiltros
{
    public int Pagina { get; set; } = 1;
    public int TamanhoPagina { get; set; } = 10;
    public string? Ordem { get; set; } = null;
    public bool OrdemCrescente { get; set; } = true;
}