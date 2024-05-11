using GerenciadorDeTarefas.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefas.Models;

public class Tarefa
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; } = null!;

    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public Importancia Importancia { get; set; }

    public DateTime? Prazo { get; set; } = null;

    public DateTime DataDoCadastro { get; set; }

    public DateTime? DataDaConclusao { get; set; } = null;

    public Tarefa() { }

    public Tarefa(
        string nome,
        string descricao,
        Importancia importancia,
        DateTime? prazo = null,
        DateTime? dataDaConclusao = null
    )
    {
        Nome = nome;
        Descricao = descricao;
        Importancia = importancia;
        Prazo = prazo;
        DataDoCadastro = DateTime.UtcNow;
        DataDaConclusao = dataDaConclusao;
    }
}
