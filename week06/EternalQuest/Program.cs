using System;
using System.Collections.Generic;
using System.IO;

abstract class Goal
{
    protected string _name;

    public Goal(string name)
    {
        _name = name;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public virtual string SaveString() => $"{GetType().Name}:{_name}";
}

class SimpleGoal : Goal
{
    private bool _isCompleted;

    public SimpleGoal(string name) : base(name)
    {
        _isCompleted = false;
    }

    public override int RecordEvent()
    {
        if (!_isCompleted)
        {
            _isCompleted = true;
            return 1; // +1 point for completion
        }
        return 0;
    }

    public override string GetStatus() => _isCompleted ? "[X] Completed" : "[ ] Not Completed";
}

class EternalGoal : Goal
{
    public EternalGoal(string name) : base(name) {}

    public override int RecordEvent() => 1; // +1 point every time recorded
    public override string GetStatus() => "[∞] Keep Going!";
}

class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;

    public ChecklistGoal(string name, int targetCount) : base(name)
    {
        _targetCount = targetCount;
        _currentCount = 0;
    }

    public override int RecordEvent()
    {
        _currentCount++;
        return 1; // +1 point for each progress step
    }

    public override string GetStatus() => $"Completed {_currentCount}/{_targetCount} times";
    public override string SaveString() => $"{base.SaveString()},{_targetCount},{_currentCount}";
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void AddGoal(Goal goal) => _goals.Add(goal);

    public void RecordGoal(string name)
    {
        foreach (var goal in _goals)
        {
            if (goal.GetStatus().Contains(name))
            {
                _score += goal.RecordEvent();
                return;
            }
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("\nYour Goals:");
        foreach (var goal in _goals)
        {
            Console.WriteLine($"{goal.GetStatus()} ({goal.SaveString()})");
        }
        Console.WriteLine($"Total Score: {_score}");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.SaveString());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (!File.Exists(filename)) return;

        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(':');
            string type = parts[0];
            string[] data = parts[1].Split(',');

            if (type == "SimpleGoal")
                _goals.Add(new SimpleGoal(data[0]));
            else if (type == "EternalGoal")
                _goals.Add(new EternalGoal(data[0]));
            else if (type == "ChecklistGoal")
                _goals.Add(new ChecklistGoal(data[0], int.Parse(data[1])));
        }
    }
}

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();

        Console.WriteLine("Welcome to Eternal Quest! Track goals with a simple point system.");
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1 - Add a Simple Goal");
            Console.WriteLine("2 - Add an Eternal Goal");
            Console.WriteLine("3 - Add a Checklist Goal");
            Console.WriteLine("4 - Record a Goal");
            Console.WriteLine("5 - Display Goals");
            Console.WriteLine("6 - Save Goals");
            Console.WriteLine("7 - Load Goals");
            Console.WriteLine("8 - Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal name: ");
                    string sName = Console.ReadLine();
                    manager.AddGoal(new SimpleGoal(sName));
                    break;

                case "2":
                    Console.Write("Enter goal name: ");
                    string eName = Console.ReadLine();
                    manager.AddGoal(new EternalGoal(eName));
                    break;

                case "3":
                    Console.Write("Enter goal name: ");
                    string cName = Console.ReadLine();
                    Console.Write("Enter total required completions: ");
                    int cTarget = int.Parse(Console.ReadLine());
                    manager.AddGoal(new ChecklistGoal(cName, cTarget));
                    break;

                case "4":
                    Console.Write("Enter goal name to record event: ");
                    string recordName = Console.ReadLine();
                    manager.RecordGoal(recordName);
                    break;

                case "5":
                    manager.DisplayGoals();
                    break;

                case "6":
                    Console.Write("Enter filename to save: ");
                    string saveFile = Console.ReadLine();
                    manager.SaveGoals(saveFile);
                    break;

                case "7":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    manager.LoadGoals(loadFile);
                    break;

                case "8":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }

        Console.WriteLine("Thanks for using Eternal Quest! Keep growing!");
    }
}
