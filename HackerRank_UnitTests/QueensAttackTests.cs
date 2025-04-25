using HackerRank_Exercises;
using NUnit.Framework;

namespace HackerRank_UnitTests;

public class QueensAttackTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(GetData))]
    public void CheckIfReturnedLengthIsValid(QueensAttackInput queensAttackInput)
    {
        int result = QueensAttackResult.QueensAttack(
            queensAttackInput.N,
            queensAttackInput.Obstacles.Count,
            queensAttackInput.R_q,
            queensAttackInput.C_q,
            queensAttackInput.Obstacles);

        Assert.That(result, Is.EqualTo(queensAttackInput.ExpectedResult));
    }

    private static IEnumerable<QueensAttackInput> GetData()
    {
        yield return new QueensAttackInput
        {
            N = 1,
            R_q = 1,
            C_q = 1,
            ExpectedResult = 0,
            Obstacles = new List<List<int>>()
        };

        yield return new QueensAttackInput
        {
            N = 4,
            R_q = 4,
            C_q = 4,
            ExpectedResult = 9,
            Obstacles = new List<List<int>>()
        };

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 4,
            C_q = 3,
            ExpectedResult = 10,
            Obstacles = new List<List<int>>
            {
                new List<int>() { 5, 5 },
                new List<int>() { 4, 2 },
                new List<int>() { 2, 3 }
            }
        };

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 4,
            C_q = 3,
            ExpectedResult = 14,
            Obstacles = new List<List<int>>()
        };

        yield return new QueensAttackInput
        {
            N = 4,
            R_q = 4,
            C_q = 3,
            ExpectedResult = 9,
            Obstacles = new List<List<int>>
            {
                new List<int> { 5, 2 },
                new List<int> { 4, 5 },
                new List<int> { 2, 5 },
                new List<int> { 1, 2 }
            }
        };

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 5,
            C_q = 1,
            ExpectedResult = 12,
            Obstacles = new List<List<int>>()
        };

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 5,
            C_q = 1,
            ExpectedResult = 10,
            Obstacles = new List<List<int>>
            {
                new List<int> { 4, 4 },
                new List<int> { 2, 4 },
            }
        };

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 3,
            C_q = 1,
            ExpectedResult = 12,
            Obstacles = new List<List<int>>()
        };

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 3,
            C_q = 1,
            ExpectedResult = 7,
            Obstacles = new List<List<int>>
            {
                new List<int> { 2, 1 },
                new List<int> { 3, 3 },
                new List<int> { 5, 4 },
            }
        };
    }
}

public record QueensAttackInput
{
    public int N { get; set; }
    public int R_q { get; set; }
    public int C_q { get; set; }
    public int ExpectedResult { get; set; }
    public required List<List<int>> Obstacles { get; set; }
}
