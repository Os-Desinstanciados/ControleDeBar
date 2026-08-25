using ControleDeBar.Dominio.Modulos.ModuloCliente;
using ControleDeBar.Infra.Compartilhado.Orm;
using ControleDeBar.Testes.Integracao.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace ControleDeBar.Testes.Integracao.ModuloCliente;

[TestClass]
public sealed class RepositorioClienteEmOrmTests : RepositorioEmOrmBaseTests
{
    private ControleDeBarDbContext dbContext = null!;
    private RepositorioClienteEmOrm repositorio = null!;

    [TestInitialize]
    public void InicializarRepositorio()
    {
        dbContext = CriarDbContext();

        repositorio = new RepositorioClienteEmOrm(dbContext);
    }

    [TestMethod]
    public void CadastrarESelecionarPorId_CarregaCliente()
    {
        Cliente cliente = new Cliente("Junior Testes");

        repositorio.Cadastrar(cliente);
        dbContext.ChangeTracker.Clear();

        Cliente? clienteSelecionado = repositorio.SelecionarPorId(cliente.Id);

        Assert.IsNotNull(clienteSelecionado);
        Assert.AreEqual("Junior Testes", clienteSelecionado.Nome);
    }

    [TestMethod]
    public void Editar_AtualizaClienteExistente()
    {
        Cliente cliente = new Cliente("Junior Testes");
        repositorio.Cadastrar(cliente);

        Cliente clienteAtualizado = new Cliente("João Testes");

        bool conseguiuEditar = repositorio.Editar(cliente.Id, clienteAtualizado);
        dbContext.ChangeTracker.Clear();

        Cliente? clienteSelecionado = repositorio.SelecionarPorId(cliente.Id);

        Assert.IsTrue(conseguiuEditar);
        Assert.IsNotNull(clienteSelecionado);
        Assert.AreEqual("João Testes", clienteSelecionado.Nome);
    }

    [TestMethod]
    public void Excluir_RemoveClienteExistente()
    {
        Cliente cliente = new Cliente("Junior Testes");
        repositorio.Cadastrar(cliente);

        bool conseguiuExcluir = repositorio.Excluir(cliente.Id);
        dbContext.ChangeTracker.Clear();

        Assert.IsTrue(conseguiuExcluir);
        Assert.IsNull(repositorio.SelecionarPorId(cliente.Id));
    }

    [TestMethod]
    public void SelecionarTodos_RetornaClientes()
    {
        Cliente cliente1 = new Cliente("Junior Testes");
        Cliente cliente2 = new Cliente("João Testes");
        Cliente cliente3 = new Cliente("Maria Testes");

        repositorio.Cadastrar(cliente1);
        repositorio.Cadastrar(cliente2);
        repositorio.Cadastrar(cliente3);

        dbContext.ChangeTracker.Clear();

        List<Cliente> clientes = repositorio.SelecionarTodos();

        Assert.HasCount(3, clientes);
    }
}
