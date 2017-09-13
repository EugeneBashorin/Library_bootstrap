using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public class IndexModel
    {
        public BooksFilterModel BooksFilterModel { get; set; }
        public MagazineFilterModel MagazineFilterModel { get; set; }
        public NewspaperFilterModel NewspaperFilterModel { get; set; }       
    }
}