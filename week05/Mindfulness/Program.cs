using System;
using System.Collections.Generic;
using System.Threading;

abstract class MindfulnessActivity
{
    protected int duration;
    protected string name;
    protected string description;

    public MindfulnessActivity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Starting {name}...");
        Console.WriteLine(description);
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
        RunActivity();
        Console.WriteLine($"Well done! You completed {name} for {duration} seconds.");
        ShowSpinner(3);
    }

    protected abstract void RunActivity();

    protected void ShowSpinner(int seconds)
    {
        string[] spinner = { "/", "-", "\\", "|" };
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write("\b" + spinner[i % 4]);
            Thread.Sleep(250);
        }
        Console.Write("\b \b");
    }

    protected void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"\r{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() 
        : base("Breathing Activity", "This activity helps you relax by guiding you through slow breathing exercises.") {}

    protected override void RunActivity()
    {
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Breathe in...");
            Countdown(5);
            Console.WriteLine("Breathe out...");
            Countdown(5);
        }
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private static List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static List<string> questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What did you learn about yourself through this experience?"
    };

    public ReflectionActivity() 
        : base("Reflection Activity", "This activity helps you reflect on times in your life when you have shown strength and resilience.") {}

    protected override void RunActivity()
    {
        Random rnd = new Random();
        Console.WriteLine(prompts[rnd.Next(prompts.Count)]);
        ShowSpinner(3);
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine(questions[rnd.Next(questions.Count)]);
            ShowSpinner(3);
        }
    }
}

class ListingActivity : MindfulnessActivity
{
    private static List<string> prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() 
        : base("Listing Activity", "This activity helps you reflect on the good things in your life by listing as many as possible.") {}

    protected override void RunActivity()
    {
        Random rnd = new Random();
        Console.WriteLine(prompts[rnd.Next(prompts.Count)]);
        Console.WriteLine("Start listing items:");
        ShowSpinner(10);
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int count = 0;
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            Console.ReadLine();
            count++;
        }
        Console.WriteLine($"You listed {count} items!");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            MindfulnessActivity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => null
            };

            if (activity == null)
            {
                Console.WriteLine("Exiting program...");
                break;
            }

            activity.Start();
        }
    }
}
