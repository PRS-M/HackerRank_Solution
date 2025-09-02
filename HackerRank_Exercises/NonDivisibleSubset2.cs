namespace HackerRank_Exercises;

public class NonDivisibleSubsetSolution
{
    public static int Main(string[] args)
    {
        // TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);
        TextWriter textWriter = new StreamWriter("./outputFile.txt", true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int k = Convert.ToInt32(firstMultipleInput[1]);

        List<int> s = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToInt32(sTemp)).ToList();

        int result = NonDivisibleSubsetResult.NonDivisibleSubset(k, s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();

        return result;
    }
}

class NonDivisibleSubsetResult
{
    /*
     * Complete the 'nonDivisibleSubset' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER k
     *  2. INTEGER_ARRAY s
     */

    public static int NonDivisibleSubset(int k, List<int> s)
    {
        return GetMaximumNonDivisibleSubsetLength(k, s);
    }

    private static int GetMaximumNonDivisibleSubsetLength(int divisor, List<int> setOfNumbers)
    {
        bool divisorIsEven = divisor % 2 == 0;
        int[] counts = GetCounts(divisor, setOfNumbers);

        return GetMaximumSubsetLength(divisor, counts);
    }

    private static int[] GetCounts(int divisor, List<int> setOfNumbers)
    {
        int[] counts = new int[divisor];

        for (int i = 0; i < setOfNumbers.Count; i++)
        {
            counts[setOfNumbers[i] % divisor]++;
        }

        return counts;
    }

    private static int GetMaximumSubsetLength(int divisor, int[] counts)
    {
        bool divisorIsEven = divisor % 2 == 0;
        int result = counts[0] > 0 ? 1 : 0;

        for (int i = 1; i < (counts.Length / 2) + 1; i++)
        {
            if (divisorIsEven && i == divisor / 2)
            {
                result += counts[i] > 0 ? 1 : 0;
            }
            else
            {
                result += Math.Max(counts[i], counts[counts.Length - i]);
            }
        }

        return result;
    }
}
