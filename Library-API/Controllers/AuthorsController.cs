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
    public class AuthorsController : Controller
    {

        private readonly ApplicationDbContext context;

        public AuthorsController(ApplicationDbContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> Get()
        {
            return context.Authors.Include(x => x.Books).ToList();
        }

        [HttpGet("{id}", Name = "getAuthor")]
        public ActionResult<Author> Get(int id)
        {
            var authorFound = context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);

            if (authorFound == null) 
            {
                return NotFound();
            }

            return authorFound;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();

            return new CreatedAtRouteResult("getAuthor", new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Author author)
        {
            var authorFound = context.Authors.FirstOrDefault(i => i.Id == id);

            if (authorFound == null)
            {
                return NotFound();
            }
            else
            {
                authorFound.name = author.name;
                context.Entry(authorFound).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();

                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Author> Delete(int id)
        {
            var authorFound = context.Authors.FirstOrDefault(i => i.Id == id);

            if (authorFound == null)
            {
                return NotFound();
            }
            else
            {
                context.Authors.Remove(authorFound);
                context.SaveChanges();

                return Ok();
            }
        }
    }
}
