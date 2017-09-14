using Entityes.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryProject.Models
{
    public class NewspaperFilterModel
    {
        public List<Newspaper> Newspapers { get; set; }
        public SelectList NewspapersPublisher { get; set; }
    }
}