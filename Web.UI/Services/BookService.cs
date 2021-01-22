using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Web.UI.Models;


namespace Web.UI.Services
{
    public class BookService : IBookService
    {
        public IConfiguration Configuration { get; }
        private readonly HttpClient _httpClient;

        public BookService(IConfiguration configuration, HttpClient httpClient)
        {
            Configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            IList<Book> books = null;
            try
            {
                books = await _httpClient.GetFromJsonAsync<Book[]>(requestUri: "getbooks");
            }
            catch (Exception)
            {
               //ignore
            }

            return books;
        }

        public async Task<string> SaveAsync(Book book)
        {
            var jsonPayload = string.Empty;
            try
            {
                using var response = await _httpClient.PostAsJsonAsync(requestUri: "save", value: book);

                jsonPayload = !response.IsSuccessStatusCode
                    ? (await response.Content.ReadFromJsonAsync<InvalidHttpRespMsg>()).Title
                    : await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
               //ignore
            }
            return jsonPayload;
        }

        public async Task<Book> EditAsync(long id)
        {
            Book book = null;
            try
            {
                using var response = await _httpClient.GetAsync(requestUri: $"edit/{id}");

                if (response.IsSuccessStatusCode)
                {
                    book = await response.Content.ReadFromJsonAsync<Book>();
                }
            }
            catch (Exception)
            {
                //ignore
            }
            return book;
        }

        public async Task<string> UpdateAsync(Book book)
        {
            var jsonPayload = string.Empty;
            try
            {
                using var response = await _httpClient.PutAsJsonAsync(requestUri: "update", value: book);

                jsonPayload = !response.IsSuccessStatusCode
                    ? (await response.Content.ReadFromJsonAsync<InvalidHttpRespMsg>()).Title
                    : await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                //ignore
            }
            return jsonPayload;
        }

        public async Task<string> DeleteAsync(long id)
        {
            var jsonPayload = string.Empty;
            try
            {
                using var response = await _httpClient.DeleteAsync(requestUri: $"delete/{id}");

                jsonPayload = !response.IsSuccessStatusCode
                    ? (await response.Content.ReadFromJsonAsync<InvalidHttpRespMsg>()).Title
                    : await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                //ignore
            }
            return jsonPayload;
        }
    }

    class InvalidHttpRespMsg
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
    }
}
