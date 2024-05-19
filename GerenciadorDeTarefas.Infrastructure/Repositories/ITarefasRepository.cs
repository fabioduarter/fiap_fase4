using GerenciadorDeTarefas.Models;

namespace GerenciadorDeTarefas.Repositories;

/// <summary>Interface padrão para um repositório de tarefas.</summary>
public interface ITarefasRepository
{
    /// <summary>
    ///     Busca, de forma assíncrona, todas as tarefas cadastradas.
    /// </summary>
    /// <returns>
    ///     O resultado da <see cref="Task" /> contém um <see cref="IEnumerable{T}" /> de <see cref="Tarefa" /> ou vazio, caso não existam tarefas cadastradas.
    /// </returns>
    Task<IEnumerable<Tarefa>> BuscarTodasAsync();

    /// <summary>
    ///     Busca, de forma assíncrona, uma tarefa específica através de seu "Id".
    /// </summary>
    /// <returns>
    ///     O resultado da <see cref="Task" /> contém uma <see cref="Tarefa" /> ou <see langword="null" />, caso a tarefa não seja encontrada.
    /// </returns>
    /// <param name="id">
    ///     Propriedade "Id" do objeto <see cref="Tarefa" />.
    /// </param>
    Task<Tarefa?> BuscarPorIdAsync(int id);

    /// <summary>
    ///     Cadastra, de forma assíncrona, uma tarefa.
    /// </summary>
    /// <returns>
    ///     O resultado da <see cref="Task" /> contém a <see cref="Tarefa" /> cadastrada.
    /// </returns>
    /// <param name="tarefa">
    ///     <see cref="Tarefa" /> a ser cadastrada.
    /// </param>
    Task<Tarefa> CadastrarAsync(Tarefa tarefa);

    /// <summary>
    ///     Edita, de forma assíncrona, uma tarefa específica.
    /// </summary>
    /// <returns>
    ///     O resultado da <see cref="Task" /> contém um <see langword="bool" /> <see langword="true" />,
    ///     caso a edição tenha sido realizada com sucesso, ou <see langword="false" />, em caso de falha.
    /// </returns>
    /// <param name="tarefa"><see cref="Tarefa" /> a ser editada.</param>
    Task<bool>EditarAsync(Tarefa tarefa);

    /// <summary>
    ///     Exclui, de forma assíncrona, uma tarefa específica.
    /// </summary>
    /// <returns>
    ///     O resultado da <see cref="Task" /> contém um <see langword="bool" /> <see langword="true" />,
    ///     caso a exclusão tenha sido realizada com sucesso, ou <see langword="false" />, em caso de falha.
    /// </returns>
    /// <param name="id">
    ///     Propriedade "Id" do objeto <see cref="Tarefa" />.
    /// </param>
    Task<bool> ExcluirAsync(int id);
}
