using GerenciadorDeTarefas.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace GerenciadorDeTarefas.Models;

public class TarefaDTO
{
    [Required(AllowEmptyStrings = false)]
    [MinLength(3)]
    public string Nome { get; set; } = null!;

    [DefaultValue(null)]
    public string? Descricao { get; set; } = null;

    [Required]
    public Importancia Importancia { get; set; }

    public DateTime? Prazo { get; set; } = null;

    public DateTime? DataDaConclusao { get; set; } = null;

    public Tarefa DTOParaModelo()
    {
        return new Tarefa(
            HttpUtility.HtmlEncode(Nome),
            Descricao is not null ? HttpUtility.HtmlEncode(Descricao) : string.Empty,
            Importancia,
            Prazo,
            DataDaConclusao
        );
    }
}
