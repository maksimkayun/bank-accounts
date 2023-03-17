using BankAccount.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BankAccountController : Controller
{
    private readonly ILogger<BankAccountController> _logger;
    private readonly ICRUDService _service;

    public BankAccountController(ILogger<BankAccountController> logger, ICRUDService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Get()
    {
        return Ok(_service.GetClients());
    }
}