using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.Modulos.ModuloCliente;

public class Cliente : EntidadeBase<Cliente>
{
    public string Nome { get; set; } = string.Empty;

    public Cliente()
    {

    }

    public Cliente(
        string nome
    ) : this()
    {
        Nome = nome;
    }

    public override void Atualizar(Cliente entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if(Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres.");

        return erros;
    }
}