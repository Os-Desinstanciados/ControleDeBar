using System.ComponentModel.DataAnnotations;

namespace ControleDeBar.WebApp.Modulos.ModuloGarcom;

public record ListarGarconsViewModel(
    Guid Id,
    string Nome
);

public record CadastrarGarcomViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 50 caracteres.")]
    string Nome
);

public record EditarGarcomViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 50 caracteres.")]
    string Nome
);

public record ExcluirGarcomViewModel(
    Guid Id,
    string Nome
);