using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloConta;
using ControleDeBar.Aplicacao.Modulos.ModuloGarcom;
using ControleDeBar.Aplicacao.Modulos.ModuloMesa;
using ControleDeBar.Dominio.Modulos.ModuloMesa;
using ControleDeBar.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloConta;

public class ContaController(
    ServicoConta servicoConta,
    ServicoMesa servicoMesa,
    ServicoGarcom servicoGarcom,
    IMapper mapeador
) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarContasDto> dtos = servicoConta.SelecionarTodos();

        List<ListarContasViewModel> listarVms =
            mapeador.Map<List<ListarContasViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        List<ListarMesasDto> mesas = servicoMesa
            .SelecionarTodos()
            .Where(m => m.StatusMesa == StatusMesa.Livre)
            .ToList();

        List<ListarGarconsDto> garcons = servicoGarcom
            .SelecionarTodos();

        CadastrarContaViewModel cadastrarVm = new CadastrarContaViewModel(
            null,
            null,
            mesas,
            garcons
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarContaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            cadastrarVm = cadastrarVm with
            {
                Mesas = servicoMesa
                    .SelecionarTodos()
                    .Where(m => m.StatusMesa == StatusMesa.Livre)
                    .ToList(),

                Garcons = servicoGarcom
                    .SelecionarTodos()
            };

            return View(cadastrarVm);
        }

        CadastrarContaDto dto = new CadastrarContaDto(
            cadastrarVm.MesaId!.Value,
            cadastrarVm.GarcomId!.Value
        );

        Result resultado = servicoConta.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            cadastrarVm = cadastrarVm with
            {
                Mesas = servicoMesa
                    .SelecionarTodos()
                    .Where(m => m.StatusMesa == StatusMesa.Livre)
                    .ToList(),

                Garcons = servicoGarcom
                    .SelecionarTodos()
            };

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesContaDto> resultado = servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesContaViewModel excluirVm =
            mapeador.Map<DetalhesContaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult ExcluirConfirmado(Guid id)
    {
        Result resultado = servicoConta.Excluir(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Detalhes), new { id });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpPost]
    public ActionResult Fechar(Guid id)
    {
        Result resultado = servicoConta.Fechar(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Detalhes), new { id });
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Detalhes(Guid id)
    {
        Result<DetalhesContaDto> resultado = servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesContaViewModel detalhesVm =
            mapeador.Map<DetalhesContaViewModel>(resultado.Value);

        return View(detalhesVm);
    }
}