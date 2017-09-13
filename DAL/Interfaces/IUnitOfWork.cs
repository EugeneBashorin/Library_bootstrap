using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Magazine> Magazines { get; }
        IRepository<Newspaper> Newspapers { get; }
    }
}
