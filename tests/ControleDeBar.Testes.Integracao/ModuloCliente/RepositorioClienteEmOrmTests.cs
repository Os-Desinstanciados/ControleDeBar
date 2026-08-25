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

        Assert.IsTrue(conseguiuEditar);
        Assert.AreEqual("João Testes", cliente.Nome);
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
        Cliente cliente = new Cliente("Junior Testes");
        repositorio.Cadastrar(cliente);

        List<Cliente> clientes = repositorio.SelecionarTodos();
        dbContext.ChangeTracker.Clear();

        Assert.HasCount(1, clientes);
    }
}
