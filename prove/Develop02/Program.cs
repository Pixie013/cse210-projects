using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }
}

class Journal
{
    private List<JournalEntry> entries;

    public Journal()
    {
        entries = new List<JournalEntry>();
    }

    public void AddEntry(string prompt, string response, DateTime date)
    {
        entries.Add(new JournalEntry { Prompt = prompt, Response = response, Date = date });
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                if (DateTime.TryParse(parts[0], out DateTime date))
                {
                    entries.Add(new JournalEntry { Date = date, Prompt = parts[1], Response = parts[2] });
                }
            }
        }
    }

    public void DisplayEntries()
    {
        Console.WriteLine("Journal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Save journal to a file");
            Console.WriteLine("3. Load journal from a file");
            Console.WriteLine("4. Display journal entries");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    string[] prompts = {
                        "Who was the most interesting person you interacted with today?",
                        "What was the best part of your day?",
                        "How did you see the hand of the Lord in your life today?",
                        "What was the strongest emotion you felt today?",
                        "If you had one thing you could do over today, what would it be?"
                    };
                    DateTime currentDate = DateTime.Now;

                    foreach (var prompt in prompts)
                    {
                        Console.WriteLine($"Prompt: {prompt}");
                        Console.Write("Enter your response: ");
                        string response = Console.ReadLine();
                        journal.AddEntry(prompt, response, currentDate);
                        Console.WriteLine("Entry added successfully!\n");
                    }
                    break;
                case "2":
                    Console.Write("Enter the filename to save the journal: ");
                    string filename = Console.ReadLine();
                    journal.SaveToFile(filename);
                    Console.WriteLine("Journal saved successfully!\n");
                    break;
                case "3":
                    Console.Write("Enter the filename to load the journal from: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadFromFile(loadFilename);
                    Console.WriteLine("Journal loaded successfully!\n");
                    break;
                case "4":
                    journal.DisplayEntries();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }
    }
}
