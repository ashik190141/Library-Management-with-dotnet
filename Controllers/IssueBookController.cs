using Microsoft.AspNetCore.Mvc;
using Library_Management.Interfaces;
using Library_Management.Models;
namespace Library_Management.Controllers;

[ApiController]
[Route("[controller]")]

public class IssueBookController : ControllerBase
{
    private readonly IIssueBookService _issueBookService;

    public IssueBookController(IIssueBookService issueBookService)
    {
        _issueBookService = issueBookService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateIssueBook([FromBody] IssueBook issueBook)
    {
        var response = await _issueBookService.CreateIssueBookAsync(issueBook);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetIssueBook()
    {
        var response = await _issueBookService.GetAllIssueBooksAsync();
        return Ok(response);
    }

    [HttpPost("return")]
    public async Task<IActionResult> ReturnIssueBook([FromBody] ReturnBookDto returnBookDto)
    {
        var response = await _issueBookService.ReturnBookAsync(returnBookDto);
        return Ok(response);
    }
}