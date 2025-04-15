using System;
using System.Collections.Generic;

public class Goal
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Category { get; set; }
    public string Frequency { get; set; } // Daily or Weekly
    public int DurationInDays { get; set; }
    public int TotalCompletions { get; set; } = 0;
    public int CurrentStreak { get; set; } = 0;
    public int LongestStreak { get; set; } = 0;
    public List<DateTime> CompletionDates { get; set; } = new();
}