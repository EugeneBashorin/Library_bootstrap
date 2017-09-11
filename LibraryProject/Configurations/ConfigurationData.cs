using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Configurations
{
    public static class ConfigurationData
    {
        public const string _BOOK_KEY = "book";
        public const string _MAGAZINE_KEY = "magazine";
        public const string _NEWSPAPER_KEY = "newspaper";

        public const string _ATTRIBUTES_STATE_OFF = "hidden";
        public const string _ATTRIBUTES_STATE_ON = "display";

        public const int _AUTOINCREMENT = 1;

        public const string _ADMIN_ROLE = "admin";
        public const string _USER_ROLE = "user";
        public const string _ADMIN_EMAIL = "qwe@qwe.qwe";
        public const string _ADMIN_PASSWORD = "1_Qwerty";

        public static string booksWriteTxtPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/books.txt";
        public static string booksWriteXmlPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/books.xml";

        public static string magazinesWriteTxtPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/magazines.txt";
        public static string magazinesWriteXmlPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/magazines.xml";

        public static string newspapersWriteTxtPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/newspapers.txt";
        public static string newspapersWriteXmlPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data/newspapers.xml";
    }
}