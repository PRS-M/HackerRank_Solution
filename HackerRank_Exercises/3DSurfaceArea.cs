namespace HackerRank_Exercises;

class _3DSurfaceAreaResult
{

    /*
     * Complete the 'surfaceArea' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY A as parameter.
     */

    public static int SurfaceArea(List<List<int>> A)
    {
        int height = A.Count;
        int width = A[0].Count;

        int area = 2 * height * width;

        for (int row = 0; row < height; row++)
        {
            for (int stack = 0; stack < width; stack++)
            {
                if (stack == 0)
                    area += A[row][stack];
                else
                    area += Math.Abs(A[row][stack] - A[row][stack - 1]);

                if (row == 0)
                    area += A[row][stack];
                else
                    area +=  Math.Abs(A[row][stack] - A[row - 1][stack]);

                if (stack == width - 1)
                    area += A[row][stack];

                if (row == height - 1)
                    area += A[row][stack];
            }
        }

        return area;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int H = Convert.ToInt32(firstMultipleInput[0]);

        int W = Convert.ToInt32(firstMultipleInput[1]);

        List<List<int>> A = new List<List<int>>();

        for (int i = 0; i < H; i++)
        {
            A.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList());
        }

        int result = _3DSurfaceAreaResult.SurfaceArea(A);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
