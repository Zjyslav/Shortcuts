using KritaShortcuts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KritaShortcutsApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class KritaController : ControllerBase
{
    private readonly ShortcutsService _shortcuts;
    private readonly ILogger<KritaController> _log;

    public KritaController(ShortcutsService shortcuts, ILogger<KritaController> log)
    {
        _shortcuts = shortcuts;
        _log = log;
    }

    [HttpPost("undo" ,Name = "undo")]
    public ActionResult<string> Undo()
    {
        try
        {
            //_shortcuts.SendShortcut("krita", "^(z)");
            _shortcuts.SendPlus("krita");
        }
        catch (Exception ex)
        {
            return BadRequest(error: $"Error message:\n{ex.Message}\n{ex.StackTrace}");
        }
        
        return Ok();
    }

    [HttpGet(Name ="test")]
    public ActionResult<int> Test()
    {
        return Ok(1);
    }
}
