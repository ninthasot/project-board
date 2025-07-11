using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public sealed class BoardController() : ControllerBase
{
    [HttpGet]
    public ActionResult<int> Get()
    {
        return Ok(1);
    }
}
