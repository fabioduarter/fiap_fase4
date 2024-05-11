using GerenciadorDeTarefas.Data;
using GerenciadorDeTarefas.Enums;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Xunit.Abstractions;

namespace GerenciadorDeTarefas.Test;

public class TarefasRepositoryTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Tarefa _tarefa1 = new()
    {
        Nome = "Tarefa 1",
        Descricao = "...",
        Importancia = Importancia.Alta,
        Prazo = new DateTime(2024, 1, 1, 12, 0, 0),
        DataDoCadastro = new DateTime(2024, 1, 1, 10, 0, 0),
        DataDaConclusao = new DateTime(2024, 1, 1, 12, 0, 0),
    };
    private readonly Tarefa _tarefa2 = new()
    {
        Nome = "Tarefa 2",
        Descricao = "Lorem ipsum dolor sit amet consectetur adipisicing elit. At, minima. Facere qui magni, blanditiis, sapiente laborum enim nisi distinctio excepturi molestias et facilis autem saepe dolor iure animi impedit illo.",
        Importancia = Importancia.Media,
        Prazo = null,
        DataDoCadastro = new DateTime(2024, 1, 1, 10, 0, 0),
        DataDaConclusao = null,
    };

    public TarefasRepositoryTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    [Trait("Method", "CadastrarAsync")]
    public async void CadastrarAsyncDeveRetornarTarefaCadastrada()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("CadastrarAsyncDeveRetornarTarefaCadastrada");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);

        // Arrange
        Tarefa tarefa = new()
        {
            Nome = "Tarefa Teste",
            Descricao = "Lorem ipsum dolor sit amet consectetur adipisicing elit. At, minima. Facere qui magni, blanditiis, sapiente laborum enim nisi distinctio excepturi molestias et facilis autem saepe dolor iure animi impedit illo.",
            Importancia = Importancia.Alta,
            Prazo = new DateTime(2024, 1, 1, 12, 0, 0),
            DataDoCadastro = new DateTime(2024, 1, 1, 10, 0, 0),
            DataDaConclusao = new DateTime(2024, 1, 1, 12, 0, 0),
        };

        // Act
        var retorno = await tarefasRepository.CadastrarAsync(tarefa);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.IsType<Tarefa>(retorno);
        Assert.Equal(tarefa.Id, retorno.Id);
        Assert.Equal(tarefa.Nome, retorno.Nome);
        Assert.Equal(tarefa.Descricao, retorno.Descricao);
        Assert.Equal(tarefa.Importancia, retorno.Importancia);
        Assert.Equal(tarefa.Prazo, retorno.Prazo);
        Assert.Equal(tarefa.DataDoCadastro, retorno.DataDoCadastro);
        Assert.Equal(tarefa.DataDaConclusao, retorno.DataDaConclusao);
    }

    [Fact]
    [Trait("Method", "BuscarTodasAsync")]
    public async void BuscarTodasAsyncDeveRetornarListaVazia()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("BuscarTodasAsyncDeveRetornarListaVazia");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);

        // Act
        var retorno = await tarefasRepository.BuscarTodasAsync();
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.IsAssignableFrom<IEnumerable<Tarefa>>(retorno);
        Assert.Empty(retorno);
    }

    [Fact]
    [Trait("Method", "BuscarTodasAsync")]
    public async void BuscarTodasAsyncDeveRetornarDuasTarefas()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("BuscarTodasAsyncDeveRetornarDuasTarefas");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);
        await tarefasRepository.CadastrarAsync(_tarefa1);
        await tarefasRepository.CadastrarAsync(_tarefa2);

        // Act
        var retorno = await tarefasRepository.BuscarTodasAsync();
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.IsAssignableFrom<IEnumerable<Tarefa>>(retorno);
        Assert.Equal(2, retorno.Count());
    }

    [Fact]
    [Trait("Method", "BuscarPorIdAsync")]
    public async void BuscarPorIdAsyncDeveRetornarNulo()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("BuscarPorIdAsyncDeveRetornarNulo");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);

        // Act
        var retorno = await tarefasRepository.BuscarPorIdAsync(1);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.Null(retorno);
    }

    [Fact]
    [Trait("Method", "BuscarPorIdAsync")]
    public async void BuscarPorIdAsyncDeveRetornarTarefa1()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("BuscarPorIdAsyncDeveRetornarTarefa1");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);
        await tarefasRepository.CadastrarAsync(_tarefa1);
        await tarefasRepository.CadastrarAsync(_tarefa2);

        // Act
        var retorno = await tarefasRepository.BuscarPorIdAsync(1);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.IsType<Tarefa>(retorno);
        Assert.Equal(1, retorno.Id);
        Assert.Equal(_tarefa1.Nome, retorno.Nome);
        Assert.Equal(_tarefa1.Descricao, retorno.Descricao);
        Assert.Equal(_tarefa1.Importancia, retorno.Importancia);
        Assert.Equal(_tarefa1.Prazo, retorno.Prazo);
        Assert.Equal(_tarefa1.DataDoCadastro, retorno.DataDoCadastro);
        Assert.Equal(_tarefa1.DataDaConclusao, retorno.DataDaConclusao);
    }

    [Fact]
    [Trait("Method", "EditarAsync")]
    public async void EditarAsyncDeveRetornarFalso()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("EditarAsyncDeveRetornarFalso");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);
        Tarefa tarefa = _tarefa1;
        tarefa.Id = 1;

        // Act
        var quantitadeItens = context.Tarefas.Count();
        var retorno = await tarefasRepository.EditarAsync(tarefa);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.Equal(0, quantitadeItens);
        Assert.False(retorno);
    }

    [Fact]
    [Trait("Method", "EditarAsync")]
    public async void EditarAsyncDeveRetornarVerdadeiro()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("EditarAsyncDeveRetornarVerdadeiro");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);
        await tarefasRepository.CadastrarAsync(_tarefa1);
        Tarefa tarefa = _tarefa1;
        tarefa.Id = 1;

        // Act
        var quantitadeItens = context.Tarefas.Count();
        var retorno = await tarefasRepository.EditarAsync(tarefa);
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.Equal(1, quantitadeItens);
        Assert.True(retorno);
    }

    [Fact]
    [Trait("Method", "ExcluirAsync")]
    public async void ExcluirAsyncDeveRetornarFalso()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("ExcluirAsyncDeveRetornarFalso");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);

        // Act
        var quantitadeItensAntes = context.Tarefas.Count();
        var retorno = await tarefasRepository.ExcluirAsync(1);
        var quantitadeItensDepois = context.Tarefas.Count();
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.False(retorno);
        Assert.Equal(0, quantitadeItensAntes);
        Assert.Equal(0, quantitadeItensDepois);
    }

    [Fact]
    [Trait("Method", "ExcluirAsync")]
    public async void ExcluirAsyncDeveRetornarVerdadeiro()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase("ExcluirAsyncDeveRetornarVerdadeiro");
        ApplicationDbContext context = new(optionsBuilder.Options);
        TarefasRepository tarefasRepository = new(context);
        await tarefasRepository.CadastrarAsync(_tarefa1);

        // Act
        var quantitadeItensAntes = context.Tarefas.Count();
        var retorno = await tarefasRepository.ExcluirAsync(1);
        var quantitadeItensDepois = context.Tarefas.Count();
        _testOutputHelper.WriteLine(JsonSerializer.Serialize(retorno, new JsonSerializerOptions() { WriteIndented = true }));

        // Assert
        Assert.True(retorno);
        Assert.Equal(1, quantitadeItensAntes);
        Assert.Equal(0, quantitadeItensDepois);
    }
}
