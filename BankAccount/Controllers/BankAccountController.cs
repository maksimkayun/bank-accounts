using BankAccount.DataStorage.MongoModels;
using BankAccount.DTO;
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
    
    [HttpGet("getClientById/{id:length(24)}")]
    public async Task<ActionResult> GetClientById(string id)
    {
        var result = await _service.GetClientById(id);

        if (result!=null)
        {
            return Ok(result);
        }
        return BadRequest("not found");
    }
    [HttpPost]
    public async Task<ActionResult> CreateClient(ClientDto clientDto)
    {
        var client = new Client()
        {
            Name = clientDto.Name,
            SurName = clientDto.SurName,
            Birthday = clientDto.Birthday,
            Email = clientDto.Email,
            PhoneNumber = clientDto.PhoneNumber
        };
        await _service.CreateClient(client);
        return Ok(client);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateClient(string id,ClientDto client)
    {
        client.Id = id;
        await _service.UpdateClient(id,client);
        return Ok(client);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteClient(string id)
    {
        var result = await _service.GetClientById(id);
        await _service.DeleteClient(id);
        return Ok(result);
    }
}