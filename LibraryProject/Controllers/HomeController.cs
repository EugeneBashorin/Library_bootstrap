using LibraryProject.Configurations;
using LibraryProject.Extention_Classes;
using LibraryProject.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        List<Book> bookList;
        List<Magazine> magazineList;
        List<Newspaper> newspaperList;
        IndexModel indexModel;

        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        [HttpGet]
        public ActionResult Index()
        {
            DbInitializer();
            if (User.IsInRole(ConfigurationData._USER_ROLE))
            {
                ViewBag.hideElement = ConfigurationData._ATTRIBUTES_STATE_OFF;
            }
            if (User.IsInRole(ConfigurationData._ADMIN_ROLE) & User.IsInRole(ConfigurationData._USER_ROLE))
            {
                ViewBag.hideElement = ConfigurationData._ATTRIBUTES_STATE_ON;
            }
            
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.accountElementState = ConfigurationData._ATTRIBUTES_STATE_OFF;
                ViewBag.logoutLinkElement = ConfigurationData._ATTRIBUTES_STATE_ON;
            }
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.accountElementState = ConfigurationData._ATTRIBUTES_STATE_ON;
                ViewBag.logoutLinkElement = ConfigurationData._ATTRIBUTES_STATE_OFF;
            }

            if (Session["LibraryState"] == null)
            {
                indexModel = new IndexModel();
                bookList = new List<Book>();
                magazineList = new List<Magazine>();
                newspaperList = new List<Newspaper>();

                bookList.AddRange(new List<Book> { new Book { Name = "Head First C#", Author = "A.Stellman, J.Greene", Publisher = "O.Reilly", Price = 540},
                    new Book{Name = "LINQ Succinctly", Author = "J.Roberts", Publisher = "Syncfusion", Price = 200},
                    new Book{Name = "Java Script Patterns", Author = "S.Stefanov", Publisher = "O.Reilly", Price = 312 },
                    new Book{Name = "Head First JS Programming", Author = "E.Freeman, E.Robson", Publisher = "O.Reilly", Price = 330 },
                    new Book{Name = "SQL The Complete Reference", Author = "J.Groff, P.Weinberg, A.Oppel", Publisher = "Williams", Price = 158 },
                    new Book{Name = "Getting started with ASP.NET 5.0 Web Forms", Author = "N.Gaylord, Ch.Wenz, P.Rastogi, T.Miranda", Publisher = "O.Reilly", Price = 400},
                    new Book{Name = "C# 6.0 Complete Guide", Author = "J.& B.Albahairy", Publisher = "Williams", Price = 499 },
                    new Book{Name = "ASP.NET 4.5 in C# and VB", Author = "J.Gaylord", Publisher = "Wrox", Price = 274 },
                    new Book{Name = "Head First SQL", Author = "Lynn Beighley", Publisher = "O.Reilly", Price = 299 },
                    new Book{Name = "Design Patterns via C#", Author = "A.Shevchyk, A.Kasianov, D.Ohrimenko", Publisher = "ITVDN", Price = 830},
                    new Book{Name = "OOP in C#. Succinctly", Author = "S.Rossel", Publisher = "Syncfusion", Price = 830}
                });

                magazineList.AddRange(new List<Magazine> { new Magazine{ Name = "Martial Mix", Category = "Sport", Price = 16, Publisher = "Williams"},
                    new Magazine{Name = "Fashion", Category = "Fashion", Price = 20, Publisher = "Mag Group"},
                    new Magazine{Name = "Forbs", Category = "Economic", Price = 25, Publisher = "Stanley & Co"},
                    new Magazine{Name = "Geek", Category = "IT", Price = 21, Publisher = "Stanley & Co"},
                    new Magazine{Name = "Amaizing wild world", Category = "Nature", Price = 22, Publisher = "Stanley & Co"},
                    new Magazine{Name = "Braine scince", Category = "Psychology", Price = 26, Publisher = "Williams"},
                    new Magazine{Name = "Car Evo", Category = "Car", Price = 24, Publisher = "MagGroup"},
                    new Magazine{Name = "Robo", Category = "Scince", Price = 32, Publisher = "Stanley & Co"},
                    new Magazine{Name = "Zadrot", Category = "Games", Price = 16, Publisher = "Williams"},
                    new Magazine{Name = "Design & Creative", Category = "Design", Price = 30, Publisher = "Mag Group"}
                });

                newspaperList.AddRange(new List<Newspaper> {new Newspaper{Name = "The NewYork Times", Category = "News", Price = 15, Publisher = "Red Octouber"},
                    new Newspaper{Name = "The WallSteet Jornal", Category = "Economy", Price = 12, Publisher = "Red Octouber"},
                    new Newspaper{Name = "Ring", Category = "Sport", Price = 14, Publisher = "Ronald"},
                    new Newspaper{Name = "Los Angeles Times", Category = "News", Price = 10, Publisher = "West-Cost"},
                    new Newspaper{Name = "The Washington Post", Category = "News", Price = 19, Publisher = "Croxy"},
                    new Newspaper{Name = "The Times", Category = "News", Price = 14, Publisher = "Croxy"},
                    new Newspaper{Name = "The Guardian", Category = "News", Price = 17, Publisher = "West-Cost"},
                    new Newspaper{Name = "The Daily Telegraph", Category = "News", Price = 13, Publisher = "Croxy"},
                    new Newspaper{Name = "Financial Times", Category = "Economy", Price = 21, Publisher = "Red Octouber"},
                    new Newspaper{Name = "Le Figaro", Category = "News", Price = 11, Publisher = "West-Cost"}
                });

                indexModel.Books = bookList;
                indexModel.Magazines = magazineList;
                indexModel.Newspapers = newspaperList;

                Session["LibraryState"] = indexModel;

            }
            if (Session["LibraryState"] != null)
            {
                indexModel = (IndexModel)Session["LibraryState"];
            }
            return View(indexModel);
        }

        public ActionResult DbInitializer()
        {

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult GetPublisherList(string id = "", string publisherName = "")
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            if (id == ConfigurationData._BOOK_KEY)
            {
                List<Book> bookList = indexModel.Books;
                List<Book> newBookList = new List<Book>();
                Session["BookMemorry"] = bookList;
                foreach (var item in bookList)
                {
                    if (item.Publisher == publisherName)
                    {
                        newBookList.Add(item);
                    }
                }
                indexModel.Books = newBookList;
            }
            if (id == ConfigurationData._MAGAZINE_KEY)
            {
                List<Magazine> magazineList = indexModel.Magazines;
                List<Magazine> newMagazineList = new List<Magazine>();
                Session["MagazineMemorry"] = magazineList;
                foreach (var item in magazineList)
                {
                    if (item.Publisher == publisherName)
                    {
                        newMagazineList.Add(item);
                    }
                }
                indexModel.Magazines = newMagazineList;
            }
            if (id == ConfigurationData._NEWSPAPER_KEY)
            {
                List<Newspaper> newspaperList = indexModel.Newspapers;
                List<Newspaper> newNewsPaperList = new List<Newspaper>();
                Session["NewsPaperMemorry"] = newspaperList;
                foreach (var item in newspaperList)
                {
                    if (item.Publisher == publisherName)
                    {
                        newNewsPaperList.Add(item);
                    }
                }
                indexModel.Newspapers = newNewsPaperList;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetAllPublisher(string id = "")
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            if (id == ConfigurationData._BOOK_KEY)
            {
                indexModel.Books = (List<Book>)Session["BookMemorry"];
            }
            if (id == ConfigurationData._MAGAZINE_KEY)
            {
                indexModel.Magazines = (List<Magazine>)Session["MagazineMemorry"];
            }
            if (id == ConfigurationData._NEWSPAPER_KEY)
            {
                indexModel.Newspapers = (List<Newspaper>)Session["NewsPaperMemorry"];
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetBooksList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.Books;
            bookList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewsPapersList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Newspaper> newspaperList = indexModel.Newspapers;
            newspaperList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> MagazinesList = indexModel.Magazines;
            MagazinesList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetBooksXmlList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.Books;
            bookList.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewspapersXmlList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Newspaper> newspaperList = indexModel.Newspapers;
            newspaperList.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesXmlList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> magazineList = indexModel.Magazines;
            magazineList.GetXmlList();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            book.Id = indexModel.Books.Count + ConfigurationData._AUTOINCREMENT;
            indexModel.Books.Add(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowBook(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Book book = (from t in indexModel.Books
                         where t.Id == id
                         select t).First();

            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult EditBook(int id)
        {
           IndexModel indexModel = (IndexModel)Session["LibraryState"];
           Book book = (from t in indexModel.Books
                         where t.Id == id
                         select t).First();
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(Book newBook)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            foreach (Book book in indexModel.Books)
            {
                if (book.Id == newBook.Id)
                {
                    book.Name = newBook.Name;
                    book.Author = newBook.Author;
                    book.Price = newBook.Price;
                    book.Publisher = newBook.Publisher;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult DeleteBook(int? id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            Book book = (from t in indexModel.Books
                         where t.Id == id 
                         select t).First();

            return PartialView(book);
        }

        [HttpPost, ActionName("DeleteBook")]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult DeleteConfirmedBook(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            indexModel.Books.Remove(indexModel.Books.Where(m => m.Id == id).First());
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseBookList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.Books;
            bookList.SetBookListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseMagazineList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> magazineList = indexModel.Magazines;
            magazineList.SetMagazineListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseNewspaperList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Newspaper> newspaperList = indexModel.Newspapers;
            newspaperList.SetNewspaperListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult CreateMagazine()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMagazine(Magazine magazine)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            magazine.Id = indexModel.Magazines.Count + ConfigurationData._AUTOINCREMENT;
            indexModel.Magazines.Add(magazine);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowMagazine(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Magazine magazine = (from t in indexModel.Magazines
                                 where t.Id == id
                                 select t).First();

            return View(magazine);
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult EditMagazine(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Magazine magazine = (from t in indexModel.Magazines
                                 where t.Id == id
                                 select t).First();
            return View(magazine);
        }

        [HttpPost]
        public ActionResult EditMagazine(Magazine newMagazine)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            foreach (Magazine magazine in indexModel.Magazines)
            {
                if (magazine.Id == newMagazine.Id)
                {
                    magazine.Name = newMagazine.Name;
                    magazine.Category = newMagazine.Category;
                    magazine.Price = newMagazine.Price;
                    magazine.Publisher = newMagazine.Publisher;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult DeleteMagazine(int? id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            Magazine newMagazine = (from t in indexModel.Magazines
                                    where t.Id == id
                                    select t).First();

            return PartialView(newMagazine);
        }

        [HttpPost, ActionName("DeleteMagazine")]
        public ActionResult DeleteConfirmedMagazine(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            indexModel.Magazines.Remove(indexModel.Magazines.Where(m => m.Id == id).First());
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult CreateNewspaper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewspaper(Newspaper newspaper)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            newspaper.Id = indexModel.Newspapers.Count + ConfigurationData._AUTOINCREMENT;
            indexModel.Newspapers.Add(newspaper);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowNewspaper(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Newspaper newspaper = (from t in indexModel.Newspapers
                                   where t.Id == id
                                   select t).First();

            return View(newspaper);
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult EditNewspaper(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Newspaper newspaper = (from t in indexModel.Newspapers
                                   where t.Id == id
                                   select t).First();
            return View(newspaper);
        }

        [HttpPost]
        public ActionResult EditNewspaper(Newspaper newNewspaper)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            foreach (Newspaper newspaper in indexModel.Newspapers)
            {
                if (newspaper.Id == newNewspaper.Id)
                {
                    newspaper.Name = newNewspaper.Name;
                    newspaper.Category = newNewspaper.Category;
                    newspaper.Price = newNewspaper.Price;
                    newspaper.Publisher = newNewspaper.Publisher;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult DeleteNewspaper(int? id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            Newspaper newspaper = (from t in indexModel.Newspapers
                                   where t.Id == id
                                   select t).First();

            return PartialView(newspaper);
        }

        [HttpPost, ActionName("DeleteNewspaper")]
        public ActionResult DeleteConfirmedNewspaper(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            indexModel.Newspapers.Remove(indexModel.Newspapers.Where(m => m.Id == id).First());
            return RedirectToAction("Index");
        }
    }
}