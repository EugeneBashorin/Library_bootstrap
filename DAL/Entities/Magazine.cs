using System;

namespace DAL.Entities
{
    //[Serializable]
    public class Magazine : PrintEdition
    {
        public string Category { get; set; }
    }
}