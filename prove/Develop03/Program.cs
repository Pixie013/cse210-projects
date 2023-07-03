using System;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Create a list of scriptures
        List<Scripture> scriptures = new List<Scripture>()
        {
            new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture("Proverbs 3:5-6", "Trust in the LORD with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
        };

        // Select a random scripture
        Random random = new Random();
        int index = random.Next(scriptures.Count);
        Scripture selectedScripture = scriptures[index];

        // Hide words in the scripture until all are hidden
        while (!selectedScripture.AllWordsHidden())
        {
            Console.Clear();
            selectedScripture.Display();

            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            selectedScripture.HideRandomWord();
        }

        // This will start by displaying "AAA" and waiting for the user to press the enter key
        Console.WriteLine("AAA");
        Console.ReadLine();

        // This will clear the console
        Console.Clear();

        // This will show "BBB" in the console where "AAA" used to be
        Console.WriteLine("BBB");
    }
}

class Scripture
{
    private string reference;
    private List<Word> words;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void Display()
    {
        Console.WriteLine(reference);
        foreach (Word word in words)
        {
            if (word.IsHidden)
                Console.Write("***** ");
            else
                Console.Write(word.Value + " ");
        }
        Console.WriteLine();
    }

    public void HideRandomWord()
    {
        Random random = new Random();
        List<Word> visibleWords = words.Where(word => !word.IsHidden).ToList();
        int index = random.Next(visibleWords.Count);
        visibleWords[index].Hide();
    }

    public bool AllWordsHidden()
    {
        return words.All(word => word.IsHidden);
    }
}

class Word
{
    public string Value { get; }
    public bool IsHidden { get; private set; }

    public Word(string value)
    {
        Value = value;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}
