using ControleDeBar.Dominio.Compartilhado.Identity;

namespace ControleDeBar.Testes.Integracao.Identity;

public sealed class ProvedorDeUsuarioFake(Guid userId) : IUserProvider
{
    public Guid? Id => userId;

    public bool EstaAutenticado => true;
}
