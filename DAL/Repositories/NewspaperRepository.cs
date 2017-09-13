using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    class NewspaperRepository : IRepository<Newspaper>
    {
        IEnumerable<Newspaper> GetAll()
        {
            throw new NotImplementedException();
        }
        public Book Get(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Newspaper> Find(Func<Newspaper, Boolean> predicate)
        {
            throw new NotImplementedException();
        }
        public void Create(Newspaper item)
        { }
        public void Update(Newspaper item)
        { }
        public void Delete(int id)
        { }
    }
}
