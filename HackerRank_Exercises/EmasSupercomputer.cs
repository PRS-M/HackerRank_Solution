using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

namespace HackerRank_Exercises;

class EmasSupercomputerResult
{

    /*
     * Complete the 'twoPluses' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING_ARRAY grid as parameter.
     */

    public static int TwoPluses(List<string> grid)
    {
        return GetMaxArea(grid);
    }

    private static int GetMaxArea(List<string> grid)
    {
        List<PlusPoint> listOfAllValidPluses = [];
        List<PlusPoint> listOfValidPlusesGreaterThanOne = [];

        int largestExpansionSize = 0;
        for (int i = 0; i < grid.Count; i++)
        {
            string row = grid[i];
            for (int j = 0; j < row.Length; j++)
            {
                bool isGoodCell = IsGoodCell(row[j]);

                if (isGoodCell)
                {
                    int expansionSize = CalculateExpansionLimit(i, j, grid);

                    if (expansionSize > 0 && expansionSize >= largestExpansionSize)
                    {
                        listOfValidPlusesGreaterThanOne.Add(new PlusPoint(j, i, expansionSize));
                    }

                    if (expansionSize > largestExpansionSize)
                    {
                        largestExpansionSize = expansionSize;
                    }


                    listOfAllValidPluses.Add(new PlusPoint(j, i, expansionSize));
                }
            }
        }

        List<int> Areas = [];
        foreach (PlusPoint plusPoint in listOfValidPlusesGreaterThanOne)
        {
            foreach (PlusPoint secondPlusPoint in listOfValidPlusesGreaterThanOne)
            {
                bool isNotInterfering = !plusPoint.IsInterfering(secondPlusPoint);
                if (isNotInterfering)
                {
                    Areas.Add(plusPoint.Area * secondPlusPoint.Area);
                }
            }

            if (Areas.Count == 0)
            {
                foreach (PlusPoint secondPlusPoint in listOfAllValidPluses)
                {
                    bool isNotInterfering = !plusPoint.IsInterfering(secondPlusPoint);
                    if (isNotInterfering)
                    {
                        Areas.Add(plusPoint.Area * secondPlusPoint.Area);
                    }
                }
            }
        }

        return Areas.Max();
    }

    private static bool IsGoodCell(char cell)
    {
        return cell == 'G';
    }

    private static int CalculateExpansionLimit(int rowNumber, int columnNumber, List<string> grid)
    {
        int expansionRunNumber = 0;
        bool canExpand;
        do
        {
            int nextRowUp = rowNumber - 1 - expansionRunNumber;
            bool canExpandUp =
                nextRowUp >= 0
                && IsGoodCell(grid[nextRowUp][columnNumber]);

            int nextRowDown = rowNumber + 1 + expansionRunNumber;
            bool canExpandDown =
                nextRowDown < grid.Count
                && IsGoodCell(grid[nextRowDown][columnNumber]);

            int nextColumnLeft = columnNumber - 1 - expansionRunNumber;
            bool canExpandLeft =
                nextColumnLeft >= 0
                && IsGoodCell(grid[rowNumber][nextColumnLeft]);

            int nextColumnRight = columnNumber + 1 + expansionRunNumber;
            bool canExpandRight =
                nextColumnRight < grid[rowNumber].Length
                && IsGoodCell(grid[rowNumber][nextColumnRight]);

            canExpand = canExpandUp
                        && canExpandDown
                        && canExpandLeft
                        && canExpandRight;

            if (canExpand)
            {
                expansionRunNumber++;
            }
        } while (canExpand);

        return expansionRunNumber;
    }
}

internal readonly record struct PlusPoint(int X, int Y, int ExpansionSize)
{
    public int Area => 1 + (4 * ExpansionSize);

    public List<(int, int)> GetAllCoordinates()
    {
        List<(int, int)> coordinates = [(X, Y)];

        for (int i = 0; i < ExpansionSize; i++)
        {
            coordinates.Add((X - i, Y));
            coordinates.Add((X + i, Y));
            coordinates.Add((X, Y - i));
            coordinates.Add((X, Y + i));
        }

        return coordinates;
    }

    public bool IsInterfering(PlusPoint other)
    {
        List<(int, int)> thisCoordinates = GetAllCoordinates();
        List<(int, int)> otherCoordinates = other.GetAllCoordinates();

        foreach ((int, int) coordinate in thisCoordinates)
        {
            foreach((int, int) otherCoordinate in otherCoordinates)
            {
                if (coordinate == otherCoordinate)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

public class EmasSupercomputerSolution
{
    public static int Main(string[] args)
    {
        // TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);
        // TextWriter textWriter = new StreamWriter("./outputFile.txt", true);

        // string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        // int n = Convert.ToInt32(firstMultipleInput[0]);

        // int m = Convert.ToInt32(firstMultipleInput[1]);

        // List<string> grid = new List<string>();

        // for (int i = 0; i < n; i++)
        // {
        //     string gridItem = Console.ReadLine();
        //     grid.Add(gridItem);
        // }

        // List<string> grid =
        // [
        //     "BGBBGB",
        //     "GGGGGG",
        //     "BGBBGB",
        //     "GGGGGG",
        //     "BGBBGB",
        //     "BGBBGB",
        // ];

        List<string> grid =
        [
            "GGGGGG",
            "GBBBGB",
            "GGGGGG",
            "GGBBGB",
            "GGGGGG",
        ];

        int result = EmasSupercomputerResult.TwoPluses(grid);
        return result;

        // textWriter.WriteLine(result);

        // textWriter.Flush();
        // textWriter.Close();
    }
}
