using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System;

[Controller][Route("/")]
public class RouteController : ControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        return new ContentResult
        {
            ContentType = "text/html",
            StatusCode = (int)HttpStatusCode.OK,
            Content = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Content/index.html"))
        };
    }

    [HttpPost("/api/add")]
    public async Task<IActionResult> Add([FromBody] Link link)
    {
        Uri uriResult;
        if(Uri.TryCreate(link.url, UriKind.Absolute, out uriResult))
        {
            DownloadQueue.Add(link.url);
            return new OkResult();
        } else
        {
            return new BadRequestResult();
        }
    }

    [HttpGet("/api/list")]
    public async Task<IActionResult> List()
    {
        return new JsonResult(DownloadQueue.Queue);
    }

    [HttpPost("/api/remove")]
    public async Task<IActionResult> Remove()
    {
        return new NoContentResult();
    }
}

public class Link
{
    public string url { get; set; }
}