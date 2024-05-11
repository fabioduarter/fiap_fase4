using GerenciadorDeTarefas.Controllers;
using GerenciadorDeTarefas.Enums;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GerenciadorDeTarefas.Test;

public class TarefasControllerTest
{
    private readonly TarefasController _tarefasController;
    private readonly Tarefa _tarefaCadastrada = new()
    {
        Id = 1,
        Nome = "Tarefa Cadastrada",
        Descricao = "Lorem ipsum dolor sit amet consectetur adipisicing elit. At, minima. Facere qui magni, blanditiis, sapiente laborum enim nisi distinctio excepturi molestias et facilis autem saepe dolor iure animi impedit illo.",
        Importancia = Importancia.Media,
        Prazo = null,
        DataDoCadastro = new DateTime(2024, 1, 1, 10, 0, 0),
        DataDaConclusao = null,
    };

    public TarefasControllerTest()
    {
        // Default Arrange
        var mockTarefaRepository = new Mock<ITarefasRepository>();
        mockTarefaRepository.Setup(m => m.BuscarTodasAsync()).ReturnsAsync(new List<Tarefa>() { _tarefaCadastrada });
        mockTarefaRepository.Setup(m => m.BuscarPorIdAsync(It.Is<long>(id => id != 1))).ReturnsAsync(() => null);
        mockTarefaRepository.Setup(m => m.BuscarPorIdAsync(It.Is<long>(id => id == 1))).ReturnsAsync(_tarefaCadastrada);
        mockTarefaRepository.Setup(m => m.CadastrarAsync(It.Is<Tarefa>(tarefa => tarefa.Nome == "Tarefa Cadastrada"))).ReturnsAsync(_tarefaCadastrada);
        mockTarefaRepository.Setup(m => m.EditarAsync(It.Is<Tarefa>(tarefa => tarefa.Id != 1))).ReturnsAsync(false);
        mockTarefaRepository.Setup(m => m.EditarAsync(It.Is<Tarefa>(tarefa => tarefa.Id == 1))).ReturnsAsync(true);
        mockTarefaRepository.Setup(m => m.ExcluirAsync(It.Is<long>(id => id != 1))).ReturnsAsync(false);
        mockTarefaRepository.Setup(m => m.ExcluirAsync(It.Is<long>(id => id == 1))).ReturnsAsync(true);
        _tarefasController = new TarefasController(mockTarefaRepository.Object);
    }

    [Fact]
    [Trait("Method", "GetTarefas")]
    public async void GetTarefasDeveRetornarListaVazia()
    {
        // Arrange
        var mockTarefaRepository = new Mock<ITarefasRepository>();
        mockTarefaRepository.Setup(m => m.BuscarTodasAsync()).ReturnsAsync(new List<Tarefa>());
        var tarefasController = new TarefasController(mockTarefaRepository.Object);

        // Act
        var actionResult = await tarefasController.GetTarefas();
        var result = actionResult.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<Tarefa>;

        // Assert
        Assert.NotNull(value);
        Assert.Empty(value);
    }

    [Fact]
    [Trait("Method", "GetTarefas")]
    public async void GetTarefasDeveRetornarListaComTarefaCadastrada()
    {
        // Act
        var actionResult = await _tarefasController.GetTarefas();
        var result = actionResult.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<Tarefa>;

        // Assert
        Assert.NotNull(value);
        Assert.NotEmpty(value);
        Assert.Equal(value.FirstOrDefault()?.Id, _tarefaCadastrada.Id);
        Assert.Equal(value.FirstOrDefault()?.Nome, _tarefaCadastrada.Nome);
        Assert.Equal(value.FirstOrDefault()?.Descricao, _tarefaCadastrada.Descricao);
        Assert.Equal(value.FirstOrDefault()?.Importancia, _tarefaCadastrada.Importancia);
        Assert.Equal(value.FirstOrDefault()?.Prazo, _tarefaCadastrada.Prazo);
        Assert.Equal(value.FirstOrDefault()?.DataDoCadastro, _tarefaCadastrada.DataDoCadastro);
        Assert.Equal(value.FirstOrDefault()?.DataDaConclusao, _tarefaCadastrada.DataDaConclusao);
    }

    [Fact]
    [Trait("Method", "GetTarefa")]
    public async void GetTarefaDeveRetornarNotFoundObjectResult()
    {
        // Act
        var actionResult = await _tarefasController.GetTarefa(0);

        // Assert
        Assert.IsType<NotFoundObjectResult>(actionResult.Result);
    }

