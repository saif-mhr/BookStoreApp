using System.Collections.Generic;
using Web.API.Models;

namespace Web.API.Repository
{
    public interface IBookRepo
    {
        int Save(Book book);
        int Update(Book book);
        int Delete(long id);

        Book Edit(long id);

        IEnumerable<Book> GetBooks();      
    }
}
