using Library_API.Context;
using Library_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Library_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {

        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            return context.Books.Include(x => x.author).ToList();
        }

        [HttpGet("{id}", Name = "getBook")]
        public ActionResult<Book> Get(int id)
        {
            var bookFound = context.Books.Include(x => x.author).FirstOrDefault(x => x.Id == id);

            if (bookFound == null)
            {
                return NotFound();
            }

            return bookFound;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();

            return new CreatedAtRouteResult("getBook", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Book book)
        {
            var bookFound = context.Books.FirstOrDefault(i => i.Id == id);

            if (bookFound == null)
            {
                return NotFound();
            }
            else
            {
                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    book.Id = bookFound.Id;
                    bookFound.title = book.title;
                    bookFound.authorId = book.authorId;
                    bookFound.cover = book.cover;
                    context.Entry(bookFound).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();

                    return Ok(book);
                }
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Book> Delete(int id)
        {
            var bookFound = context.Books.FirstOrDefault(i => i.Id == id);

            if (bookFound == null)
            {
                return NotFound();
            }
            else
            {
                context.Books.Remove(bookFound);
                context.SaveChanges();

                return Ok();
            }
        }
    }
}

