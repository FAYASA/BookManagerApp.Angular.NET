using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> books = new();

        public List<Book> GetAll() => books;

        public Book GetById(int id) => books.FirstOrDefault(b => b.Id == id);

        public void Add(Book book)
        {
            book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
            books.Add(book);
        }

        public void Update(Book book)
        {
            var index = books.FindIndex(b => b.Id == book.Id);
            if (index != -1)
                books[index] = book;
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
                books.Remove(book);
        }
    }
}