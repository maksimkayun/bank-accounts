using BankAccount.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TechnicalSupportController : Controller
{
    private readonly ITechnicalSupport _service;

    public TechnicalSupportController(ITechnicalSupport service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> FillDbs()
    {
        _service.SeedCollectionAccounts();
        return Ok();
    }
    
}