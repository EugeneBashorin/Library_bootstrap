using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    //Serializable]
    class Book : PrintEdition
    {
        public string Author { get; set; }
    }
}
