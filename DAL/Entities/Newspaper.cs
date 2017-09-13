using System;

namespace DAL.Entities
{
    //[Serializable]
    public class Newspaper : PrintEdition
    {
        public string Category { get; set; }
    }
}