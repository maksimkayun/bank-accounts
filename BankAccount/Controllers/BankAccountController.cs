using BankAccount.DTO;
using BankAccount.Exceptions;
using BankAccount.Interfaces;
using BankAccount.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BankAccountController : Controller
{
    private readonly ILogger<BankAccountController> _logger;
    private readonly IAccountService _service;

    public BankAccountController(ILogger<BankAccountController> logger, IAccountService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] GetAccountsRequest request)
    {
        return Ok(_service.GetAccounts(take: request.Take, skip: request.Skip));
    }

    [HttpPost("{id}")]
    [ProducesErrorResponseType(typeof(ErrorInfo))]
    public async Task<ActionResult> GetById(string id)
    {
        var result = _service.GetAccountById(id);

        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest("not found");
    }
    
    [HttpPost("{accountNumber}")]
    public async Task<ActionResult> GetByAccountNumber(int accountNumber)
    {
        var result = _service.GetAccountByNumber(accountNumber);

        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest("not found");
    }

    [HttpPost]
    public async Task<IActionResult> CreateIndex(List<string> properties)
    {
        _service.CreateCompositeIndex("banskaccounts", "accounts", properties);
        return Ok();
    }
}