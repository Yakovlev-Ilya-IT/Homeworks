using System;
using System.Collections.Generic;

namespace BookStorage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BookStorage bookStorage = new BookStorage();

            bookStorage.Work();
        }
    }

    public class Book
    {
        public Book(string title, string author, int releaseYear)
        {
            Title = title;
            Author = author;
            ReleaseYear = releaseYear;
        }

        public string Title { get; }
        public string Author { get; }
        public int ReleaseYear { get; }

        public void ShowInfo() => Console.WriteLine($"Название - {Title}, автор - {Author}, год выпуска - {ReleaseYear}");
    }

    public class BookStorage
    {
        private const string AddCommand = "1";
        private const string RemoveCommand = "2";
        private const string ShowSortCommand = "3";
        private const string ShowAllCommand = "4";
        private const string ExitCommand = "5";

        private const string SortByAuthorCommand = "1";
        private const string SortByTitleCommand = "2";
        private const string SortByReleaseYearCommand = "3";

        private List<Book> _books;

        public BookStorage()
        {
            _books = new List<Book>();

            _books.Add(new Book("Преступление и наказание", "Достоевский", 1865));
            _books.Add(new Book("Три товарища", "Ремарк", 1936));
            _books.Add(new Book("Властелин колец", "Толкин", 1954));
        }

        private bool IsEmpty => _books.Count == 0;

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                ShowMainMenu();

                string input = Console.ReadLine();

                switch (input)
                {
                    case AddCommand:
                        AddBook();
                        break;

                    case RemoveCommand:
                        RemoveBook();
                        break;

                    case ShowSortCommand:
                        ShowSortedBooks();
                        break;

                    case ShowAllCommand:
                        ShowAllBooks();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды нет в списке, попробуйте снова");
                        break;
                }
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Выберите одну из команд:");
            Console.WriteLine($"{AddCommand} - добавить книгу");
            Console.WriteLine($"{RemoveCommand} - убрать книгу по номеру");
            Console.WriteLine($"{ShowSortCommand} - вывести отсортированные книги");
            Console.WriteLine($"{ShowAllCommand} - вывести все книги");
            Console.WriteLine($"{ExitCommand} - выйти");
            Console.WriteLine();
        }

        private void ShowSortingMenu()
        {
            Console.WriteLine();
            Console.WriteLine("По какому параметру выводить книги?");
            Console.WriteLine($"{SortByAuthorCommand} - по автору");
            Console.WriteLine($"{SortByTitleCommand} - по названию");
            Console.WriteLine($"{SortByReleaseYearCommand} - по году выпуска");
            Console.WriteLine();
        }

        private void AddBook()
        {
            Console.WriteLine("Введите название книги");

            string title = Console.ReadLine();

            Console.WriteLine("Введите имя автора");

            string author = Console.ReadLine();

            Console.WriteLine("Введите год выпуска");

            if (uint.TryParse(Console.ReadLine(), out uint releaseYear))
            {
                _books.Add(new Book(title, author, (int)releaseYear));

                Console.WriteLine("Книга успешно добавлена");
                return;
            }

            ShowRequirementsMessageForEnteringReleaseDate();
        }

        private void RemoveBook()
        {
            Console.WriteLine("Введите номер книги, которую хотите удалить");

            if(int.TryParse(Console.ReadLine(), out int bookNumber))
            {
                bookNumber -= 1;

                if(bookNumber < 0 || bookNumber >= _books.Count)
                {
                    Console.WriteLine("Введенный номер книги за пределами хранлища");
                    return;
                }

                _books.RemoveAt(bookNumber);
                Console.WriteLine("Книга успешно удалена");
                return;
            }

            Console.WriteLine("Пожалуйста, введите НОМЕР книги");
        }

        private void ShowSortedBooks()
        {
            if (IsEmpty)
            {
                ShowEmptyStorageMessage();
                return;
            }

            ShowSortingMenu();

            string input = Console.ReadLine();

            switch (input)
            {
                case SortByAuthorCommand:
                    ShowSortedBooksByAuthor();
                    break;

                case SortByTitleCommand:
                    ShowSortedBooksByTitle();
                    break;

                case SortByReleaseYearCommand:
                    ShowSortedBooksByReleaseYear();
                    break;

                default:
                    Console.WriteLine("Такой команды нет в списке");
                    break;
            }
        }

        private void ShowSortedBooksByAuthor()
        {
            Console.WriteLine("Введите автора");

            string input = Console.ReadLine();

            ShowBy((book, requestAuthor) => book.Author.ToUpper() == requestAuthor.ToUpper(), input);
        }

        private void ShowSortedBooksByTitle()
        {
            Console.WriteLine("Введите название");

            string input = Console.ReadLine();

            ShowBy((book, requestTitle) => book.Title.ToUpper() == requestTitle.ToUpper(), input);
        }

        private void ShowSortedBooksByReleaseYear()
        {
            Console.WriteLine("Введите год выпуска");

            if (uint.TryParse(Console.ReadLine(), out uint releaseYear))
            {
                ShowBy((book, requestReleaseYear) => book.ReleaseYear.ToString() == requestReleaseYear.ToString(), releaseYear.ToString());
                return;
            }

            ShowRequirementsMessageForEnteringReleaseDate();
        }

        private void ShowBy(Func<Book, string, bool> comparer, string comparedValue)
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

            if (matchCounter == 0)
                Console.WriteLine("Таких книг не найдено");
        }

        private void ShowAllBooks()
        {
            if (IsEmpty)
            {
                ShowEmptyStorageMessage();
                return;
            }

            for (int i = 0; i < _books.Count; i++)
            {
                Console.Write($"{i + 1} -");
                _books[i].ShowInfo();
            }
        }

        private void ShowRequirementsMessageForEnteringReleaseDate() => Console.WriteLine("Нужно ввести неотрицательное число");

        private void ShowEmptyStorageMessage() => Console.WriteLine("Книг в хранилище нет");
    }
}
