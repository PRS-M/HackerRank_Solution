using System.Numerics;

namespace HackerRank_Exercises;

class ExtraLongFactorialsResult
{

    /*
     * Complete the 'extraLongFactorials' function below.
     *
     * The function accepts INTEGER n as parameter.
     */

    public static string ExtraLongFactorials(int n)
    {
        ulong result = 1;
        ushort index = 1;
        bool overflow = false;

        checked
        {
            for (; index <= n; index++)
            {
                overflow = (int)Math.Log10(result) + 1 >= 19;

                if (overflow)
                    break;

                result *= index;
            }
        }

        if (overflow)
        {
            BigInteger bigIndex;
            BigInteger bigResult;

            bigIndex = new BigInteger(index);
            bigResult = new BigInteger(result);

            for (; bigIndex <= n; bigIndex++)
            {
                bigResult *= bigIndex;
            }

            return bigResult.ToString();
        }

        return result.ToString();
    }
}

class ExtraLongFactorialsSolution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());
        string result = ExtraLongFactorialsResult.ExtraLongFactorials(n);
        Console.WriteLine(result);
    }
}