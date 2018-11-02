using System;
using System.Linq;
using System.Collections.Generic;

namespace CIDM3312MidtermFall2018
{
    class Program
    {
        static void Main(string[] args)
        {
            SeedDatabase();

            // LINQ WHERE METHODS

            // In your program, connect to the database and show all records in the Books table, 
            //print this to the Console
            BasicFiltersQuerySyntaxA();

            // In your program, connect to the database and show all records of Books Published by "APress"
            BasicFiltersQuerySyntaxB();

            // In your program, connect to the database and show all records of Books
            // whose author's first name is the shortest 
            BasicFiltersWhereMethodSyntaxA();

            // LINQ FIND METHOD

            // In your program, connect to the database and find the first book by an author named "Adam" 
            // and print that record to the screen
            FirstAuthorAdam();

            // In your program, connect to the database and find the first book whose page count is greater 
            // than 1000
            FirstBookwith1000Pages();

            // LINQ ORDERBY METHOD

            // Connect to the database and show all Books sorted by Author's last name
            OrderByAuthorLastName();

            // Connect to the database and show all Books sorted by book title descending
            OrderByBooksDescending();

            // LINQ GROUPBY AND ORDERBY METHODS

            // Connect to the database and show all Books Grouped by publisher
            GroupByMethodA();

            // Connect to the database and show all Books Grouped by publisher and sorted 
            // by Author's last name
            GroupByQueryA();
        }

        
        public static void BasicFiltersQuerySyntaxA()
        {
            using(var db = new AppDbContext())
            {
                             
                var filteredResult = from s in db.Books
                                    select s;

                PrintList(filteredResult);
            }
        } 

        
        public static void BasicFiltersQuerySyntaxB()
        {
            using(var db = new AppDbContext())
            {
                                
                var filteredResult = from s in db.Books
                                    where s.Publisher == "Apress"
                                    select s.BookTitle;

                PrintList(filteredResult);
            }
        } 

        
        public static void BasicFiltersWhereMethodSyntaxA()
        {
            using(var db = new AppDbContext())
            {
               
                var filteredResult = from t in db.Books 
                                        orderby t.AuthorID
                                        select t;

                PrintList(filteredResult);
            }
        }

        
        public static void FirstAuthorAdam(){
            using(var db = new AppDbContext())
            {
                var filteredResult = db.Books.Where(s => s.AuthorFName == "Adam").FirstOrDefault();

                PrintList(filteredResult);
            }
        }

        
        public static void FirstBookwith1000Pages(){
            using(var db = new AppDbContext())
            {
                var filteredResult = db.Books.Where(t => t.Pages >= 1000);

                Console.WriteLine(filteredResult);
            }
        }

        
        public static void OrderByAuthorLastName(){
            using(var db = new AppDbContext())
            {
                var filteredResult = from t in db.Books
                                        orderby t.AuthorLName.FirstOrDefault()
                                        select t;

                Console.WriteLine(filteredResult);
            }
        }

        
        public static void OrderByBooksDescending(){
            using(var db = new AppDbContext())
            {
                var filteredResult = from t in db.Books
                                        orderby t.BookTitle descending
                                        select t;

                Console.WriteLine(filteredResult);
            }
        }

        

        public static void GroupByMethodA()
        {
            using(var db = new AppDbContext())
            {
           
                var groupedResult = db.Books.GroupBy(s => s.Publisher);

                foreach (var s in groupedResult)
                {
                    Console.WriteLine($"Publisher: {s.Key}");

                    foreach(Book t in groupedResult)
                    {
                        Console.WriteLine(t);
                    }
                }

                
            }              
        }

        
        public static void GroupByQueryA()
        {
            using(var db = new AppDbContext())
            {
           
                var groupedResult = db.Books.OrderBy(t => t.AuthorLName).GroupBy(s => s.AuthorLName);

                PrintList(groupedResult);

                
            }            
        }
        public static void SeedDatabase()
        {
            using(var db = new AppDbContext())
            {
                try
                {
                    //first, if it is there, delete it
                    //db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();

                    if(!db.Authors.Any() && !db.Books.Any())
                    {
                        IList<Author> authorList = new List<Author>()
                        {
                            new Author() { AuthorID = 1, AuthorFName = "Adam", AuthorLName = "Freeman"},
                            new Author() { AuthorID = 2, AuthorFName = "Haishi", AuthorLName = "Bai"}
                        };

                        db.Authors.AddRange(authorList);

                        IList<Book> bookList = new List<Book>() { 
                            new Book() {

                            BookID = 1, 
                            BookTitle = "Pro ASP.NET Core MVC 2 7th ed. Edition",
                            Publisher = "Apress", 
                            PublishDate = DateTime.Parse("2017-10-25"),
                            Pages = 1017,
                            AuthorID = "1"},

                            new Book() {

                            BookID = 2, 
                            BookTitle = "Pro Angular 6 3rd Edition",
                            Publisher = "Apress",
                            PublishDate = DateTime.Parse("2018-10-10"), 
                            Pages = 776, 
                            AuthorID = "1"},
                            new Book() { 

                            BookID = 3,
                            BookTitle = "Programming Microsoft Azure Service Fabric (Developer Reference) 2nd Edition",
                            Publisher = "Microsoft Press", 
                            PublishDate = DateTime.Parse("2018-05-25"),
                            Pages = 528, 
                            AuthorID = "2"},
                        };  

                        db.Books.AddRange(bookList);

                        db.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("There are exisiting items in the database.");
                    }
                }
                catch(Exception exp)
                {
                    Console.Error.WriteLine(exp.Message);
                }
            }
        }       

        public static void PrintList(IEnumerable<Object> list)
        {
            foreach(var s in list)
            {
                Console.WriteLine(s);
            }
        }
    }
}
