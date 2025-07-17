using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    class Program
    {
        public class Book
        {
            public string Title { get; set; }
            public bool IsBorrowed { get; set; }

            public Book(string title)
            {
                Title = title;
                IsBorrowed = false;
            }
        }

        public class User
        {
            public string Name { get; set; }
            public List<Book> BorrowedBooks { get; set; }

            public User(string name)
            {
                Name = name;
                BorrowedBooks = new List<Book>();
            }

            public bool CanBorrow()
            {
                return BorrowedBooks.Count < 3;
            }
        }

        static void Main(string[] args)
        {
            List<Book> library = new List<Book>
            {
                new Book("The Great Gatsby"),
                new Book("To Kill a Mockingbird"),
                new Book("1984"),
                new Book("Pride and Prejudice"),
                new Book("Moby Dick")
            };

            User currentUser = new User("Selma");

            while (true)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Search for a book");
                Console.WriteLine("2. Borrow a book");
                Console.WriteLine("3. View borrowed books");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option (1-4): ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter book title to search: ");
                        string searchTitle = Console.ReadLine();
                        Book found = library.FirstOrDefault(b => b.Title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase));

                        if (found != null)
                        {
                            Console.WriteLine($" '{found.Title}' is in the collection.");
                        }
                        else
                        {
                            Console.WriteLine(" Book not found in the collection.");
                        }
                        break;

                    case "2":
                        if (!currentUser.CanBorrow())
                        {
                            Console.WriteLine(" Borrowing limit reached. Return a book to borrow a new one.");
                            break;
                        }

                        Console.Write("Enter book title to borrow: ");
                        string borrowTitle = Console.ReadLine();
                        Book bookToBorrow = library.FirstOrDefault(b => b.Title.Equals(borrowTitle, StringComparison.OrdinalIgnoreCase));

                        if (bookToBorrow == null)
                        {
                            Console.WriteLine(" Book not found.");
                        }
                        else if (bookToBorrow.IsBorrowed)
                        {
                            Console.WriteLine(" Book is already borrowed by someone else.");
                        }
                        else
                        {
                            bookToBorrow.IsBorrowed = true;
                            currentUser.BorrowedBooks.Add(bookToBorrow);
                            Console.WriteLine($" You have successfully borrowed '{bookToBorrow.Title}'.");
                        }
                        break;

                    case "3":
                        Console.WriteLine(" Borrowed Books:");
                        if (currentUser.BorrowedBooks.Count == 0)
                        {
                            Console.WriteLine("You have not borrowed any books.");
                        }
                        else
                        {
                            foreach (var book in currentUser.BorrowedBooks)
                            {
                                Console.WriteLine("- " + book.Title);
                            }
                        }
                        break;

                    case "4":
                        Console.WriteLine("Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}
