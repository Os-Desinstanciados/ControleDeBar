using System.ComponentModel.DataAnnotations;
using ControleDeBar.Dominio.Modulos.ModuloMesa;

namespace ControleDeBar.WebApp.Modulos.ModuloMesa;

public record ListarMesasViewModel(
    string Numero,
    string NumeroLugares,
    StatusMesa StatusMesa,
    Guid Id
);

public record CadastrarMesaViewModel(
    [Required(ErrorMessage = "O campo \"Número\" deve ser preenchido.")]
    [StringLength(3, ErrorMessage = "O campo \"Número\" deve conter até 3 caracteres.")]
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "O campo \"Número\" deve conter apenas números e não pode ser igual a zero.")]
    string Numero,

    [Required(ErrorMessage = "O campo \"Número de Lugares\" deve ser preenchido.")]
    [StringLength(2, ErrorMessage = "O campo \"Número\" deve conter até 2 caracteres.")]
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "O campo \"Número de Lugares\" deve conter apenas números e não pode ser igual a zero.")]
    string NumeroLugares,

    StatusMesa StatusMesa = StatusMesa.Livre
);

public record EditarMesaViewModel(

    Guid Id,
   
    [Required(ErrorMessage = "O campo \"Número\" deve ser preenchido.")]
    [StringLength(3, ErrorMessage = "O campo \"Número\" deve conter até 3 caracteres.")]
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "O campo \"Número\" deve conter apenas números e não pode ser igual a zero.")]
    string Numero,

    [Required(ErrorMessage = "O campo \"Número de Lugares\" deve ser preenchido.")]
    [StringLength(2, ErrorMessage = "O campo \"Número\" deve conter até 2 caracteres.")]
    [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "O campo \"Número de Lugares\" deve conter apenas números e não pode ser igual a zero.")]
    string NumeroLugares,

    StatusMesa StatusMesa = StatusMesa.Livre    
    
);

public record ExcluirMesaViewModel(
    string Numero,
    string NumeroLugares,
    StatusMesa StatusMesa,
    Guid Id
);