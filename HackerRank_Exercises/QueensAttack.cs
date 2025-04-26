using System;
using System.Runtime.CompilerServices;
using HackerRank_Exercises.Helpers;

[assembly:InternalsVisibleTo("HackerRank_UnitTests")]
namespace HackerRank_Exercises;

public class QueensAttackResult
{

    /*
     * Complete the 'queensAttack' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  3. INTEGER r_q
     *  4. INTEGER c_q
     *  5. 2D_INTEGER_ARRAY obstacles
     */

    // To make it a bit more difficult -> No LINQ usage
    public static int QueensAttack(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    {
        Point queenPosition = new Point(c_q, r_q);

        // Vectors from queen to obstacles.
        List<Vector> queenToObstacleCrossVectors = [];
        List<Vector> queenToHypotenuseObstacleVectors = [];

        for (int i = 0; i < obstacles.Count; i++)
        {
            Point obstaclePoint = new Point(obstacles[i][1], obstacles[i][0]);
            Vector vector = new Vector(queenPosition, obstaclePoint)
            {
                Obstacle = true
            };

            // Add only blocking obstacles (8 directions).
            // This way non-blocking obstacles are removed.
            if (vector.X == 0 && vector.Y != 0
                || vector.Y == 0 && vector.X != 0)
            {
                queenToObstacleCrossVectors.Add(vector);
            }
            else if (Math.Abs(vector.X) == Math.Abs(vector.Y))
            {
                queenToHypotenuseObstacleVectors.Add(vector);
            }

            Console.WriteLine($"Obstacle: {obstaclePoint.X}, {obstaclePoint.Y}");
            Console.WriteLine($"Vector: {vector.X}, {vector.Y}");
        }

        // Vectors from queen to the boundaries (last cell inclusive)
        List<Vector> queenToHypotenuseBoundaryVectors = GetHypotenuseBoundaryVectors(n, queenPosition);
        List<Vector> queenToBoundaryVectors = GetBoundaryVectors(n, queenPosition);

        List<Vector> queenToHypotenuseBoundaryVectorsCopy = new(queenToHypotenuseBoundaryVectors);
        List<Vector> queenToBoundaryVectorsCopy = new(queenToBoundaryVectors);

        HashSet<Vector> possibleMoves = [];

        // Main directions
        EvaluateQueenMoveVectors(queenToObstacleCrossVectors, queenToBoundaryVectors, queenToBoundaryVectorsCopy, possibleMoves);

        // Diagonal directions
        EvaluateQueenMoveVectors(queenToHypotenuseObstacleVectors, queenToHypotenuseBoundaryVectors, queenToHypotenuseBoundaryVectorsCopy, possibleMoves);

        // Obstacle vectors conversion to possible move vectors
        NormalizeMoveVectors(possibleMoves);

        // Calculate the amount of fields available for movement.
        int totalLength = GetTotalLength(possibleMoves);
        Console.WriteLine(totalLength);
        return totalLength;
    }

    private static void EvaluateQueenMoveVectors(
        List<Vector> queenToObstacleVectors,
        List<Vector> queenToBoundaryVectors,
        List<Vector> queenToBoundaryVectorsCopy,
        HashSet<Vector> possibleMoves)
    {
        if (queenToObstacleVectors.Count == 0)
        {
            possibleMoves.UnionWith(queenToBoundaryVectors);
        }
        else
        {
            for (int i = 0; i < queenToBoundaryVectors.Count; i++)
            {
                Vector queenToBoundaryVector = queenToBoundaryVectors[i];
                foreach (Vector queenToObstacleCrossVector in queenToObstacleVectors)
                {
                    if (queenToBoundaryVector.IsSameDirection(queenToObstacleCrossVector))
                    {
                        possibleMoves.Add(queenToObstacleCrossVector);
                        queenToBoundaryVectorsCopy.Remove(queenToBoundaryVector);
                    }
                }
            }
        }

        if (queenToObstacleVectors.Count != 0)
        {
            possibleMoves.UnionWith(queenToBoundaryVectorsCopy);
        }
    }

    private static void NormalizeMoveVectors(HashSet<Vector> possibleMoves)
    {
        HashSet<Vector> tempNew = new HashSet<Vector>();
        HashSet<Vector> tempOld = new HashSet<Vector>();

        foreach (Vector v in possibleMoves)
        {
            if (v.Obstacle)
            {
                Vector converted  = ConvertObstacleToMoveVector(v);

                tempNew.Add(converted);
                tempOld.Add(v);
            }
        }

        possibleMoves.ExceptWith(tempOld);
        possibleMoves.UnionWith(tempNew);
    }

    private static int GetTotalLength(HashSet<Vector> vectors)
    {
        int totalLength = 0;
        foreach (Vector vector in vectors)
        {
            totalLength += vector.GetLength();
        }

        return totalLength;
    }

    private static List<Vector> GetBoundaryVectors(int n, Point queenPosition)
    {
        List<Vector> vectors = 
        [
            new Vector(1 - queenPosition.X, 0),
            new Vector(0, 1 - queenPosition.Y),
            new Vector(n - queenPosition.X, 0),
            new Vector(0, n - queenPosition.Y)
        ];

        for (int i = 0; i < vectors.Count; i++)
        {
            Vector vector = vectors[i];
            if (vector.X == 0 && vector.Y == 0)
            {
                vectors.Remove(vector);
            }
        }

        return vectors;
    }

    private static List<Vector> GetHypotenuseBoundaryVectors(int n, Point queenPosition)
    {
        int[,] directionsMatrix = SignCombinationsGenerator.GenerateMatrixCombinations([1, 1]);
        List<Vector> queenToHypotenuseBoundaryVectors = [];

        for (int i = 0; i < directionsMatrix.GetLength(0); i++)
        {
            int axisX = queenPosition.X;
            int axisY = queenPosition.Y;

            while (axisX <= n
                   && axisX >= 1
                   && axisY <= n
                   && axisY >= 1)
            {
                axisX += directionsMatrix[i, 0];
                axisY += directionsMatrix[i, 1];
            }

            bool pastEdgesAfterConversion = axisX > n
                                        || axisX < 1
                                        || axisY > n
                                        || axisY < 1;

            if (pastEdgesAfterConversion)
            {
                axisX -= directionsMatrix[i, 0];
                axisY -= directionsMatrix[i, 1];
                if (axisX != queenPosition.X && axisY != queenPosition.Y)
                {
                    Point point = new Point(axisX, axisY);
                    Vector vectorToBoundary = new Vector(queenPosition, point);
                    queenToHypotenuseBoundaryVectors.Add(vectorToBoundary);
                }

                Console.WriteLine($"Reached boundary: x: {axisX}, y: {axisY}.");
            }
        }

        return queenToHypotenuseBoundaryVectors;
    }

    private static Vector ConvertObstacleToMoveVector(Vector vector)
    {
        if (vector.X < 0)
        {
            vector.X++;
        }
        else if (vector.X > 0)
        {
            vector.X--;
        }

        if (vector.Y < 0)
        {
            vector.Y++;
        }
        else if (vector.Y > 0)
        {
            vector.Y--;
        }

        return new Vector(vector);
    }
}

internal record struct Vector
{
    public Vector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector(Vector vector)
    {
        X = vector.X;
        Y = vector.Y;
    }

