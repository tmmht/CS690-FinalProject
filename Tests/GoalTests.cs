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
}
