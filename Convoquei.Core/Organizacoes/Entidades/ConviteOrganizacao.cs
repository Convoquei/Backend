using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores;
using Convoquei.Core.Organizacoes.Excecoes;
using Convoquei.Core.Usuarios.Entidades;
using Convoquei.Core.Usuarios.ValueObjects;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public class ConviteOrganizacao : EntidadeBase
    {
        private static int INTERVALO_MINUTOS_ENVIO_CONVITE = 300;

        public Email Email { get; private set; } = null!;
        public virtual Organizacao Organizacao { get; private set; }
        public DateTime DataExpiracao { get; private set; }
        public DateTime? UltimoReenvio { get; private set; }
        public virtual Usuario Convidador { get; private set; }
        public bool PermiteReenvio =>
            !UltimoReenvio.HasValue ||
            UltimoReenvio.Value.AddMinutes(INTERVALO_MINUTOS_ENVIO_CONVITE) < DateTime.UtcNow;

        public ConviteOrganizacao(Email email, Organizacao organizacao, DateTime dataExpiracao, Usuario convidador) : base()
        {
            Email = email;
            Organizacao = organizacao;
            DataExpiracao = dataExpiracao;
            Convidador = convidador;
        }

        protected ConviteOrganizacao()
        {
            
        }

        public void Reenviar(MembroOrganizacao membroReenviando)
        {
            membroReenviando.ValidarPermissoesAdministrativas();

            DateTime dataPermitidaReenvio = UltimoReenvio.HasValue ? UltimoReenvio.Value.AddMinutes(INTERVALO_MINUTOS_ENVIO_CONVITE) : DateTime.MinValue;

            if (dataPermitidaReenvio > DateTime.UtcNow)
                throw new RegraDeNegocioExcecao($"O convite poderá ser reenviado a partir de: {dataPermitidaReenvio:dd/MM/yyyy HH:mm}");

            UltimoReenvio = DateTime.UtcNow;
        }

        public MembroOrganizacao Aceitar(Usuario usuarioAceitando) 
        {
            if (!usuarioAceitando.Email.Endereco.Equals(Email.Endereco, StringComparison.OrdinalIgnoreCase))
                throw new RegraDeNegocioExcecao("O e-mail não é o mesmo do convite.");

            if (DataExpiracao < DateTime.UtcNow)
                throw new ConviteExpiradoExcecao($"O convite expirou em {DataExpiracao:dd/MM/yyyy HH:mm}");

            MembroOrganizacao membro = new(usuarioAceitando, Organizacao, CargoOrganizacaoEnum.Membro);
            return membro;
        }

        public void Negar(Usuario usuarioNegando)
        {
            if (!usuarioNegando.Email.Endereco.Equals(Email.Endereco, StringComparison.OrdinalIgnoreCase))
                throw new RegraDeNegocioExcecao("O e-mail não é o mesmo do convite.");
        }
    }
}
