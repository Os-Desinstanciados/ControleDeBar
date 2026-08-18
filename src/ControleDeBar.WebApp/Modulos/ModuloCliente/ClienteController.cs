using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using ControleDeBar.Aplicacao.Modulos.ModuloCliente;
using ControleDeBar.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeBar.WebApp.Modulos.ModuloCliente;

public class ClienteController(ServicoCliente servicoCliente, IMapper mapeador) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarClientesDto> dtos = servicoCliente.SelecionarTodos();

        List<ListarClientesViewModel> listarVms = mapeador.Map<List<ListarClientesViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarClienteViewModel cadastrarVm = new CadastrarClienteViewModel(
            string.Empty
        );

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarClienteViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarClienteDto dto = mapeador.Map<CadastrarClienteDto>(cadastrarVm);

        Result resultado = servicoCliente.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesClienteDto> resultado = servicoCliente.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarClienteViewModel editarVm = mapeador.Map<EditarClienteViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarClienteViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarClienteDto dto = mapeador.Map<EditarClienteDto>(editarVm);

        Result resultado = servicoCliente.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesClienteDto> resultado = servicoCliente.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirClienteViewModel excluirVm = mapeador.Map<ExcluirClienteViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirClienteViewModel excluirVm)
    {
        Result resultado = servicoCliente.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}