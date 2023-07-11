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

class Patron
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactInfo { get; set; }
    public List<LibraryItem> BorrowedItems { get; set; }

    public Patron()
    {
        BorrowedItems = new List<LibraryItem>();
    }

    public void BorrowItem(LibraryItem item)
    {
        BorrowedItems.Add(item);
        item.IsAvailable = false;
    }

    public void ReturnItem(LibraryItem item)
    {
        BorrowedItems.Remove(item);
        item.IsAvailable = true;
    }
}

class StaffMember
{
    public string Name { get; set; }
    public string Role { get; set; }

    public void AddItemToLibrary(LibraryItem item, Library library)
    {
        library.AddItem(item);
    }

    public void RemoveItemFromLibrary(LibraryItem item, Library library)
    {
        library.RemoveItem(item);
    }

    public void GenerateReport(Library library)
    {
        // Generate library report
        Console.WriteLine("Generating library report...");
        library.PrintAvailableItems();
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
    private List<Patron> patrons;
    private StaffMember staffMember;

    public UserInterface()
    {
        library = new Library();
        patrons = new List<Patron>();
        staffMember = new StaffMember();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Add DVD");
            Console.WriteLine("3. Remove Item");
            Console.WriteLine("4. Print Available Items");
            Console.WriteLine("5. Borrow Item");
            Console.WriteLine("6. Return Item");
            Console.WriteLine("7. Exit");
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
                    BorrowItem();
                    break;
                case "6":
                    ReturnItem();
                    break;
                case "7":
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

        staffMember.AddItemToLibrary(book, library);
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

        staffMember.AddItemToLibrary(dvd, library);
        Console.WriteLine("DVD added successfully.");
    }

    private void RemoveItem()
    {
        Console.Write("Enter item title: ");
        string title = Console.ReadLine();

        LibraryItem itemToRemove = library.FindItemByTitle(title);

        if (itemToRemove != null)
        {
            staffMember.RemoveItemFromLibrary(itemToRemove, library);
            Console.WriteLine("Item removed successfully.");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    private void PrintAvailableItems()
    {
        staffMember.GenerateReport(library);
    }

    private void BorrowItem()
    {
        Console.Write("Enter patron name: ");
        string patronName = Console.ReadLine();

        Console.Write("Enter item title: ");
        string itemTitle = Console.ReadLine();

        Patron patron = patrons.Find(p => p.Name.Equals(patronName, StringComparison.OrdinalIgnoreCase));
        LibraryItem item = library.FindItemByTitle(itemTitle);

        if (patron != null && item != null && item.IsAvailable)
        {
            patron.BorrowItem(item);
            Console.WriteLine("Item borrowed successfully.");
        }
        else
        {
            Console.WriteLine("Invalid patron or item not available.");
        }
    }

    private void ReturnItem()
    {
        Console.Write("Enter patron name: ");
        string patronName = Console.ReadLine();

        Console.Write("Enter item title: ");
        string itemTitle = Console.ReadLine();

        Patron patron = patrons.Find(p => p.Name.Equals(patronName, StringComparison.OrdinalIgnoreCase));
        LibraryItem item = library.FindItemByTitle(itemTitle);

        if (patron != null && item != null && !item.IsAvailable)
        {
            patron.ReturnItem(item);
            Console.WriteLine("Item returned successfully.");
        }
        else
        {
            Console.WriteLine("Invalid patron or item not borrowed.");
        }
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
