using LibraryProject.Configurations;
using LibraryProject.Extention_Classes;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using Entityes.Entities;
using BusinessLogicLayer;

namespace LibraryProject.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        [HttpGet]
        public ActionResult Index(string bookPublisher = "All", string magazinePublisher = "All", string newspaperPublisher = "All")
        {
            CheckRole();
            IndexModel indexModel = new IndexModel();
            Initialize(indexModel);
            indexModel = CheckPublisher(indexModel, bookPublisher, magazinePublisher, newspaperPublisher);

            return View(indexModel);
        }

        private void CheckRole()
        {
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
        }

        private IndexModel Initialize(IndexModel model)
        {
            model.BooksFilterModel = new BooksFilterModel();
            model.BooksFilterModel.BooksPublisher = new SelectList(new List<string>() { "All", "O.Reilly", "Syncfusion", "Williams", "Wrox", "ITVDN" });
            model.BooksFilterModel.Books = new List<Book>();

            model.MagazineFilterModel = new MagazineFilterModel();
            model.MagazineFilterModel.MagazinesPublisher = new SelectList(new List<string>() { "All", "Williams", "Mag Group", "Stanley & Co" });
            model.MagazineFilterModel.Magazines = new List<Magazine>();

            model.NewspaperFilterModel = new NewspaperFilterModel();
            model.NewspaperFilterModel.NewspapersPublisher = new SelectList(new List<string>() { "All", "Red Octouber", "Ronald", "West-Cost", "Croxy" });
            model.NewspaperFilterModel.Newspapers = new List<Newspaper>();

            return model;
        }

        public IndexModel CheckPublisher( IndexModel model, string bookPublisher = "All", string magazinePublisher = "All", string newspaperPublisher = "All")
        {
            
            if (!String.IsNullOrEmpty(bookPublisher) && !bookPublisher.Equals("All"))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if (connection != null)
                    {
                        string bookSelectExpression = $"SELECT * FROM Books WHERE Publisher = '{bookPublisher}'";
                        SqlCommand command = new SqlCommand(bookSelectExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.BooksFilterModel.Books.Add(new Book { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Author = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                            }
                        }
                    }
                }
            }
            else
            {
                model.BooksFilterModel.Books = BLL.GetAllBooks();//GetAllBooks();
            }

            if (!String.IsNullOrEmpty(magazinePublisher) && !magazinePublisher.Equals("All"))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();                 
                    if (connection != null)
                    {
                        string magazineSelectExpression = $"SELECT * FROM Magazines WHERE Publisher = '{magazinePublisher}'";
                        SqlCommand command = new SqlCommand(magazineSelectExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.MagazineFilterModel.Magazines.Add(new Magazine { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                            }
                        }
                    }
                }
            }
            else
            {
                model.MagazineFilterModel.Magazines = GetAllMagazines();
            }
            if (!String.IsNullOrEmpty(newspaperPublisher) && !newspaperPublisher.Equals("All"))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();                 
                    if (connection != null)
                    {
                        string newspaperSelectExpression = $"SELECT * FROM Newspapers WHERE Publisher = '{newspaperPublisher}'";
                        SqlCommand command = new SqlCommand(newspaperSelectExpression, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.NewspaperFilterModel.Newspapers.Add(new Newspaper { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                            }
                        }
                    }
                }
            }
            else
            {
                model.NewspaperFilterModel.Newspapers = GetAllNewspapers();
            }
            return model;
        }

        private List<Entityes.Entities.Magazine> GetAllMagazines()
        {
            List<Magazine> magazinesList;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                magazinesList = new List<Magazine>();
                if (connection != null)
                {
                    Magazine magazine = new Magazine();
                    string magazinesSelectExpression = "SELECT * FROM Magazines";
                    SqlCommand command = new SqlCommand(magazinesSelectExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            magazinesList.Add(new Magazine { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                        }
                    }
                }
            }
            return magazinesList;
        }

        private List<Newspaper> GetAllNewspapers()
        {
            List<Newspaper> newspapersList;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                newspapersList = new List<Newspaper>();
                if (connection != null)
                {
                    Newspaper newspaper = new Newspaper();
                    string newspaperSelectExpression = "SELECT * FROM Newspapers";
                    SqlCommand command = new SqlCommand(newspaperSelectExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newspapersList.Add(new Newspaper { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Category = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
                        }
                    }
                }
            }
            return newspapersList;
        }

        public ActionResult GetBooksList()
        {
            List<Book> bookList = BLL.GetAllBooks();
            bookList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewsPapersList()
        {
            List<Newspaper> newspaperList = GetAllNewspapers();
            newspaperList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesList()
        {
            List<Magazine> MagazinesList = GetAllMagazines();
            MagazinesList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetBooksXmlList()
        {
            List<Book> bookList = BLL.GetAllBooks();
            bookList.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewspapersXmlList()
        {
            List<Newspaper> newspaperList = GetAllNewspapers();
            newspaperList.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesXmlList()
        {
            List<Magazine> magazineList = GetAllMagazines();
            magazineList.GetXmlList();
            return RedirectToAction("Index");
        }

        [HttpGet]/*****************************************?????How can save Db To exist Db?????**************************************************/
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseBookList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.BooksFilterModel.Books;
            bookList.SetBookListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [HttpGet]/*****************************************?????How can save Db To exist Db?????**************************************************/
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseMagazineList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> magazineList = indexModel.MagazineFilterModel.Magazines;
            magazineList.SetMagazineListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [HttpGet]/*****************************************?????How can save Db To exist Db?????**************************************************/
        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        public ActionResult GetDatabaseNewspaperList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Newspaper> newspaperList = indexModel.NewspaperFilterModel.Newspapers;
            newspaperList.SetNewspaperListToDb(connectionString);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            string createBookExpression = $"INSERT INTO Books([Name], [Author], [Publisher],[Price]) VALUES('{book.Name}','{book.Author}','{book.Publisher}','{book.Price}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(createBookExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowBook(int id)
        {
            Book book = new Book();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchBookExpression = $"SELECT * FROM Books WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchBookExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            book.Id = (int)reader.GetValue(0);
                            book.Name = (string)reader.GetValue(1);
                            book.Author = (string)reader.GetValue(2);
                            book.Publisher = (string)reader.GetValue(3);
                            book.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return View(book);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult EditBook(int id)
        {
            Book book = BLL.GetItemById(id);//new Book();
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    if (connection != null)
            //    {
            //        string searchBookExpression = $"SELECT * FROM Books WHERE Id = '{id}'";
            //        SqlCommand command = new SqlCommand(searchBookExpression, connection);
            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                book.Id = (int)reader.GetValue(0);
            //                book.Name = (string)reader.GetValue(1);
            //                book.Author = (string)reader.GetValue(2);
            //                book.Publisher = (string)reader.GetValue(3);
            //                book.Price = (int)reader.GetValue(4);
            //            }
            //        }
            //    }
            //}
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(int Id, Book newBook)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string editBookExpression = $"UPDATE Books SET Name = '{newBook.Name}', Author = '{newBook.Author}', Publisher = '{newBook.Publisher}', Price = '{newBook.Price}' WHERE Id = '{Id}'";
                    SqlCommand command = new SqlCommand(editBookExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return RedirectToAction("Index");
        }


        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteBook(int? id)
        {
            Book book = new Book();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchBookExpression = $"SELECT * FROM Books WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchBookExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            book.Id = (int)reader.GetValue(0);
                            book.Name = (string)reader.GetValue(1);
                            book.Author = (string)reader.GetValue(2);
                            book.Publisher = (string)reader.GetValue(3);
                            book.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return PartialView(book);
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteConfirmedBook(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string deleteBookExpression = $"DELETE FROM Books WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(deleteBookExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult CreateMagazine()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMagazine(Magazine magazine)
        {
            string createMagazineExpression = $"INSERT INTO Magazines([Name], [Category], [Publisher],[Price]) VALUES('{magazine.Name}','{magazine.Category}','{magazine.Publisher}','{magazine.Price}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(createMagazineExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowMagazine(int id)
        {
            Magazine magazine = new Magazine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchMagazineExpression = $"SELECT * FROM Magazines WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchMagazineExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            magazine.Id = (int)reader.GetValue(0);
                            magazine.Name = (string)reader.GetValue(1);
                            magazine.Category = (string)reader.GetValue(2);
                            magazine.Publisher = (string)reader.GetValue(3);
                            magazine.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
           return View(magazine);
        }

        [HttpGet]
        public ActionResult EditMagazine(int id)
        {
            Magazine magazine = new Magazine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchMagazineExpression = $"SELECT * FROM Magazines WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchMagazineExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            magazine.Id = (int)reader.GetValue(0);
                            magazine.Name = (string)reader.GetValue(1);
                            magazine.Category = (string)reader.GetValue(2);
                            magazine.Publisher = (string)reader.GetValue(3);
                            magazine.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return View(magazine);
        }

        [HttpPost]
        public ActionResult EditMagazine(int Id, Magazine newMagazine)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string editMagazineExpression = $"UPDATE Magazines SET Name = '{newMagazine.Name}', Category = '{newMagazine.Category}', Publisher = '{newMagazine.Publisher}', Price = '{newMagazine.Price}' WHERE Id = '{Id}'";
                    SqlCommand command = new SqlCommand(editMagazineExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }          
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteMagazine(int? id)
        {
            Magazine newMagazine = new Magazine();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchMagazineExpression = $"SELECT * FROM Magazines WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchMagazineExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newMagazine.Id = (int)reader.GetValue(0);
                            newMagazine.Name = (string)reader.GetValue(1);
                            newMagazine.Category = (string)reader.GetValue(2);
                            newMagazine.Publisher = (string)reader.GetValue(3);
                            newMagazine.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return PartialView(newMagazine);
        }

        [HttpPost, ActionName("DeleteMagazine")]
        public ActionResult DeleteConfirmedMagazine(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string deleteMagazineExpression = $"DELETE FROM Magazines WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(deleteMagazineExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult CreateNewspaper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewspaper(Newspaper newspaper)
        {
            string createNewspaperExpression = $"INSERT INTO Newspapers([Name], [Category], [Publisher],[Price]) VALUES('{newspaper.Name}','{newspaper.Category}','{newspaper.Publisher}','{newspaper.Price}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(createNewspaperExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowNewspaper(int id)
        {
            Newspaper newspaper = new Newspaper();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchNewspaperExpression = $"SELECT * FROM Newspapers WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchNewspaperExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newspaper.Id = (int)reader.GetValue(0);
                            newspaper.Name = (string)reader.GetValue(1);
                            newspaper.Category = (string)reader.GetValue(2);
                            newspaper.Publisher = (string)reader.GetValue(3);
                            newspaper.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return View(newspaper);
        }


        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult EditNewspaper(int id)
        {
            Newspaper newspaper = new Newspaper();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchNewspaperExpression = $"SELECT * FROM Newspapers WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchNewspaperExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newspaper.Id = (int)reader.GetValue(0);
                            newspaper.Name = (string)reader.GetValue(1);
                            newspaper.Category = (string)reader.GetValue(2);
                            newspaper.Publisher = (string)reader.GetValue(3);
                            newspaper.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }
            return View(newspaper);
        }

        [HttpPost]
        public ActionResult EditNewspaper(int Id, Newspaper newNewspaper)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string editNewspaperExpression = $"UPDATE Newspapers SET Name = '{newNewspaper.Name}', Category = '{newNewspaper.Category}', Publisher = '{newNewspaper.Publisher}', Price = '{newNewspaper.Price}' WHERE Id = '{Id}'";
                    SqlCommand command = new SqlCommand(editNewspaperExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }          
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = ConfigurationData._ADMIN_ROLE)]
        [HttpGet]
        public ActionResult DeleteNewspaper(int? id)
        {
            Newspaper newspaper = new Newspaper();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string searchNewspaperExpression = $"SELECT * FROM Newspapers WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(searchNewspaperExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newspaper.Id = (int)reader.GetValue(0);
                            newspaper.Name = (string)reader.GetValue(1);
                            newspaper.Category = (string)reader.GetValue(2);
                            newspaper.Publisher = (string)reader.GetValue(3);
                            newspaper.Price = (int)reader.GetValue(4);
                        }
                    }
                }
            }

            return PartialView(newspaper);
        }

        [HttpPost, ActionName("DeleteNewspaper")]
        public ActionResult DeleteConfirmedNewspaper(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection != null)
                {
                    string deleteNewspaperExpression = $"DELETE FROM Newspapers WHERE Id = '{id}'";
                    SqlCommand command = new SqlCommand(deleteNewspaperExpression, connection);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}

// private List<Book> GetAllBooks()
//{
//    List<Book> booksList;
//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        connection.Open();
//        booksList = new List<Book>();
//        if (connection != null)
//        {

//            Book book = new Book();
//            string newspaperSelectExpression = "SELECT * FROM Books";
//            SqlCommand command = new SqlCommand(newspaperSelectExpression, connection);
//            SqlDataReader reader = command.ExecuteReader();
//            if (reader.HasRows)
//            {
//                while (reader.Read())
//                {
//                    booksList.Add(new Book { Id = (int)reader.GetValue(0), Name = (string)reader.GetValue(1), Author = (string)reader.GetValue(2), Publisher = (string)reader.GetValue(3), Price = (int)reader.GetValue(4) });
//                }
//            }
//        }
//    }
//    return booksList;
//}