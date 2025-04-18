// SkillHabit v1.0.0 - Console App in C# using .NET 8
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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

public class Program
{
    static string dataPath = "data/goals.json";
    static List<Goal> goals = new();

    public static void Main()
    {
        Directory.CreateDirectory("data");
        LoadGoals();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== SkillHabit Tracker ====");
            Console.WriteLine("1. Add New Learning Goal");
            Console.WriteLine("2. View Goals & Progress");
            Console.WriteLine("3. Mark Goal as Completed");
            Console.WriteLine("4. View Summary Statistics");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddGoal(); break;
                case "2": ViewGoals(); break;
                case "3": MarkGoalCompleted(); break;
                case "4": ViewSummary(); break;
                case "5": SaveGoals(); return;
                default: Console.WriteLine("Invalid input. Press Enter."); Console.ReadLine(); break;
            }
        }
    }

    static void AddGoal()
    {
        Console.Clear();
        Console.WriteLine("--- Add New Goal ---");
        Goal goal = new();
        Console.Write("Enter Goal Name: "); goal.Name = Console.ReadLine();
        Console.Write("Enter Category: "); goal.Category = Console.ReadLine();
        Console.Write("Frequency (Daily/Weekly): "); goal.Frequency = Console.ReadLine();
        Console.Write("Duration (in days): "); goal.DurationInDays = int.Parse(Console.ReadLine());
        goals.Add(goal);
        SaveGoals();
        Console.WriteLine("Goal saved! Press Enter to return to menu.");
        Console.ReadLine();
    }

    static void ViewGoals()
    {
        Console.Clear();
        Console.WriteLine("--- Your Goals ---");
        foreach (var goal in goals)
        {
            Console.WriteLine($"[{goal.Name}]\n- Total Completions: {goal.TotalCompletions}\n- Current Streak: {goal.CurrentStreak} days\n- Longest Streak: {goal.LongestStreak} days\n- Badges: {(goal.CurrentStreak >= 7 ? "🏅 7-Day Streak" : "None")}\n");
        }
        Console.WriteLine("Press Enter to return to menu.");
        Console.ReadLine();
    }

    static void MarkGoalCompleted()
    {
        Console.Clear();
        Console.WriteLine("--- Mark Goal as Completed ---");
        for (int i = 0; i < goals.Count; i++)
        {
            var g = goals[i];
            Console.WriteLine($"{i + 1}. {g.Name} (🔥 {g.CurrentStreak}-day streak)");
        }
        Console.Write("Enter the goal number: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= goals.Count)
        {
            var goal = goals[index - 1];
            DateTime today = DateTime.Today;
            if (!goal.CompletionDates.Contains(today))
            {
                goal.CompletionDates.Add(today);
                goal.TotalCompletions++;

                if (goal.CompletionDates.Contains(today.AddDays(-1)))
                    goal.CurrentStreak++;
                else
                    goal.CurrentStreak = 1;

                if (goal.CurrentStreak > goal.LongestStreak)
                    goal.LongestStreak = goal.CurrentStreak;

                SaveGoals();
                Console.WriteLine("✔️ Progress logged successfully! Keep it up!!!");
            }
            else
            {
                Console.WriteLine("You already marked this goal completed today.");
            }
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
        Console.WriteLine("Press Enter to return to menu.");
        Console.ReadLine();
    }

    static void ViewSummary()
    {
        ViewGoals();
    }

    static void LoadGoals()
    {
        if (File.Exists(dataPath))
        {
            var json = File.ReadAllText(dataPath);
            goals = JsonSerializer.Deserialize<List<Goal>>(json);
        }
    }

    static void SaveGoals()
    {
        var json = JsonSerializer.Serialize(goals, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataPath, json);
    }
}