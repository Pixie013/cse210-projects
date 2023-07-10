using System;
using System.Collections.Generic;

class LibraryItem
{
    public string Title { get; set; }
    public bool IsAvailable { get; set; }

    public virtual void PrintDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Availability: {(IsAvailable ? "Available" : "Not Available")}");
    }
}

class Book : LibraryItem
{
    public string Author { get; set; }

    public override void PrintDetails()
    {
        base.PrintDetails();
        Console.WriteLine($"Author: {Author}");
    }
}

class DVD : LibraryItem
{
    public string Director { get; set; }

    public override void PrintDetails()
    {
        base.PrintDetails();
        Console.WriteLine($"Director: {Director}");
    }
}

class Library
{
    private List<LibraryItem> items;

    public Library()
    {
        items = new List<LibraryItem>();
    }

    public void AddItem(LibraryItem item)
    {
        items.Add(item);
    }

    public void RemoveItem(LibraryItem item)
    {
        items.Remove(item);
    }

    public LibraryItem FindItemByTitle(string title)
    {
        return items.Find(item => item.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public void PrintAvailableItems()
    {
        Console.WriteLine("Available Items:");
        foreach (var item in items)
        {
            if (item.IsAvailable)
            {
                item.PrintDetails();
                Console.WriteLine();
            }
        }
    }
}

class UserInterface
{
    private Library library;

    public UserInterface()
    {
        library = new Library();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add DVD");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Print Available Items");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    AddDVD();
                    break;
                case "3":
                    RemoveItem();
                    break;
                case "4":
                    PrintAvailableItems();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void AddBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();

        Console.Write("Enter book author: ");
        string author = Console.ReadLine();

        Book book = new Book
        {
            Title = title,
            Author = author,
            IsAvailable = true
        };

        library.AddItem(book);
        Console.WriteLine("Book added successfully.");
    }

    private void AddDVD()
    {
        Console.Write("Enter DVD title: ");
        string title = Console.ReadLine();

        Console.Write("Enter DVD director: ");
        string director = Console.ReadLine();

        DVD dvd = new DVD
        {
            Title = title,
            Director = director,
            IsAvailable = true
        };

        library.AddItem(dvd);
        Console.WriteLine("DVD added successfully.");
    }

    private void RemoveItem()
    {
        Console.Write("Enter item title: ");
        string title = Console.ReadLine();

        LibraryItem itemToRemove = library.FindItemByTitle(title);

        if (itemToRemove != null)
        {
            library.RemoveItem(itemToRemove);
            Console.WriteLine("Item removed successfully.");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    private void PrintAvailableItems()
    {
        library.PrintAvailableItems();
    }
}

class Program
{
    static void Main(string[] args)
    {
        UserInterface ui = new UserInterface();
        ui.Run();
    }
}