    [Fact]
    [Trait("Method", "GetTarefa")]
    public async void GetTarefaDeveRetornarTarefaCadastrada()
    {
        // Act
        var actionResult = await _tarefasController.GetTarefa(1);
        var result = actionResult.Result as OkObjectResult;
        var value = result?.Value as Tarefa;

        // Assert
        Assert.NotNull(value);
        Assert.Equal(value.Id, _tarefaCadastrada.Id);
        Assert.Equal(value.Nome, _tarefaCadastrada.Nome);
        Assert.Equal(value.Descricao, _tarefaCadastrada.Descricao);
        Assert.Equal(value.Importancia, _tarefaCadastrada.Importancia);
        Assert.Equal(value.Prazo, _tarefaCadastrada.Prazo);
        Assert.Equal(value.DataDoCadastro, _tarefaCadastrada.DataDoCadastro);
        Assert.Equal(value.DataDaConclusao, _tarefaCadastrada.DataDaConclusao);
    }

    [Fact]
    [Trait("Method", "PostTarefa")]
    public async void PostTarefaDeveRetornarTarefaCadastrada()
    {
        // Arrange
        TarefaDTO tarefaDTO = new()
        {
            Nome = _tarefaCadastrada.Nome,
            Descricao = _tarefaCadastrada.Descricao,
            Importancia = _tarefaCadastrada.Importancia,
            Prazo = _tarefaCadastrada.Prazo,
            DataDaConclusao = _tarefaCadastrada.DataDaConclusao
        };

        // Act
        var actionResult = await _tarefasController.PostTarefa(tarefaDTO);
        var result = actionResult.Result as CreatedAtActionResult;
        var value = result?.Value as Tarefa;

        // Assert
        Assert.NotNull(value);
        Assert.Equal(value.Id, _tarefaCadastrada.Id);
        Assert.Equal(value.Nome, _tarefaCadastrada.Nome);
        Assert.Equal(value.Descricao, _tarefaCadastrada.Descricao);
        Assert.Equal(value.Importancia, _tarefaCadastrada.Importancia);
        Assert.Equal(value.Prazo, _tarefaCadastrada.Prazo);
        Assert.Equal(value.DataDoCadastro, _tarefaCadastrada.DataDoCadastro);
        Assert.Equal(value.DataDaConclusao, _tarefaCadastrada.DataDaConclusao);
    }

    [Fact]
    [Trait("Method", "PutTarefa")]
    public async void PutTarefaDeveRetornarNotFoundObjectResult()
    {
        // Arrange
        TarefaDTO tarefaDTO = new()
        {
            Nome = _tarefaCadastrada.Nome,
            Descricao = _tarefaCadastrada.Descricao,
            Importancia = _tarefaCadastrada.Importancia,
            Prazo = _tarefaCadastrada.Prazo,
            DataDaConclusao = _tarefaCadastrada.DataDaConclusao
        };

        // Act
        var actionResult = await _tarefasController.PutTarefa(0, tarefaDTO);

        // Assert
        Assert.IsType<NotFoundObjectResult>(actionResult);
    }

    [Fact]
    [Trait("Method", "PutTarefa")]
    public async void PutTarefaDeveRetornarNoContentResult()
    {
        // Arrange
        TarefaDTO tarefaDTO = new()
        {
            Nome = _tarefaCadastrada.Nome,
            Descricao = _tarefaCadastrada.Descricao,
            Importancia = _tarefaCadastrada.Importancia,
            Prazo = _tarefaCadastrada.Prazo,
            DataDaConclusao = _tarefaCadastrada.DataDaConclusao
        };

        // Act
        var actionResult = await _tarefasController.PutTarefa(1, tarefaDTO);

        // Assert
        Assert.IsType<NoContentResult>(actionResult);
    }

    [Fact]
    [Trait("Method", "DeleteTarefa")]
    public async void DeleteTarefaDeveRetornarNotFoundObjectResult()
    {
        // Act
        var actionResult = await _tarefasController.DeleteTarefa(0);

        // Assert
        Assert.IsType<NotFoundObjectResult>(actionResult);
    }

    [Fact]
    [Trait("Method", "DeleteTarefa")]
    public async void DeleteTarefaDeveRetornarNoContentResult()
    {
        // Act
        var actionResult = await _tarefasController.DeleteTarefa(1);

        // Assert
        Assert.IsType<NoContentResult>(actionResult);
    }
}
