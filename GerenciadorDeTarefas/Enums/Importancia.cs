using System.ComponentModel;

namespace GerenciadorDeTarefas.Enums;

public enum Importancia
{
    [Description("Crítica")]
    Critica = 0,

    [Description("Alta")]
    Alta = 1,

    [Description("Média")]
    Media = 2,

    [Description("Baixa")]
    Baixa = 3,
}
