using System;
using System.Threading;

public abstract class Activity
{
    protected static void DisplayStartingMessage(string activityName, string description)
    {
        Console.WriteLine($"--- {activityName} ---");
        Console.WriteLine(description);
        Console.WriteLine("Prepare to begin...");
        Console.WriteLine();
        PauseWithSpinner(3);
    }

    protected static void DisplayEndingMessage(string activityName)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {activityName} Completed ---");
        Console.WriteLine("You did a great job!");
        Console.WriteLine();
        PauseWithSpinner(3);
    }

    protected static void PauseWithSpinner(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
        }
        Console.WriteLine();
    }

    public abstract void PerformActivity();
}

public class BreathingActivity : Activity
{
    public override void PerformActivity()
    {
        DisplayStartingMessage("Breathing Activity", "This activity will help you relax by guiding you through breathing in and out slowly. Clear your mind and focus on your breathing.");

        Console.WriteLine("Breathing in...");
        Countdown(4);

        Console.WriteLine("Holding breath...");
        Countdown(4);

        Console.WriteLine("Breathing out...");
        Countdown(4);

        Console.WriteLine("Repeat...");
        Console.WriteLine();

        Console.WriteLine("Breathing in...");
        Countdown(4);

        Console.WriteLine("Holding breath...");
        Countdown(4);

        Console.WriteLine("Breathing out...");
        Countdown(4);

        Console.WriteLine("Repeat...");
        Console.WriteLine();

        Console.WriteLine("Breathing in...");
        Countdown(4);

        Console.WriteLine("Holding breath...");
        Countdown(4);

        Console.WriteLine("Breathing out...");
        Countdown(4);

        Console.WriteLine("Repeat...");
        Console.WriteLine();

        DisplayEndingMessage("Breathing Activity");
    }

    private static void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"Time remaining: {i} seconds");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

public class ReflectionActivity : Activity
{
    private static readonly string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public override void PerformActivity()
    {
        DisplayStartingMessage("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine($"Prompt: {prompt}");
        PauseWithSpinner(4);

        Console.WriteLine("Reflection questions:");
        foreach (string question in questions)
        {
            Console.WriteLine($"Question: {question}");
            Countdown(10);
            Console.WriteLine();
        }

        DisplayEndingMessage("Reflection Activity");
    }

    private static void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.WriteLine($"Time remaining: {i} seconds");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

public class ListingActivity : Activity
{
    private static readonly string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public override void PerformActivity()
    {
        DisplayStartingMessage("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");

        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine($"Prompt: {prompt}");
        PauseWithSpinner(4);

        Console.WriteLine("Start listing items:");
        string input;
        int count = 0;
        do
        {
            Console.Write("> ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                count++;
            }
        } while (!string.IsNullOrWhiteSpace(input));
        Console.WriteLine();
        Console.WriteLine($"Number of items listed: {count}");

        DisplayEndingMessage("Listing Activity");
    }
}

public class GratitudeJournalingActivity : Activity
{
    private static readonly string[] prompts = {
        "What are you grateful for today?",
        "Who or what made you smile today?",
        "What made today a good day?",
        "What are the positive aspects of your life right now?",
        "What is something you're looking forward to?"
    };

    public override void PerformActivity()
    {
        DisplayStartingMessage("Gratitude Journaling Activity", "This activity allows you to write and reflect on things you are grateful for. Input multiple items and review them later to foster a positive mindset.");

        Console.WriteLine("Start journaling:");
        Console.WriteLine("(Enter 'done' to finish)");
        Console.WriteLine();

        string input;
        int count = 0;
        do
        {
            Console.Write("> ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && input.ToLower() != "done")
            {
                count++;
            }
        } while (input.ToLower() != "done");
        Console.WriteLine();
        Console.WriteLine($"Number of items journaled: {count}");

        DisplayEndingMessage("Gratitude Journaling Activity");
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Mindfulness App ---");
        Console.WriteLine();
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.WriteLine("4. Gratitude Journaling Activity");
        Console.WriteLine("5. Exit");

        while (true)
        {
            Console.WriteLine();
            Console.Write("Enter your choice (1-5): ");
            string choice = Console.ReadLine();

            Console.Clear();

            Activity activity;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    activity = new GratitudeJournalingActivity();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.PerformActivity();

            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("--- Mindfulness App ---");
            Console.WriteLine();
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Gratitude Journaling Activity");
            Console.WriteLine("5. Exit");
        }
    }
}
