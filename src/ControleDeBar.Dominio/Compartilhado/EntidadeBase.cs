using ControleDeBar.Dominio.Compartilhado.Identity;

namespace ControleDeBar.Dominio.Compartilhado;
public abstract class EntidadeBase<T> : IEntidadeUsuario
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid UserId { get; set; } = Guid.Empty;
    public abstract List<string> Validar();
    public abstract void Atualizar(T entidadeAtualizada);
}
