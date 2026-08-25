using ControleDeBar.Dominio.Compartilhado;


namespace ControleDeBar.Dominio.Modulos.ModuloConta;

public interface IRepositorioConta : IRepositorio<Conta>
{
    bool ExisteGarcomContaAberta(Guid garcomId);
    bool ExisteMesaContaAberta(Guid mesaId);
}