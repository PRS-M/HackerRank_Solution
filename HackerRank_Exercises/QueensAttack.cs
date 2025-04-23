using System;
using HackerRank_Exercises.Helpers;

namespace HackerRank_Exercises;

class QueensAttackResult
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

    // To make it more difficult -> No LINQ usage
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

        List<Vector> possibleMoves = [];

        if (queenToObstacleCrossVectors.Count == 0)
        {
            possibleMoves.AddRange(queenToBoundaryVectors);
        }
        else
        {
            for (int i = 0; i < queenToBoundaryVectors.Count; i++)
            {
                Vector queenToBoundaryVector = queenToBoundaryVectors[i];
                foreach (Vector queenToObstacleCrossVector in queenToObstacleCrossVectors)
                {
                    if (IsSameDirection(queenToBoundaryVector, queenToObstacleCrossVector))
                    {
                        possibleMoves.Add(queenToObstacleCrossVector);
                        queenToBoundaryVectorsCopy.Remove(queenToBoundaryVector);
                    }
                }
            }
        }

        if (queenToObstacleCrossVectors.Count != 0)
        {
            possibleMoves.AddRange(queenToBoundaryVectorsCopy);
        }

        if (queenToHypotenuseObstacleVectors.Count == 0)
        {
            possibleMoves.AddRange(queenToHypotenuseBoundaryVectors);
        }
        else
        {
            for (int i = 0; i < queenToHypotenuseBoundaryVectors.Count; i++)
            {
                Vector queenToHypotenuseBoundaryVector = queenToHypotenuseBoundaryVectors[i];
                foreach (Vector queenToHypotenuseObstacleVector in queenToHypotenuseObstacleVectors)
                {
                    if (IsSameDirection(queenToHypotenuseBoundaryVector, queenToHypotenuseObstacleVector))
                    {
                        possibleMoves.Add(queenToHypotenuseObstacleVector);
                        queenToHypotenuseBoundaryVectorsCopy.Remove(queenToHypotenuseBoundaryVector);
                    }
                }
            }
        }

        if (queenToHypotenuseObstacleVectors.Count != 0)
        {
            possibleMoves.AddRange(queenToBoundaryVectorsCopy);
        }
        
        return 0;
    }

    private static List<Vector> GetBoundaryVectors(int n, Point queenPosition)
    {
        return
        [
            new Vector(1 - queenPosition.X, 0),
            new Vector(0, 1 - queenPosition.Y),
            new Vector(n - queenPosition.X, 0),
            new Vector(0, n - queenPosition.Y)
        ];
    }

    private static List<Vector> GetHypotenuseBoundaryVectors(int n, Point queenPosition)
    {
        int[,] intsMatrix = SignCombinationsGenerator.GenerateMatrixCombinations([1, 1]);
        List<Vector> queenToHypotenuseBoundaryVectors = [];
        for (int i = 0; i < intsMatrix.GetLength(0); i++)
        {
            int axisX = queenPosition.X;
            int axisY = queenPosition.Y;

            while (axisX < n && axisY < n && axisX > 1 && axisY > 1)
            {
                axisX += intsMatrix[i, 0];
                axisY += intsMatrix[i, 1];
            }

            if (axisX == n
                || axisX == 1
                || axisY == n
                || axisY == 1)
            {
                Point point = new Point(axisX, axisY);
                Vector vectorToBoundary = new Vector(queenPosition, point);
                queenToHypotenuseBoundaryVectors.Add(vectorToBoundary);

                Console.WriteLine($"Reached boundary: x: {axisX}, y: {axisY}.");
            }
        }

        return queenToHypotenuseBoundaryVectors;
    }

    public static Vector ExtractMovementFromObstacleVector(Vector vector)
    {
        bool recalculated = false;
        if (vector.X < 0)
        {
            vector.X++;
            recalculated = true;
        }
        else if (vector.X > 0)
        {
            vector.X--;
            recalculated = true;
        }

        if (vector.Y < 0)
        {
            vector.Y++;
            recalculated = true;
        }
        else if (vector.Y > 0)
        {
            vector.Y--;
            recalculated = true;
        }

        if (recalculated)
        {
            Vector newVector = new(vector.X, vector.Y);

            return newVector;
        }
        else
        {
            return vector;
        }
    }

    public static bool IsSameDirection(Vector vectorA, Vector vectorB)
    {
        // if (vectorA.X == 0 && vectorA.Y == 0
        //     || vectorB.X == 0 && vectorB.Y == 0)
        // {
        //     return true;
        // }

        if (vectorA.X >= 0 && vectorB.X >= 0
            && vectorA.Y >= 0 && vectorB.Y >= 0)
        {
            return true;
        }

        if (vectorA.X < 0 && vectorB.X < 0
            && vectorA.Y < 0 && vectorB.Y < 0)
        {
            return true;
        }

        if (vectorA.X >= 0 && vectorB.X >= 0
            && vectorA.Y < 0 && vectorB.Y < 0)
        {
            return true;
        }

        if (vectorA.X < 0 && vectorB.X < 0
            && vectorA.Y >= 0 && vectorB.Y >= 0)
        {
            return true;
        }

        return false;
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
        // TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = "5 3".TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);
        int k = Convert.ToInt32(firstMultipleInput[1]);

        string[] secondMultipleInput = "4 3".TrimEnd().Split(' ');

        int r_q = Convert.ToInt32(secondMultipleInput[0]);
        int c_q = Convert.ToInt32(secondMultipleInput[1]);

        List<List<int>> obstacles = new List<List<int>>();
        string[] strings =
        [
            "5 5",
            "4 2",
            "2 3"
        ];

        for (int i = 0; i < k; i++)
        {
            obstacles.Add(strings[i]
                .TrimEnd()
                .Split(' ')
                .Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp))
                .ToList());
        }

        int result = QueensAttackResult.QueensAttack(n, k, r_q, c_q, obstacles);

        // textWriter.WriteLine(result);

        // textWriter.Flush();
        // textWriter.Close();
    }

    // public static void QueensAttackMain(string[] args)
    // {
    //     TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

    //     string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

    //     int n = Convert.ToInt32(firstMultipleInput[0]);
    //     int k = Convert.ToInt32(firstMultipleInput[1]);

    //     string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

    //     int r_q = Convert.ToInt32(secondMultipleInput[0]);
    //     int c_q = Convert.ToInt32(secondMultipleInput[1]);

    //     List<List<int>> obstacles = new List<List<int>>();

    //     for (int i = 0; i < k; i++)
    //     {
    //         obstacles.Add(Console.ReadLine()
    //             .TrimEnd()
    //             .Split(' ')
    //             .Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp))
    //             .ToList());
    //     }

    //     int result = QueensAttackResult.QueensAttack(n, k, r_q, c_q, obstacles);

    //     textWriter.WriteLine(result);

    //     textWriter.Flush();
    //     textWriter.Close();
    // }
}
