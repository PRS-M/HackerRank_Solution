using System.Numerics;
using HackerRank_Exercises;
using NUnit.Framework;

namespace HackerRank_UnitTests;

public class ExtraLongFactorialsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCaseSource(nameof(FactorialTestCases))]
    public void FactorialCalculatedProperly(int number, string expectedResult)
    {
        string result = ExtraLongFactorialsResult.ExtraLongFactorials(number);

        Assert.That(result, Is.EqualTo(expectedResult));
    }

    public static object[] FactorialTestCases =>
    [
        new object[] { 10, "3628800" },
        new object[] { 12, "479001600" },
        new object[] { 13, "6227020800" },
        new object[] { 20, "2432902008176640000" },
        new object[] { 21, "51090942171709440000" },
        new object[] { 22, "1124000727777607680000" },
        new object[] { 23, "25852016738884976640000" },
        new object[] { 25, "15511210043330985984000000" },
    ];
}
