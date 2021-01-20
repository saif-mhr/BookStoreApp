using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.UI.Models;

namespace Web.UI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<string> SaveAsync(Book book);

        //Task<Book> InsertBookAsync();
    }
}
