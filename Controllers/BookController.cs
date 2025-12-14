using Microsoft.AspNetCore.Mvc;
using Library_Management.Interfaces;
using Library_Management.Models;
namespace Library_Management.Controllers;

[ApiController]
[Route("[controller]")]

public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] Book book)
    {
        var response = await _bookService.AddBookAsync(book);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var response = await _bookService.GetAllBooksAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] UpdateBookDto book)
    {
        var response = await _bookService.UpdateBookAsync(id, book);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        var response = await _bookService.DeleteBookAsync(id);
        return Ok(response);
    }
}