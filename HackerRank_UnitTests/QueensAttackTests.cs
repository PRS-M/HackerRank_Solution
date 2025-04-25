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

    [TestCaseSource(nameof(VectorCases))]
    public void CheckIfSameDirectionCalculatedCorrectly(object a, object b, bool expectedResult)
    {
        Vector vectorA = (Vector)a;
        Vector vectorB = (Vector)b;

        bool result = vectorA.IsSameSign(vectorB);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    private static object[] VectorCases =>
    [
        new object[]
        {
            new Vector(1, 0),
            new Vector(10, 0),
            true
        },
        new object[]
        {
            new Vector(-1, 0),
            new Vector(-10, 0),
            true
        },
        new object[]
        {
            new Vector(0, 1),
            new Vector(0, 10),
            true
        },
        new object[]
        {
            new Vector(0, -1),
            new Vector(0, -10),
            true
        },
        new object[]
        {
            new Vector(1, 1),
            new Vector(10, 10),
            true
        },
        new object[]
        {
            new Vector(-1, -1),
            new Vector(-10, -10),
            true
        },
        new object[]
        {
            new Vector(1000, 1000),
            new Vector(1000, 1000),
            true
        },
        new object[]
        {
            new Vector(1, 10),
            new Vector(1, 10),
            true
        },
        new object[]
        {
            new Vector(10, 1),
            new Vector(1, 10),
            false
        },
        new object[]
        {
            new Vector(1, 10),
            new Vector(10, 1),
            false
        },
        new object[]
        {
            new Vector(1000, 1000),
            new Vector(0, 0),
            true
        },
        new object[]
        {
            new Vector(0, 0),
            new Vector(0, 0),
            true
        },
    ];

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

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 3,
            C_q = 1,
            ExpectedResult = 6,
            Obstacles = new List<List<int>>
            {
                new List<int> { 2, 1 },
                new List<int> { 2, 1 },
                new List<int> { 3, 3 },
                new List<int> { 3, 3 },
                new List<int> { 5, 4 },
                new List<int> { 5, 4 },
                new List<int> { 5, 2 },
                new List<int> { 5, 1 },
            }
        };

        yield return new QueensAttackInput
        {
            N = 5,
            R_q = 3,
            C_q = 1,
            ExpectedResult = 5,
            Obstacles = new List<List<int>>
            {
                new List<int> { 2, 1 },
                new List<int> { 2, 1 },
                new List<int> { 3, 3 },
                new List<int> { 3, 3 },
                new List<int> { 3, 3 },
                new List<int> { 3, 4 },
                new List<int> { 3, 5 },
                new List<int> { 1, 3 },
                new List<int> { 5, 4 },
                new List<int> { 5, 4 },
                new List<int> { 5, 2 },
                new List<int> { 5, 1 },
            }
        };

        yield return new QueensAttackInput
        {
            N = 100,
            R_q = 48,
            C_q = 81,
            ExpectedResult = 6,
            Obstacles = new List<List<int>>
            {
                new List<int>{ 54, 87 },
                new List<int>{ 64, 97 },
                new List<int>{ 42, 75 },
                new List<int>{ 32, 65 },
                new List<int>{ 42, 87 },
                new List<int>{ 32, 97 },
                new List<int>{ 54, 75 },
                new List<int>{ 64, 65 },
                new List<int>{ 48, 87 },
                new List<int>{ 48, 75 },
                new List<int>{ 54, 81 },
                new List<int>{ 42, 81 },
                new List<int>{ 45, 17 },
                new List<int>{ 14, 24 },
                new List<int>{ 35, 15 },
                new List<int>{ 95, 64 },
                new List<int>{ 63, 87 },
                new List<int>{ 25, 72 },
                new List<int>{ 71, 38 },
                new List<int>{ 96, 97 },
                new List<int>{ 16, 30 },
                new List<int>{ 60, 34 },
                new List<int>{ 31, 67 },
                new List<int>{ 26, 82 },
                new List<int>{ 20, 93 },
                new List<int>{ 81, 38 },
                new List<int>{ 51, 94 },
                new List<int>{ 75, 41 },
                new List<int>{ 79, 84 },
                new List<int>{ 79, 65 },
                new List<int>{ 76, 80 },
                new List<int>{ 52, 87 },
                new List<int>{ 81, 54 },
                new List<int>{ 89, 52 },
                new List<int>{ 20, 31 },
                new List<int>{ 10, 41 },
                new List<int>{ 32, 73 },
                new List<int>{ 83, 98 },
                new List<int>{ 87, 61 },
                new List<int>{ 82, 52 },
                new List<int>{ 80, 64 },
                new List<int>{ 82, 46 },
                new List<int>{ 49, 21 },
                new List<int>{ 73, 86 },
                new List<int>{ 37, 70 },
                new List<int>{ 43, 12 },
                new List<int>{ 94, 28 },
                new List<int>{ 10, 93 },
                new List<int>{ 52, 25 },
                new List<int>{ 50, 61 },
                new List<int>{ 52, 68 },
                new List<int>{ 52, 23 },
                new List<int>{ 60, 91 },
                new List<int>{ 79, 17 },
                new List<int>{ 93, 82 },
                new List<int>{ 12, 18 },
                new List<int>{ 75, 64 },
                new List<int>{ 69, 69 },
                new List<int>{ 94, 74 },
                new List<int>{ 61, 61 },
                new List<int>{ 46, 57 },
                new List<int>{ 67, 45 },
                new List<int>{ 96, 64 },
                new List<int>{ 83, 89 },
                new List<int>{ 58, 87 },
                new List<int>{ 76, 53 },
                new List<int>{ 79, 21 },
                new List<int>{ 94, 70 },
                new List<int>{ 16, 10 },
                new List<int>{ 50, 82 },
                new List<int>{ 92, 20 },
                new List<int>{ 40, 51 },
                new List<int>{ 49, 28 },
                new List<int>{ 51, 82 },
                new List<int>{ 35, 16 },
                new List<int>{ 15, 86 },
                new List<int>{ 78, 89 },
                new List<int>{ 41, 98 },
                new List<int>{ 70, 46 },
                new List<int>{ 79, 79 },
                new List<int>{ 24, 40 },
                new List<int>{ 91, 13 },
                new List<int>{ 59, 73 },
                new List<int>{ 35, 32 },
                new List<int>{ 40, 31 },
                new List<int>{ 14, 31 },
                new List<int>{ 71, 35 },
                new List<int>{ 96, 18 },
                new List<int>{ 27, 39 },
                new List<int>{ 28, 38 },
                new List<int>{ 41, 36 },
                new List<int>{ 31, 63 },
                new List<int>{ 52, 48 },
                new List<int>{ 81, 25 },
                new List<int>{ 49, 90 },
                new List<int>{ 32, 65 },
                new List<int>{ 25, 45 },
                new List<int>{ 63, 94 },
                new List<int>{ 89, 50 },
                new List<int>{ 43, 41 },
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
