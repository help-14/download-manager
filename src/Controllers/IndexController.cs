using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

[Controller][Route("/")]
public class IndexController : ControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> Index()
    {
        //var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Content/HelloWorld.html"));

        return new ContentResult
        {
            ContentType = "text/html",
            StatusCode = (int)HttpStatusCode.OK,
            Content = "<html><body>Welcome</body></html>"
        };
    }
}