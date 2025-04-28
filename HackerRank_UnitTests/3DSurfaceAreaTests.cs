using HackerRank_Exercises;
using NUnit.Framework;

namespace HackerRank_UnitTests;

public class _3DSurfaceAreaTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(SurfaceTestCases))]
    public void _3DSurfaceCalculatedCorrectly(List<List<int>> stacks, int expectedResult)
    {
        int result = _3DSurfaceAreaResult.SurfaceArea(stacks);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    private static object[] SurfaceTestCases =>
    [
        new object[]
        {
            new List<List<int>>
            {
                new List<int> { 1 },
            },
            6
        },
        new object[]
        {
            new List<List<int>>
            {
                new List<int> { 1, 3, 4 },
                new List<int> { 2, 2, 3 },
                new List<int> { 1, 2, 4 }
            },
            60
        }
    ];
}
