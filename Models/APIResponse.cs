using System.Net;

namespace PropiedadesMinimalAPI.Models;

public class APIResponse
{
    public bool Success { get; set; }
    public Object Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Errors { get; set; }
}
