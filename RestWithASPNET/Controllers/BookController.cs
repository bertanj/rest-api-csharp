using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Data.Dto.V1;
using RestWithASPNET.Services.V1;

namespace RestWithASPNET.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")] //api/book/v1
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDTO>>> FindAllAsync()
        {
            var books = await _bookService.FindAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}", Name = "GetBookById")]
        [MapToApiVersion("1")]
        public async Task<ActionResult<BookDTO>> FindByIdAsync(long id)
        { 
            var book = await _bookService.FindByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("author/{author}")]
        public async Task<ActionResult<List<BookDTO>>> FindByAuthorAsync(string author)
        {
            var books = await _bookService.FindByAuthorAsync(author);
            if (books == null) return NotFound();

            return Ok(books);
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<List<BookDTO>>> FindByTitleAsync(string title)
        {
            var books = await _bookService.FindByTitleAsync(title);
            if (books == null) return NotFound();
            return Ok(books);
        }

        [HttpGet("launchdate/{launchDate}")]
        public async Task<ActionResult<List<BookDTO>>> FindByLaunchDateAsync(DateTime launchDate)
        {
            var books = await _bookService.FindByLaunchDateAsync(launchDate);
            if (books == null) return NotFound();

            return Ok(books);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateAsync([FromBody] BookDTO bookDto)
        {
            if (bookDto == null) return BadRequest();

            var createdBook = await _bookService.CreateAsync(bookDto);

            return CreatedAtRoute("GetBookId", new { version = "1", id = createdBook.Id }, createdBook);

        }

        [HttpPut]
        public async Task<ActionResult<BookDTO>> UpdateAsync([FromBody] BookDTO bookDto)
        { 
            if (bookDto == null) return BadRequest();
            var updatedBook = await _bookService.UpdateAsync(bookDto);
            if (updatedBook == null) return NotFound();
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var book = await _bookService.FindByIdAsync(id);
            if (book == null) return NotFound();
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