    public Vector(Point pointA, Point pointB)
    {
        X = pointB.X - pointA.X;
        Y = pointB.Y - pointA.Y;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public bool Obstacle { get; set; } = false;

    public readonly int GetLength()
    {
        if (X == 0 && Y == 0)
        {
            return 0;
        }

        if (X == 0)
        {
            return Math.Abs(Y);
        }

        if (Y == 0)
        {
            return Math.Abs(X);
        }

        if (X != 0 && Math.Abs(X) == Math.Abs(Y))
        {
            return Math.Abs(X);
        }

        if (X != 0 && Math.Abs(X) != Math.Abs(Y))
        {
            return (int)Math.Sqrt(X * X + Y * Y);
        }

        return 0;
    }

    public readonly bool IsSameDirection(Vector other)
    {
        bool sameSignX = X * other.X > 0;
        bool sameSignY = Y * other.Y > 0;

        // Zero-length vector
        if (X == 0 && Y == 0 || other.X == 0 && other.Y == 0)
            return true;

        // Vertical, same direction
        if (X == 0 && Y != 0 && sameSignY)
            return true;

        // Horizontal, same direction
        if (Y == 0 && X != 0 && sameSignX)
            return true;

        // Diagonal
        if (sameSignX && sameSignY
            && (X == other.X && Y == other.Y || Math.Abs(X) == Math.Abs(Y)))
            return true;

        return false;
    }

    public static bool operator > (Vector a, Vector b)
    {
        return a.GetLength() > b.GetLength() && a.IsSameDirection(b);
    }

    public static bool operator < (Vector a, Vector b)
    {
        return a.GetLength() < b.GetLength() && a.IsSameDirection(b);
    }

    public static bool operator >= (Vector a, Vector b)
    {
        return a == b || a.GetLength() > b.GetLength() && a.IsSameDirection(b);
    }

        public static bool operator <= (Vector a, Vector b)
    {
        return a == b || a.GetLength() < b.GetLength() && a.IsSameDirection(b);
    }

    public static Vector operator +(Vector a, Vector b)
    {
        return new Vector(a.X + b.X, a.Y + b.Y);
    }

    public static Vector operator -(Vector a, Vector b)
    {
        return new Vector(a.X - b.X, a.Y - b.Y);
    }
}

internal record struct Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(Point point)
    {
        X = point.X;
        Y = point.Y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}

public class QueensAttackSolution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);
        int k = Convert.ToInt32(firstMultipleInput[1]);

        string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r_q = Convert.ToInt32(secondMultipleInput[0]);
        int c_q = Convert.ToInt32(secondMultipleInput[1]);

        List<List<int>> obstacles = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            obstacles.Add(Console.ReadLine()
                .TrimEnd()
                .Split(' ')
                .Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp))
                .ToList());
        }

        int result = QueensAttackResult.QueensAttack(n, k, r_q, c_q, obstacles);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
