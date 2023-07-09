using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GoalTracker
{
    class Program
    {
        static List<Goal> goals = new List<Goal>();

        static void Main(string[] args)
        {
            LoadGoals();

            bool quit = false;
            while (!quit)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Create new goal");
                Console.WriteLine("2. List goals");
                Console.WriteLine("3. Save goals");
                Console.WriteLine("4. Load goals");
                Console.WriteLine("5. Record event");
                Console.WriteLine("6. Quit");

                Console.Write("Enter your choice (1-6): ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        ListGoals();
                        break;
                    case "3":
                        SaveGoals();
                        break;
                    case "4":
                        LoadGoals();
                        break;
                    case "5":
                        RecordEvent();
                        break;
                    case "6":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void CreateGoal()
        {
            Console.WriteLine("Goal Types:");
            Console.WriteLine("1. Simple");
            Console.WriteLine("2. Eternal");
            Console.WriteLine("3. Checklist");

            Console.Write("Enter the goal type (1-3): ");
            string goalTypeChoice = Console.ReadLine();
            Console.WriteLine();

            GoalType goalType;
            switch (goalTypeChoice)
            {
                case "1":
                    goalType = GoalType.Simple;
                    break;
                case "2":
                    goalType = GoalType.Eternal;
                    break;
                case "3":
                    goalType = GoalType.Checklist;
                    break;
                default:
                    Console.WriteLine("Invalid goal type. Please try again.");
                    return;
            }

            Console.Write("Enter the name of the goal: ");
            string name = Console.ReadLine();

            Console.Write("Enter a short description of the goal: ");
            string description = Console.ReadLine();

            Console.Write("Enter the number of points for the goal: ");
            string pointsString = Console.ReadLine();
            int points;
            if (!int.TryParse(pointsString, out points))
            {
                Console.WriteLine("Invalid points value. Please try again.");
                return;
            }

            switch (goalType)
            {
                case GoalType.Simple:
                    SimpleGoal simpleGoal = new SimpleGoal(name, description, points);
                    goals.Add(simpleGoal);
                    Console.WriteLine("Simple goal created successfully.");
                    break;
                case GoalType.Eternal:
                    EternalGoal eternalGoal = new EternalGoal(name, description, points);
                    goals.Add(eternalGoal);
                    Console.WriteLine("Eternal goal created successfully.");
                    break;
                case GoalType.Checklist:
                    Console.Write("Enter the number of times the goal needs to be accomplished: ");
                    string timesString = Console.ReadLine();
                    int times;
                    if (!int.TryParse(timesString, out times))
                    {
                        Console.WriteLine("Invalid times value. Please try again.");
                        return;
                    }

                    ChecklistGoal checklistGoal = new ChecklistGoal(name, description, points, times);
                    goals.Add(checklistGoal);
                    Console.WriteLine("Checklist goal created successfully.");
                    break;
            }
        }

        static void ListGoals()
        {
            if (goals.Count == 0)
            {
                Console.WriteLine("No goals found.");
                return;
            }

            Console.WriteLine("Goals:");
            foreach (Goal goal in goals)
            {
                Console.WriteLine(goal);
            }
        }

        static void RecordEvent()
        {
            if (goals.Count == 0)
            {
                Console.WriteLine("No goals found.");
                return;
            }

            Console.WriteLine("Goals:");
            ListGoals();

            Console.WriteLine("Enter 'q' to quit.");
            Console.Write("Enter the index of the goal to record an event for: ");
            string indexString = Console.ReadLine();

            if (indexString.ToLower() == "q")
                return;

            if (!int.TryParse(indexString, out int index) || index < 0 || index >= goals.Count)
            {
                Console.WriteLine("Invalid goal index. Please try again.");
                return;
            }

            Goal goal = goals[index];
            goal.CompleteEvent();

            Console.WriteLine("Congratulations! You have completed the goal.");
            Console.WriteLine($"You earned {goal.Points} points.");
        }

        static void SaveGoals()
        {
            if (goals.Count == 0)
            {
                Console.WriteLine("No goals to save.");
                return;
            }

            Console.Write("Enter the file name to save the goals: ");
            string fileName = Console.ReadLine();

            try
            {
                string json = JsonSerializer.Serialize(goals);
                File.WriteAllText(fileName, json);
                Console.WriteLine("Goals saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving goals: {ex.Message}");
            }
        }

        static void LoadGoals()
        {
            Console.Write("Enter the file name to load the goals: ");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            try
            {
                string json = File.ReadAllText(fileName);
                goals = JsonSerializer.Deserialize<List<Goal>>(json);
                Console.WriteLine("Goals loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading goals: {ex.Message}");
            }
        }
    }

    enum GoalType
    {
        Simple,
        Eternal,
        Checklist
    }

    class Goal
    {
        public string Name { get; }
        public string Description { get; }
        public int Points { get; }
        public bool Completed { get; protected set; }

        public Goal(string name, string description, int points)
        {
            Name = name;
            Description = description;
            Points = points;
            Completed = false;
        }

        public virtual void CompleteEvent()
        {
            Completed = true;
        }

        public override string ToString()
        {
            string status = Completed ? "[X]" : "[ ]";
            return $"{status} {Name}: {Description} ({Points} points)";
        }
    }

    class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override void CompleteEvent()
        {
            // Eternal goals are never completed
        }
    }

    class ChecklistGoal : Goal
    {
        public int Times { get; }
        public int TimesCompleted { get; private set; }

        public ChecklistGoal(string name, string description, int points, int times)
            : base(name, description, points)
        {
            Times = times;
            TimesCompleted = 0;
        }

        public override void CompleteEvent()
        {
            if (TimesCompleted < Times)
            {
                TimesCompleted++;
                if (TimesCompleted == Times)
                {
                    Completed = true;
                }
            }
        }

        public override string ToString()
        {
            string status = Completed ? "[X]" : $"[{TimesCompleted}/{Times}]";
            return $"{status} {Name}: {Description} ({Points} points)";
        }
    }
}
