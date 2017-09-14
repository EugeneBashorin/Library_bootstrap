using Entityes.Entities;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Models
{ 
    public class MagazineFilterModel
    {
        public List<Magazine> Magazines { get; set; }
        public SelectList MagazinesPublisher { get; set; }
    }
}