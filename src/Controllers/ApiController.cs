using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

[Controller]
[Route("/api")]
public class ApiController : ControllerBase
{
    [HttpGet("/list")]
    public async Task<IActionResult> List()
    {
        return new JsonResult("[]");
    }

    [HttpPost("/add")]
    public async Task<IActionResult> Add()
    {
        return new JsonResult("{}");
    }

    [HttpPost("/remove")]
    public async Task<IActionResult> Remove()
    {
        return new JsonResult("{}");
    }
}