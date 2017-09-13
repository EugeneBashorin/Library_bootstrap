using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    class MagazineRepository : IRepository<Magazine>
    {
        IEnumerable<Magazine> GetAll()
        {
            throw new NotImplementedException();
        }
        public Magazine Get(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Magazine> Find(Func<Magazine, Boolean> predicate)
        {
            throw new NotImplementedException();
        }
        public void Create(Magazine item)
        { }
        public void Update(Magazine item)
        { }
        public void Delete(int id)
        { }
    }
}
