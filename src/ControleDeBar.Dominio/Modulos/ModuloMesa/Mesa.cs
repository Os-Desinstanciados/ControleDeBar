using ControleDeBar.Dominio.Compartilhado;

namespace ControleDeBar.Dominio.Modulos.ModuloMesa;

public class Mesa : EntidadeBase<Mesa>
{
    public string Numero { get; set; } = string.Empty; 
    public string NumeroLugares { get; set; } = string.Empty;
    public StatusMesa StatusMesa { get; set; } 
    public Guid MesaId { get; set; }

    public Mesa()
    {
    }

    public Mesa(string numero, string numeroLugares) : this()
    {
        Numero = numero;
        NumeroLugares = numeroLugares;
        StatusMesa = StatusMesa.Livre;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Numero))
            erros.Add("O campo \"Número\" deve ser preenchido.");

        else if (Numero.Length > 3)
            erros.Add("O campo \"Número\" deve conter até 3 caracteres.");

        else if (Numero == "0")
            erros.Add("O campo \"Número\" não deve ser igual a zero.");

        else if (!Numero.All(char.IsDigit))
            erros.Add("O campo \"Número\" deve conter apenas números.");

        if (string.IsNullOrWhiteSpace(NumeroLugares))
            erros.Add("O campo \"Número de Lugares\" deve ser preenchido.");

        else if (NumeroLugares.Length > 2)
            erros.Add("O campo \"Número de Lugares\" deve conter até 2 caracteres.");

        else if (NumeroLugares == "0")
            erros.Add("O campo \"Número de Lugares\" não deve ser igual a zero.");

        else if (!NumeroLugares.All(char.IsDigit))
            erros.Add("O campo \"Número\" deve conter apenas números.");

        return erros;
    }

    public override void Atualizar(Mesa entidadeAtualizada)
    {
        Numero = entidadeAtualizada.Numero;
        NumeroLugares = entidadeAtualizada.NumeroLugares;
        StatusMesa = entidadeAtualizada.StatusMesa;
    }

    public void MesaEstaOcupada()
    {
        StatusMesa = StatusMesa.Ocupada;
    }

    public void MesaEstaLivre()
    {
        StatusMesa = StatusMesa.Livre;
    }
}