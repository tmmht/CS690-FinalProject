# CS690-FinalProject(SkillHabit – Skill Learning Habit Tracker)

**SkillHabit** is a simple, console-based habit tracker written in C# using .NET 8.  
It helps users build consistent learning routines by tracking small, manageable daily or weekly skill-based goals.

---

## Features (v1.0.0)

- Add new learning goals
- Set frequency (Daily/Weekly) and duration
- Track completion and update streaks
- View progress, total completions, and longest streak
- Local data storage (no internet required)
- Console UI with motivational messages

---

## Installation

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

2. Clone the repository:

```bash
git clone https://github.com/tmmht/CS690-FinalProject.git
cd SkillHabit
```

## Run APP

```bash
dotnet run
```


## Screen Description

### SkillHabit Tracker

==== SkillHabit Tracker ====
1. Add New Learning Goal
2. View Goals & Progress
3. Mark Goal as Completed
4. View Summary Statistics
5. Exit
Select an option: _


### Add New Learning Goal

--- Add New Goal ---

- Enter Goal Name (e.g., Vocabulary Practice, Coding Practice): ____________________
- Enter Category (e.g., Language, Coding): ____________________
- Frequency (Daily / Weekly): ________________
- Duration (in days): _______



### View Goals & Progress

You can see Your Goals& Progress

--- Your Goals ---

[Vocabulary Practice]
- Total Completions: 1
- Current Streak: 1 days
- Longest Streak: 1 days
- Badges: None

[Coding Practice]
- Total Completions: 1
- Current Streak: 1 days
- Longest Streak: 1 days
- Badges: None

Press Enter to return to menu.


### Mark Goal as Completed

--- Mark Goal as Completed ---

1. Vocabulary Practice (🔥 1-day streak)
2. Coding Practice (🔥 1-day streak)
Enter the goal number: _

You already marked this goal completed today.
Press Enter to return to menu.

### View Summary Statistics

--- Your Goals ---

[Vocabulary Practice]
- Total Completions: 12
- Current Streak: 6 days
- Longest Streak: 6 days
- Badges: 🏅 7-Day Streak

[Coding Practice]
- Total Completions: 5
- Current Streak: 2 weeks
- Badges: None

Press Enter to return to menu.

### Exit

Exit app


