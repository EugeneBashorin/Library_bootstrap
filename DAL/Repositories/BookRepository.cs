using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    class BookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetAll()
        {
            throw new NotImplementedException();
        }
        public Book Get(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Book> Find(Func<Book, Boolean> predicate)
        {
            throw new NotImplementedException();
        }
        public void Create(Book item)
        { }
        public void Update(Book item)
        { }
        public void Delete(int id)
        { }
    }
}
