using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.API.Models;
using Web.API.Repository;

namespace Web.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepo _IbookRepo;

        public BookController(IBookRepo bookRepo)
        {
            _IbookRepo = bookRepo;
        }

        [HttpPost(template: "save")]
        public ActionResult Save([FromBody] Book book)
        {
            if(_IbookRepo.Save(book: book) > 0)
            {
                return Created(uri: "http://localhost:58504/api/getbooks", value: new { book.Isbn });
            }
            return BadRequest();
        }


        [HttpGet(template: "getbooks")]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(value: _IbookRepo.GetBooks());
        }


        [HttpPut(template: "update")]
        public ActionResult Update([FromBody] Book book)
        {
            if (_IbookRepo.Update(book: book) > 0)
            {
                return Ok();
            }
            return NotFound();
        }


        [HttpDelete(template: "delete/{id:long}")]
        public ActionResult Delete([FromRoute] long id)
        {
            if (_IbookRepo.Delete(id: id) > 0)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
