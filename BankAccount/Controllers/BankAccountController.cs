using BankAccount.DTO;
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
    public async Task<ActionResult> GetById(string id)
    {
        var result = _service.GetAccountById(id);

        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest("not found");
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount([FromBody] AccountDto accountDto)
    {
        _service.CreateAccount(accountDto);
        return Ok(accountDto.Id);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> UpdateAccount(string id, [FromBody] AccountDto accountDto)
    {
        var accountUpd = _service.UpdateAccount(id, accountDto);
        return Ok(accountUpd);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> DeleteAccount(string id)
    {
        var account = _service.DeleteAccount(id);
        return Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIndex(List<string> properties)
    {
        _service.CreateCompositeIndex("banskaccounts", "accounts", properties);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> FillDb()
    {
        _service.SeedCollectionAccounts();
        return Ok();
    }
}