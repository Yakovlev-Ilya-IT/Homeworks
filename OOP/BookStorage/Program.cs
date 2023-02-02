using System;
using System.Collections.Generic;

namespace BookStorage
{
    internal class Program
    {
        private const string AddCommand = "1";
        private const string ShowSortCommand = "2";
        private const string ShowAllCommand = "3";
        private const string ExitCommand = "4";

        private const string SortByAuthorCommand = "1";
        private const string SortByTitleCommand = "2";
        private const string SortByReleaseYearCommand = "3";

        static void Main(string[] args)
        {
            BookStorage bookStorage = new BookStorage();

            bool isProgramRuning = true;

            while (isProgramRuning)
            {
                ShowMainMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case AddCommand:
                        AddBook(bookStorage);
                        break;

                    case ShowSortCommand:
                        ShowSortedBooks(bookStorage);
                        break;

                    case ShowAllCommand:
                        ShowAllBooks(bookStorage);
                        break;

                    case ExitCommand:
                        isProgramRuning = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет в списке, попробуйте снова");
                        break;
                }
            }
        }

        private static void ShowAllBooks(BookStorage bookStorage)
        {
            if (CheckCapacity(bookStorage) == false)
                return;

            bookStorage.ShowAll();
        }

        private static void ShowSortedBooks(BookStorage bookStorage)
        {
            if (CheckCapacity(bookStorage) == false)
                return;

            ShowSortingMenu();

            string input = Console.ReadLine();

            switch (input)
            {
                case SortByAuthorCommand:
                    ShowSortedBooksByAuthor(bookStorage);
                    break;

                case SortByTitleCommand:
                    ShowSortedBooksByTitle(bookStorage);
                    break;

                case SortByReleaseYearCommand:
                    ShowSortedBooksByReleaseYear(bookStorage);
                    break;

                default:
                    Console.WriteLine("Такой команды нет в списке");
                    break;
            }
        }

        private static void ShowSortedBooksByAuthor(BookStorage bookStorage)
        {
            Console.WriteLine("Введите автора");

            string input = Console.ReadLine();

            bookStorage.ShowBy((x, y) => x.Author.ToUpper() == y.ToUpper(), input);
        }

        private static void ShowSortedBooksByTitle(BookStorage bookStorage)
        {
            Console.WriteLine("Введите название");

            string input = Console.ReadLine();

            bookStorage.ShowBy((x, y) => x.Title.ToUpper() == y.ToUpper(), input);
        }

        private static void ShowSortedBooksByReleaseYear(BookStorage bookStorage)
        {
            Console.WriteLine("Введите год выпуска");

            string input = Console.ReadLine();

            bookStorage.ShowBy((x, y) => x.ReleaseYear.ToUpper() == y.ToUpper(), input);
        }

        private static bool CheckCapacity(BookStorage bookStorage)
        {
            if(bookStorage.Count == 0)
            {
                Console.WriteLine("Книг в хранилище нет");
                return false;
            }

            return true;
        }

        private static void AddBook(BookStorage bookStorage)
        {
            Console.WriteLine("Введите название книги");

            string title = Console.ReadLine();

            Console.WriteLine("Введите имя автора");

            string author = Console.ReadLine();

            Console.WriteLine("Введите год выпуска");

            string releaseYear = Console.ReadLine();

            bookStorage.Add(new Book(title, author, releaseYear));
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите одну из команд:");
            Console.WriteLine($"{AddCommand} - добавить книгу"); 
            Console.WriteLine($"{ShowSortCommand} - вывести отсортированные книги"); 
            Console.WriteLine($"{ShowAllCommand} - вывести все книги"); 
            Console.WriteLine($"{ExitCommand} - выйти");
            Console.WriteLine();
        }

        private static void ShowSortingMenu()
        {
            Console.WriteLine();
            Console.WriteLine("По какому параметру выводить книги?");
            Console.WriteLine($"{SortByAuthorCommand} - по автору");
            Console.WriteLine($"{SortByTitleCommand} - по названию");
            Console.WriteLine($"{SortByReleaseYearCommand} - по году выпуска");
            Console.WriteLine();
        }
    }

    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public string ReleaseYear { get; }

        public Book(string title, string author, string releaseYear)
        {
            Title = title;
            Author = author;
            ReleaseYear = releaseYear;
        }

        public void ShowInfo() => Console.WriteLine($"Название - {Title}, автор - {Author}, год выпуска - {ReleaseYear}");
    }

    public class BookStorage
    {
        private List<Book> _books = new List<Book>();

        public int Count => _books.Count;

        public void Add(Book book) => _books.Add(book);

        public bool TryRemoveAt(int index)
        {
            if(index >= _books.Count || index < 0)
            {
                Console.WriteLine("Введенный номер книги за пределами хранлища");
                return false;
            }

            _books.RemoveAt(index);
            return true;
        }

        public void ShowAll()
        {
            foreach (Book book in _books)
                book.ShowInfo();
        }

        public void ShowBy(Func<Book, string ,bool> comparer, string comparedValue)
        {
            int matchCounter = 0;

            foreach (Book book in _books)
            {
                if (comparer(book, comparedValue))
                {
                    book.ShowInfo();
                    matchCounter++;
                }
            }

            if(matchCounter == 0)
                Console.WriteLine("Таких книг не найдено");
        }
    }
}
