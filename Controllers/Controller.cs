using api_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    public Controller()
    {
    }

    // GET all action
    [HttpGet]
    public string GetAll() =>
        "Hello sir";

    
}