using BankAccount.Interfaces;
using BankAccount.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ClientController : Controller
{
    private readonly IClientService _service;
    private readonly ILogger<ClientController> _logger;

    public ClientController(IClientService service, ILogger<ClientController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    
    public async Task<IActionResult> SendMoney(SendMoneyRequest request)
    {
        return Ok(await _service.MakeTransaction(request));
    }

    [HttpPost]
    public async Task<IActionResult> GetTransactionsByClientId(GetTransactionsByClientIdRequest request)
    {
        var result = _service.GetTransactionsByClientId(request);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetClients(GetClientsRequest request)
    {
        var result = _service.GetClients(take: request.Take, skip: request.Skip);
        return Ok(result);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> GetClientById(string id)
    {
        var result = _service.GetClientById(id);
        return Ok(result);
    }
}