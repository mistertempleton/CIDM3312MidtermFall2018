using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIDM3312MidtermFall2018
{
    public class Author
    {
        //PK
        [ForeignKey("Book")]
        public int AuthorID {get; set;}
        public string AuthorFName {get; set;}
        public string AuthorLName {get; set;}

        // Override ToString for viewing Author Data
        public override string ToString()
        {
            return $"{AuthorID} - {AuthorFName} {AuthorLName}";
        }
    }
}