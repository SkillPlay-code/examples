using System;
using System.Collections.Generic;
using System.Linq;

namespace SkillPlay
{
    public class Program
    {
        private const string CommandShowAllBooks = "1";
        private const string CommandAddBook = "2";
        private const string CommandRemoveBook = "3";
        private const string CommandSearchBook = "4";
        private const string CommandExit = "5";

        static void Main(string[] args)
        {
            Storage bookStorage = new Storage();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Меню: \n" +
                                  $"{CommandShowAllBooks}. Показать все книги в хранилище \n" +
                                  $"{CommandAddBook}. Добавить книгу \n" +
                                  $"{CommandRemoveBook}. Убрать книгу \n" +
                                  $"{CommandSearchBook}. Поиск книги \n" +
                                  $"{CommandExit}. Выход");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case CommandShowAllBooks:
                        bookStorage.ShowAllBooks();
                        break;
                        
                    case CommandAddBook:
                        bookStorage.AddBook();
                        break;
                        
                    case CommandRemoveBook:
                        bookStorage.RemoveBook();
                        break;
                        
                    case CommandSearchBook:
                        bookStorage.SearchBooks();
                        break;
                        
                    case CommandExit:
                        isWork = false;
                        break;
                        
                    default:
                        Console.WriteLine("Некорректный ввод\n");
                        break;
                }
            }
        }
    }

    class Book
    {
        public Book(string title, string author, int yearRelease, int index)
        {
            Title = title;
            Author = author;
            YearRelease = yearRelease;
            Index = index;
        }
        
        public string Title { get; }
        public string Author { get; }
        public int YearRelease { get; }
        public int Index { get; }

        public void ShowInfo()
        {
            Console.WriteLine($"Название книги: {Title} \nАвтор: {Author} \nГод издания: {YearRelease}\n");
        }
    }

    class Storage
    {
        private const string CommandSearchTitle = "1";
        private const string CommandSearchAuthor = "2";
        private const string CommandSearchYearRelease = "3";
        private const string CommandExit = "4";
        private const string CommandDeleteBook = "1";

        private List<Book> _books = new List<Book>();
        
        public void SearchBooks()
        {
            if (_books.Count > 0)
            {
                bool isWork = true;

                while (isWork)
                {
                    Console.WriteLine($"{CommandSearchTitle}. По названию \n" +
                                      $"{CommandSearchAuthor}. По автору \n" +
                                      $"{CommandSearchYearRelease}. По году релиза \n" +
                                      $"{CommandExit}. Выход");

                    switch (Console.ReadLine())
                    {
                        case CommandSearchTitle:
                            SearchByTitleBook();
                            break;
                            
                        case CommandSearchAuthor:
                            SearchBooksByAuthor();
                            break;
                            
                        case CommandSearchYearRelease:
                            SearchBooksByYearRelease();
                            break;
                            
                        case CommandExit:
                            isWork = false;
                            break;
                            
                        default:
                            Console.WriteLine("Некорректный ввод\n");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("В хранилище ещё нет книг\n");
            }
        }

        public void AddBook()
        {
            Console.WriteLine("Введите название книги\n");
            string title = Console.ReadLine();

            Console.WriteLine("Введите автора книги: \n");
            string author = Console.ReadLine();

            Console.WriteLine("Введите год релиза книги: \n");
            
            if (int.TryParse(Console.ReadLine(), out int yearRelease))
            {
                int index = _books.Count > 0 ? _books.Max(books => books.Index) + 1 : 1;
                Book book = new Book(title, author, yearRelease, index);
                _books.Add(book);
                Console.WriteLine("Книга добавлена!\n");
            }
            else
            {
                Console.WriteLine("Некорректный ввод\n");
            }
        }

        public void RemoveBook()
        {
            if (_books.Count != 0)
            {
                Console.WriteLine("Введите индекс книги, которую хотите удалить:");
    
                if (int.TryParse(Console.ReadLine(), out int bookIndex))
                {
                    Book bookToRemove = _books.FirstOrDefault(book => book.Index == bookIndex);
    
                    if (bookToRemove != null)
                    {
                        Console.WriteLine($"Найдена книга с индексом {bookIndex}");
                        bookToRemove.ShowInfo();
                        Console.WriteLine($"Удалить данную книгу? \n{CommandDeleteBook} - Да");
    
                        string input = Console.ReadLine();
    
                        if (input == CommandDeleteBook)
                        {
                            DeleteBookFromStorage(bookToRemove);
                            Console.WriteLine("Книга удалена!");
                        }
                        else
                        {
                            Console.WriteLine("Удаление отменено.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Книги с таким индексом не существует.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Введите число.");
                }
            }
            else
            {
                Console.WriteLine("В хранилище ещё нет книг");
            }
        }

        private void DeleteBookFromStorage(Book book)
        {
            _books.Remove(book);
        }

        public void ShowAllBooks()
        {
            if (_books.Count > 0)
            {
                int index = 1;
                foreach (Book book in _books)
                {
                    Console.WriteLine($"Книга ID{index}");
                    book.ShowInfo();
                    index++;
                }
            }
            else
            {
                Console.WriteLine("В хранилище ещё нет книг\n");
            }
        }

        private void SearchByTitleBook()
        {
            Console.WriteLine("Введите часть названия книги\n");
            string input = Console.ReadLine();
        
            List<Book> foundBooks = _books.Where(book => book.Title.IndexOf(input, StringComparison.OrdinalIgnoreCase) != -1).ToList();
        
            DisplaySearchResults(foundBooks);
        }

        private void SearchBooksByAuthor()
        {
            Console.WriteLine("Введите автора\n");
            string input = Console.ReadLine();

            List<Book> foundBooks = _books.Where(book => book.Author.Equals(input, StringComparison.OrdinalIgnoreCase)).ToList();

            DisplaySearchResults(foundBooks);
        }

        private void SearchBooksByYearRelease()
        {
            Console.WriteLine("Введите год релиза\n");
            
            if (int.TryParse(Console.ReadLine(), out int yearRelease))
            {
                List<Book> foundBooks = _books.Where(book => book.YearRelease == yearRelease).ToList();

                DisplaySearchResults(foundBooks);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Введите число.");
            }
        }

        private void DisplaySearchResults(List<Book> foundBooks)
        {
            if (foundBooks.Count > 0)
            {
                Console.WriteLine("Результаты поиска\n");
                foreach (Book book in foundBooks)
                {
                    book.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("По вашему запросу ничего не найдено\n");
            }
        }
    }
}
