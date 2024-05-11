using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route("/")]
public class RaizController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(void), StatusCodes.Status302Found)]
    public ActionResult Redirecionar()
    {
        return Redirect("/swagger/index.html");
    }
}
