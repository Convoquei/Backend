namespace Convoquei.Core.Usuarios.Servicos.Interfaces
{
    public interface ICriptografiaServico
    {
        string Criptografar(string senha);
        bool Validar(string senhaHash, string senhaSemHash);
    }
}