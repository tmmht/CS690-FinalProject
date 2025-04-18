// SkillHabit v2.0.0 - Console App in C# using .NET 8
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Program
{
    static string dataPath = "data/goals.json";
    static List<Goal> goals = new();

    public static void Main()
    {
        Directory.CreateDirectory("data");
        LoadGoals();
        ShowReminders();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== SkillHabit Tracker ====");
            Console.WriteLine("1. Add New Learning Goal");
            Console.WriteLine("2. View Goals & Progress");
            Console.WriteLine("3. Mark Goal as Completed");
            Console.WriteLine("4. Edit Goal");
            Console.WriteLine("5. Delete Goal");
            Console.WriteLine("6. View Summary Statistics");
            Console.WriteLine("7. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddGoal(); break;
                case "2": ViewGoals(); break;
                case "3": MarkGoalCompleted(); break;
                case "4": EditGoal(); break;
                case "5": DeleteGoal(); break;
                case "6": ViewSummary(); break;
                case "7": SaveGoals(); return;
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
            string badge = goal.CurrentStreak >= 30 ? "🏅 30-Day Streak"
                        : goal.CurrentStreak >= 7  ? "🏅 7-Day Streak"
                        : "None";

            int percent = goal.DurationInDays > 0 
                ? (int)(((double)goal.TotalCompletions / goal.DurationInDays) * 100)
                : 0;
            if (percent > 100) percent = 100;

            string bar = new string('█', percent / 10).PadRight(10);

            Console.WriteLine($"[{goal.Name}]\n- Total Completions: {goal.TotalCompletions}\n- Current Streak: {goal.CurrentStreak} days\n- Longest Streak: {goal.LongestStreak} days\n- Badges: {badge}\n- Progress: [{bar}] {percent}%\n");
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

    static void EditGoal()
    {
        Console.Clear();
        Console.WriteLine("--- Edit Goal ---");
        for (int i = 0; i < goals.Count; i++)
            Console.WriteLine($"{i + 1}. {goals[i].Name}");

        Console.Write("Select goal number to edit: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= goals.Count)
        {
            var goal = goals[index - 1];
            Console.Write($"Edit name ({goal.Name}): ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) goal.Name = name;

            Console.Write($"Edit frequency ({goal.Frequency}): ");
            string freq = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(freq)) goal.Frequency = freq;

            Console.Write($"Edit duration ({goal.DurationInDays}): ");
            if (int.TryParse(Console.ReadLine(), out int dur)) goal.DurationInDays = dur;

            SaveGoals();
            Console.WriteLine("Goal updated! Press Enter.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
        Console.ReadLine();
    }

    static void DeleteGoal()
    {
        Console.Clear();
        Console.WriteLine("--- Delete Goal ---");
        for (int i = 0; i < goals.Count; i++)
            Console.WriteLine($"{i + 1}. {goals[i].Name}");

        Console.Write("Select goal number to delete: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= goals.Count)
        {
            var goal = goals[index - 1];
            Console.Write($"Are you sure you want to delete \"{goal.Name}\"? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                goals.RemoveAt(index - 1);
                SaveGoals();
                Console.WriteLine("Goal deleted.");
            }
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
        Console.WriteLine("Press Enter to return.");
        Console.ReadLine();
    }

    static void ShowReminders()
    {
        DateTime today = DateTime.Today;
        var incompleteGoals = goals.Where(g => !g.CompletionDates.Contains(today)).ToList();

        if (incompleteGoals.Count > 0)
        {
            Console.WriteLine("🔔 Reminder: You have goals that haven’t been completed today!");
            foreach (var g in incompleteGoals)
            {
                Console.WriteLine($"- {g.Name} ({g.CurrentStreak}-day streak)");
            }
            Console.WriteLine("Keep going! You're doing great!\n");
        }
    }
}