using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using userManagement_API.Models;
using userManagement_API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace userManagement_API.Controllers
{


    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private IBooksRepository _booksRepo;


        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepo = booksRepository;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public IEnumerable<Book>  GetBooks()
        {
            return  _booksRepo.GetBooks().ToList();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _booksRepo.GetBooksByID(id);

            if(book == null)
            {
                return NotFound();
            }
            return book;
        }

        // POST api/<BooksController>
        [HttpPost]
        public ActionResult<Book> PostBooks(Book book)
        {
            _booksRepo.InsertBooks(book);
            try
            {
                _booksRepo.Save();
            }catch (DbUpdateException)
            {
                if (_booksRepo.ExistBooks(book.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetBooks",new {id = book.BookId},book);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        
        public ActionResult<Book> PutBooks(int id, Book book )
        {
            var newBook = new Book() { BookId = id,Name=book.Name,Category=book.Category,
                                        Description=book.Description,Author=book.Author,
                                        Publisher=book.Publisher};

            _booksRepo.UpdateBooks(newBook);
            try
            {
                _booksRepo.Save();
            }
            catch (DbUpdateException)
            {
                if(!_booksRepo.ExistBooks(book.BookId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return  NoContent();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        
        public IActionResult DeleteBooks(int id)
        {
            var book = _booksRepo.GetBooksByID(id);
            if(book == null)
            {
                return NotFound();
            }
            else
            {
                _booksRepo.DeleteBooks(id);
                _booksRepo.Save();
            }

            return NoContent();
        }
    }
}
