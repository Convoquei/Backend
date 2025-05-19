using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Eventos.Entidades
{
    public sealed class ArquivoEvento
    {
        public Evento Evento { get; private set; }
        public string Nome { get; private set; }
        public string MimeType { get; private set; }
        public long TamanhoEmBytes { get; private set; }
        public string ChaveStorage { get; private set; }
        public Usuario Criador { get; private set; }

        public ArquivoEvento(Evento evento, string nome, string mimeType, long tamanhoEmBytes, string chaveStorage, Usuario criador)
        {
            Evento = evento;
            Nome = nome;
            MimeType = mimeType;
            TamanhoEmBytes = tamanhoEmBytes;
            ChaveStorage = chaveStorage;
            Criador = criador;
        }
    }
}
