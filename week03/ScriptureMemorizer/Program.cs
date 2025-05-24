using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture(new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. In all thy ways acknowledge him, and he shall direct thy paths.");

        while (!scripture.IsFullyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture);
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }

        Console.Clear();
        Console.WriteLine(scripture);
        Console.WriteLine("\nAll words are hidden. Program ended.");
    }
}

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _rand;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
        _rand = new Random();
    }

    public void HideRandomWords()
    {
        var visibleWords = _words.Where(word => !word.IsHidden).ToList();
        if (visibleWords.Count == 0) return;

        int wordsToHide = Math.Max(1, visibleWords.Count / 10); // Hide about 10% per step

        for (int i = 0; i < wordsToHide; i++)
        {
            if (visibleWords.Count == 0) break;
            int index = _rand.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool IsFullyHidden() => _words.All(word => word.IsHidden);

    public override string ToString() => $"{_reference}\n{string.Join(" ", _words)}";
}

class Reference
{
    private string _book;
    private int _chapter;
    private int _verseStart;
    private int _verseEnd;

    public Reference(string book, int chapter, int verseStart, int verseEnd = -1)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _verseEnd = verseEnd == -1 ? verseStart : verseEnd;
    }

    public override string ToString() => $"{_book} {_chapter}:{_verseStart}-{_verseEnd}";
}

class Word
{
    private string _text;
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        _text = text;
        IsHidden = false;
    }

    public void Hide() => IsHidden = true;

    public override string ToString() => IsHidden ? new string('_', _text.Length) : _text;
}
