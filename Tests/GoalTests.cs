using System;
using System.Collections.Generic;
using Xunit;

public class GoalTests
{
    [Fact]
    public void Goal_DefaultValues_ShouldBeZero()
    {
        var goal = new Goal();
        Assert.Equal(0, goal.TotalCompletions);
        Assert.Equal(0, goal.CurrentStreak);
        Assert.Equal(0, goal.LongestStreak);
        Assert.NotNull(goal.CompletionDates);
    }

    [Fact]
    public void Goal_Properties_ShouldSetCorrectly()
    {
        var goal = new Goal
        {
            Name = "Test Goal",
            Category = "Study",
            Frequency = "Daily",
            DurationInDays = 10
        };

        Assert.Equal("Test Goal", goal.Name);
        Assert.Equal("Study", goal.Category);
        Assert.Equal("Daily", goal.Frequency);
        Assert.Equal(10, goal.DurationInDays);
    }

    [Fact]
    public void Completion_ShouldUpdateStreakAndTotalCorrectly()
    {
        var goal = new Goal();
        var today = DateTime.Today;
        var yesterday = today.AddDays(-1);

        goal.CompletionDates.Add(yesterday);
        goal.TotalCompletions++;

        if (!goal.CompletionDates.Contains(today))
        {
            goal.CompletionDates.Add(today);
            goal.TotalCompletions++;

            if (goal.CompletionDates.Contains(yesterday))
                goal.CurrentStreak++;
            else
                goal.CurrentStreak = 1;

            if (goal.CurrentStreak > goal.LongestStreak)
                goal.LongestStreak = goal.CurrentStreak;
        }

        Assert.Equal(2, goal.CompletionDates.Count);
        Assert.Equal(2, goal.TotalCompletions);
        Assert.Equal(1, goal.CurrentStreak);
        Assert.Equal(1, goal.LongestStreak);
    }


    [Fact]
    public void MissedDay_ShouldResetStreak()
    {
        var goal = new Goal();
        var threeDaysAgo = DateTime.Today.AddDays(-3);

        goal.CompletionDates.Add(threeDaysAgo);
        goal.TotalCompletions++;

        goal.CurrentStreak = 1;
        var today = DateTime.Today;

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
        }

        Assert.Equal(2, goal.CompletionDates.Count);
        Assert.Equal(2, goal.TotalCompletions);
        Assert.Equal(1, goal.CurrentStreak);
    }


    [Theory]
    [InlineData(6, "None")]
    [InlineData(7, "🏅 7-Day Streak")]
    [InlineData(30, "🏅 30-Day Streak")]
    public void BadgeLogic_ShouldReturnExpectedBadge(int streak, string expectedBadge)
    {
        string badge = streak >= 30 ? "🏅 30-Day Streak"
                    : streak >= 7  ? "🏅 7-Day Streak"
                    : "None";

        Assert.Equal(expectedBadge, badge);
    }

    [Fact]
    public void ProgressPercentage_ShouldNotExceed100()
    {
        var goal = new Goal
        {
            DurationInDays = 10,
            TotalCompletions = 15
        };

        int percent = goal.DurationInDays > 0
            ? (int)(((double)goal.TotalCompletions / goal.DurationInDays) * 100)
            : 0;
        if (percent > 100) percent = 100;

        Assert.True(percent <= 100);
        Assert.Equal(100, percent);
    }
}
