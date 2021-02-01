using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using App.Core.Util.Adonet;
using Web.API.Models;
using System.Data.SqlClient;
using App.Core.Util.Logger;

namespace Web.API.Repository
{
    public class BookRepo : IBookRepo
    {
        public IConfiguration Configuration { get; }

        public BookRepo(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int Update(Book book)
        {
            var affectedRows = 0;
            try
            {
                using var sqlServerHelper = new SqlHelper(connectionString: Configuration.GetConnectionString(name: "BookStoreDB"));
                affectedRows = sqlServerHelper.ExecNonQuerySp(
                    procName: "usp_update_book", paramNames: new string[]
                    {
                          "@id",
                          "@title",
                          "@isbn",
                          "@language",
                          "@author",
                          "@price",
                          "@remarks"
                    },

                    paramValues: new object[]
                    {
                          book.Id,
                          book.Title,
                          book.Isbn,
                          !string.IsNullOrWhiteSpace(value: book.Language)?  book.Language : (object)DBNull.Value,
                          !string.IsNullOrWhiteSpace(value: book.Author)?  book.Author : (object)DBNull.Value,
                          book.Price??(object)DBNull.Value,
                          !string.IsNullOrWhiteSpace(value: book.Remarks)?  book.Remarks : (object)DBNull.Value,
                    }
               );
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(exception: ex, path: Configuration[key: "Exceptions:FilePath"]);
            }

            return affectedRows;
        }
        public int Delete(long id)
        {
            var affectedRows = 0;
            try
            {
                using var sqlServerHelper = new SqlHelper(connectionString: Configuration.GetConnectionString(name: "BookStoreDB"));
                affectedRows = sqlServerHelper.ExecNonQuerySp(procName: "usp_delete_book", paramNames: new string[]
                { 
                    "@id" 
                }, 
                paramValues: new object[]
                {
                    id
                });
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(exception: ex, path: Configuration[key: "Exceptions:FilePath"]);
            }

            return affectedRows;
        }


        public int Save(Book book)
        {
            var affectedRows = 0;
            try
            {
                using var sqlServerHelper = new SqlHelper(connectionString: Configuration.GetConnectionString(name: "BookStoreDB"));
                affectedRows = sqlServerHelper.ExecNonQuerySp(
                    procName: "usp_add_book", paramNames: new string[]
                    {
                          "@title",
                          "@isbn",
                          "@language",
                          "@author",
                          "@price",
                          "@remarks"
                    },

                    paramValues: new object[]
                    {
                          book.Title,
                          book.Isbn,
                          !string.IsNullOrWhiteSpace(value: book.Language)?  book.Language : (object)DBNull.Value,
                          !string.IsNullOrWhiteSpace(value: book.Author)?  book.Author : (object)DBNull.Value,
                          book.Price??(object)DBNull.Value,
                          !string.IsNullOrWhiteSpace(value: book.Remarks)?  book.Remarks : (object)DBNull.Value,
                    }
               );
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(exception: ex, path: Configuration[key: "Exceptions:FilePath"]);
            }

            return affectedRows;
        }

        IEnumerable<Book> IBookRepo.GetBooks()
        {
            IList<Book> books = new List<Book>();
            SqlDataReader reader = null;

            try
            {
                using var sqlServerHelper = new SqlHelper(connectionString: Configuration.GetConnectionString(name: "BookStoreDB"));
                reader = sqlServerHelper.ExecReaderSp(procName: "usp_get_books");

                while (reader.Read())
                {
                    books.Add(item: new Book
                    {
                        Id = reader.GetInt64(0),
                        Title = reader.GetString(1),
                        Isbn = reader.GetInt32(2),
                        Language = !reader.IsDBNull(3) ? reader.GetString(3) : null,
                        Author = !reader.IsDBNull(4) ? reader.GetString(4) : null,
                        Price = !reader.IsDBNull(5) ? reader.GetDecimal(5) : null,
                        LastUpdate = reader.GetDateTime(6),
                        Remarks = !reader.IsDBNull(7) ? reader.GetString(7) : null
                    });
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(exception: ex, path: Configuration[key: "Exceptions:FilePath"]);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return books;
        }

        public Book Edit(long id)
        {
            Book book = null;
            SqlDataReader reader = null;

            try
            {
                using var sqlServerHelper = new SqlHelper(connectionString: Configuration.GetConnectionString(name: "BookStoreDB"));
                reader = sqlServerHelper.ExecReaderSp(
                    procName: "usp_edit_books", paramNames: new[] { "@id" }, paramValues: new object[] { id }
                );

                if (reader.Read())
                {
                    book = new Book
                    {
                        Id = reader.GetInt64(0),
                        Title = reader.GetString(1),
                        Isbn = reader.GetInt32(2),
                        Language = !reader.IsDBNull(3) ? reader.GetString(3) : null,
                        Author = !reader.IsDBNull(4) ? reader.GetString(4) : null,
                        Price = !reader.IsDBNull(5) ? reader.GetDecimal(5) : null,
                        Remarks = !reader.IsDBNull(6) ? reader.GetString(6) : null
                    };
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(exception: ex, path: Configuration[key: "Exceptions:FilePath"]);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return book;
        }
    }
}
