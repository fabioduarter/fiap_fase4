using System.ComponentModel;

namespace GerenciadorDeTarefas.Enums;

public enum Importancia
{
    [Description("Crítica")]
    Critica,

    [Description("Alta")]
    Alta,

    [Description("Média")]
    Media,

    [Description("Baixa")]
    Baixa,
}
