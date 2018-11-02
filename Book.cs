using System;
using System.Linq;
using System.Collections.Generic;


namespace CIDM3312MidtermFall2018
{
    public class Book
    {
        //PK
        public int BookID {get; set;}
        public string BookTitle {get; set;}
        public string Publisher {get; set;}
        public DateTime PublishDate {get; set;}
        public int Pages {get; set;}
        //FK
        public ICollection<Author> Authors { get; set; }
        public string AuthorID {get; set;}
        public Author AuthorFName {get; set;}
        public Author AuthorLName {get; set;}

        public override string ToString()
        {
            return $"{BookID} - {BookTitle}";
        }

    }
}