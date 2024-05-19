using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.Controllers;

[Route("api/tarefas")]
[Produces("application/json")]
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly ITarefasRepository _tarefaRepository;

    public TarefasController(ITarefasRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    /// <summary>
    /// Listar Tarefas
    /// </summary>
    /// <remarks>
    /// Lista todas as tarefas cadastradas.
    /// </remarks>
    /// <response code="200">Lista das tarefas cadastradas.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Tarefa>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
    {
        return Ok(await _tarefaRepository.BuscarTodasAsync());
    }

    /// <summary>
    /// Buscar Tarefa
    /// </summary>
    /// <remarks>
    /// Busca uma tarefa específica através de seu "id".
    /// </remarks>
    /// <param name="id">Identificador da tarefa</param>
    /// <response code="200">Tarefa encontrada.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tarefa>> GetTarefa(int id)
    {
        return await _tarefaRepository.BuscarPorIdAsync(id) is Tarefa tarefa
            ? Ok(tarefa)
            : NotFound(new NotFoundObjectResult("Tarefa não encontrada."));
    }

    /// <summary>
    /// Cadastrar Tarefa
    /// </summary>
    /// <remarks>
    /// Cadastra uma tarefa.
    /// </remarks>
    /// <param name="tarefaDTO">Objeto do tipo TarefaDTO</param>
    /// <response code="201">Tarefa cadastrada com sucesso.</response>
    
    /// <response code="400">Erro na requisição.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Tarefa>> PostTarefa(TarefaDTO tarefaDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Tarefa tarefa = await _tarefaRepository.CadastrarAsync(tarefaDTO.DTOParaModelo());
        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
    }

    /// <summary>
    /// Editar Tarefa
    /// </summary>
    /// <remarks>
    /// Edita uma tarefa específica.
    /// </remarks>
    /// <param name="id">Identificador da tarefa</param>
    /// <param name="tarefaDTO">Objeto do tipo TarefaDTO</param>
    /// <response code="204">Tarefa editada com sucesso.</response>
    /// <response code="400">Erro na requisição.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutTarefa(int id, TarefaDTO tarefaDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Tarefa tarefa = tarefaDTO.DTOParaModelo();
        tarefa.Id = id;
        return await _tarefaRepository.EditarAsync(tarefa)
            ? NoContent()
            : NotFound(new NotFoundObjectResult("Tarefa não encontrada."));
    }

    /// <summary>
    /// Excluir Tarefa
    /// </summary>
    /// <remarks>
    /// Exclui uma tarefa específica.
    /// </remarks>
    /// <param name="id">Identificador da tarefa</param>
    /// <response code="204">Tarefa excluída com sucesso.</response>
    /// <response code="404">Tarefa não encontrada.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        return await _tarefaRepository.ExcluirAsync(id)
            ? NoContent()
            : NotFound(new NotFoundObjectResult("Tarefa não encontrada."));
    }
}
