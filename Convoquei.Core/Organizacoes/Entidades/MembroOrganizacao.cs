using Convoquei.Core.Genericos.Entidades;
using Convoquei.Core.Genericos.Excecoes;
using Convoquei.Core.Organizacoes.Enumeradores.CargoMembroEnum;
using Convoquei.Core.Usuarios.Entidades;

namespace Convoquei.Core.Organizacoes.Entidades
{
    public sealed class MembroOrganizacao : EntidadeBase, IEquatable<MembroOrganizacao>
    {
        public Usuario Usuario { get; private set; }
        public Organizacao Organizacao { get; private set; }
        public CargoMembroEnum Cargo { get; private set; }

        public MembroOrganizacao(Usuario usuario, Organizacao organizacao, CargoMembroEnum cargo) : base(Guid.NewGuid())
        {
            Usuario = usuario;
            Organizacao = organizacao;
            Cargo = cargo;
        }

        public bool PossuiPermissaoAdministrativa()
        {
            return Cargo == CargoMembroEnum.Administrador ||
                   Cargo == CargoMembroEnum.Criador;
        }

        public void ValidarPermissaoAdministrativa(Organizacao organizacao)
        {
            if (Organizacao.Id != organizacao.Id)
                throw new RegraDeNegocioExcecao("Você não pertence a esta organização.");

            if (!PossuiPermissaoAdministrativa())
                throw new RegraDeNegocioExcecao("Você não possui permissão administrativa.");
        }

        public bool Equals(MembroOrganizacao? outroMembro)
        {
            if (outroMembro is null)
                return false;

            return Usuario.Id == outroMembro.Usuario.Id &&
                Organizacao.Id == outroMembro.Organizacao.Id;
        }

        public override bool Equals(object? obj) 
            => Equals(obj as MembroOrganizacao);

        public override int GetHashCode()
            => HashCode.Combine(Usuario.Id, Organizacao.Id);

        public static implicit operator string(MembroOrganizacao membro)
            => membro.Usuario.Nome;
    }
}
