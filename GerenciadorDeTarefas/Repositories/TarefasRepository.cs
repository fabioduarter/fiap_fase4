using GerenciadorDeTarefas.Data;
using GerenciadorDeTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Repositories;

/// <summary>
///     Repositório de tarefas (<see cref="Tarefa" />), que implementa a interface <see cref="ITarefasRepository" />.
/// </summary>
/// <remarks>
///     Todos os seus métodos são assíncronos. Portanto, utilize <see langword="await" /> para garantir
///     que quaisquer operações assíncronas foram concluídas antes de chamar outro método neste contexto.
/// </remarks>
public class TarefasRepository : ITarefasRepository
{
    public ApplicationDbContext _context;

    public TarefasRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tarefa>> BuscarTodasAsync()
    {
        return await _context.Tarefas.ToListAsync();
    }

    public async Task<Tarefa?> BuscarPorIdAsync(long id)
    {
        return await _context.Tarefas.FindAsync(id);
    }

    public async Task<Tarefa> CadastrarAsync(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<bool> EditarAsync(Tarefa tarefa)
    {
        _context.Entry(tarefa).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tarefas.Any(e => e.Id == tarefa.Id)) return false;
            else throw;
        }
        return true;
    }

    public async Task<bool> ExcluirAsync(long id)
    {
        var tarefa = await BuscarPorIdAsync(id);
        if (tarefa is null) return false;
        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }
}
