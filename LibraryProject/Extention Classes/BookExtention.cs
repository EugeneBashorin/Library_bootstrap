using LibraryProject.Configurations;
using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LibraryProject.Extention_Classes
{
    public static class BookExtention
    {
        public static void GetTxtList(this List<Book> list)
        {
            StringBuilder result = new StringBuilder(130);

            if (list.Count > 0)
            {
                foreach (Book item in list)
                {
                    result.AppendLine($"Name: {item.Name} Author: {item.Author} Publisher: {item.Publisher} Price: {item.Price.ToString()}");
                }
            }

            using (StreamWriter sw = new StreamWriter(ConfigurationData.booksWriteTxtPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(result);
            }
        }

        public static void GetXmlList(this List<Book> xmlBooksList)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));

            using (FileStream fs = new FileStream(ConfigurationData.booksWriteXmlPath, FileMode.Create))
            {
                xs.Serialize(fs, xmlBooksList);
            }
        }

        public static void SetBookListToDb(this List<Book> bookList, string connectionString)
        {
            StringBuilder insertSqlExpression = new StringBuilder(300);
            insertSqlExpression.Append("INSERT INTO Books ([Name], [Author], [Publisher],[Price]) VALUES");

            foreach (Book item in bookList)
            {
                if (item == bookList.Last())
                {
                    insertSqlExpression.Append($"('{item.Name}','{item.Author}','{item.Publisher}','{item.Price}');");
                }
                else
                {
                    insertSqlExpression.Append($"('{item.Name}','{item.Author}','{item.Publisher}','{item.Price}'),");
                }
            }

            string InsertSqlExpression = insertSqlExpression.ToString();
            string DeleteSqlExpression = "DELETE FROM Books";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(DeleteSqlExpression, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }

                command = new SqlCommand(InsertSqlExpression, connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}