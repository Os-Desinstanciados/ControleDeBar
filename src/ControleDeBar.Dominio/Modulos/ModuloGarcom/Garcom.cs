using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.Modulos.ModuloGarcom;

public class Garcom : EntidadeBase<Garcom>
{
    public string Nome { get; set; } = string.Empty;    

    public Garcom()
    {
    }

    public Garcom(string nome) : this()
    {
        Nome = nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        else if (Nome.Length < 2 || Nome.Length > 50)
            erros.Add("O campo \"Nome\" deve conter entre 2 e 50.");

        return erros;
    }

    public override void Atualizar(Garcom entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
    }
}