using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public class MembroOrganizacao : EntidadeBase
    {
        public virtual Usuario Usuario { get; private set; }
        public virtual Organizacao Organizacao { get; private set; }
        public CargoOrganizacaoEnum Cargo { get; private set; }

        public MembroOrganizacao(Usuario usuario, Organizacao organizacao, CargoOrganizacaoEnum cargo)
        {
            Usuario = usuario;
            Organizacao = organizacao;
            Cargo = cargo;
        }

        protected MembroOrganizacao()
        {
            
        }

        public void AlterarCargo(CargoOrganizacaoEnum cargo)
        {
            if(Cargo == CargoOrganizacaoEnum.Criador && cargo != CargoOrganizacaoEnum.Criador)
                throw new RegraDeNegocioExcecao("O criador não pode ter seu cargo alterado.");

            Cargo = cargo;
        }

        public bool PossuiPermissoesAdministrativas(Organizacao organizacao)
        {
            if (!Organizacao.Equals(organizacao))
                return false;

            return Cargo is CargoOrganizacaoEnum.Criador or CargoOrganizacaoEnum.Criador;
        }

        public void ValidarPermissoesAdministrativas()
        {
            if (!PossuiPermissoesAdministrativas(Organizacao))
                throw new RegraDeNegocioExcecao("É necessário possuir permissões administrativas na organização.");
        }
    }
}
