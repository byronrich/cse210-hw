using System;
using System.Collections.Generic;

class Comment
{
    private string _commenterName;
    private string _text;

    public Comment(string commenterName, string text)
    {
        _commenterName = commenterName;
        _text = text;
    }

    public string GetCommentDetails()
    {
        return $"{_commenterName}: {_text}";
    }
}

class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return _comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Author: {_author}");
        Console.WriteLine($"Length: {_lengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in _comments)
        {
            Console.WriteLine($"- {comment.GetCommentDetails()}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Creating Video objects
        Video video1 = new Video("Intro to C#", "John Doe", 300);
        Video video2 = new Video("Object-Oriented Programming", "Jane Smith", 450);
        Video video3 = new Video("Understanding Data Structures", "Emily Johnson", 600);
        Video video4 = new Video("Algorithms Explained", "Michael Brown", 750);

        // Adding comments to each video
        video1.AddComment(new Comment("Alice", "Great introduction!"));
        video1.AddComment(new Comment("Bob", "Very helpful."));
        video1.AddComment(new Comment("Charlie", "Clear and concise."));

        video2.AddComment(new Comment("Dave", "Love the examples."));
        video2.AddComment(new Comment("Eve", "Thanks, this helps greatly."));
        video2.AddComment(new Comment("Frank", "Nicely explained!"));

        video3.AddComment(new Comment("Grace", "Well structured."));
        video3.AddComment(new Comment("Hank", "Fantastic!"));
        video3.AddComment(new Comment("Ivy", "Learned a lot!"));

        video4.AddComment(new Comment("Jack", "Algorithms made easy."));
        video4.AddComment(new Comment("Kim", "Clear breakdown."));
        video4.AddComment(new Comment("Leo", "Helpful visuals."));

        // Storing videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3, video4 };

        // Displaying video information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
