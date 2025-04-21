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
        List<PlusPoint> allValidPluses = GetValidPluses(grid);
        return GetMaxArea(allValidPluses);
    }

    private static int GetMaxArea(List<PlusPoint> allValidPluses)
    {
        List<int> areas = [0];
        foreach (PlusPoint plusPoint in allValidPluses)
        {
            List<int> areasForGivenRun = GetNonInterferingAreas(allValidPluses, plusPoint);
            areas.AddRange(areasForGivenRun);
        }

        return areas.Max();
    }

    private static List<PlusPoint> GetValidPluses(List<string> grid)
    {
        List<PlusPoint> allValidPluses = [];

        for (int rowIndex = 0; rowIndex < grid.Count; rowIndex++)
        {
            string row = grid[rowIndex];
            for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
            {
                bool isGoodCell = IsGoodCell(row[columnIndex]);

                if (isGoodCell)
                {
                    int expansionSize = CalculateExpansionLimit(rowIndex, columnIndex, grid);
                    for (int i = 0; i <= expansionSize; i++)
                    {
                        allValidPluses.Add(new PlusPoint(columnIndex, rowIndex, i));
                    }
                }
            }
        }

        return allValidPluses;
    }

    private static List<int> GetNonInterferingAreas(List<PlusPoint> listOfValidPluses, PlusPoint plusPoint)
    {
        List<int> areas = [];
        foreach (PlusPoint secondPlusPoint in listOfValidPluses)
        {
            bool isNotInterfering = !plusPoint.IsInterfering(secondPlusPoint);
            if (isNotInterfering)
            {
                areas.Add(plusPoint.Area * secondPlusPoint.Area);
            }
        }

        return areas;
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

        for (int i = 0; i <= ExpansionSize; i++)
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

        // List<string> grid =
        // [
        //     "GGGGGG",
        //     "GBBBGB",
        //     "GGGGGG",
        //     "GGBBGB",
        //     "GGGGGG",
        // ];
        
        // Ans: 81
        List<string> grid =
        [
            "BBBGBGBBB",
            "BBBGBGBBB",
            "BBBGBGBBB",
            "GGGGGGGGG",
            "BBBGBGBBB",
            "BBBGBGBBB",
            "GGGGGGGGG",
            "BBBGBGBBB",
            "BBBGBGBBB",
            "BBBGBGBBB"
        ];



        int result = EmasSupercomputerResult.TwoPluses(grid);
        return result;

        // textWriter.WriteLine(result);

        // textWriter.Flush();
        // textWriter.Close();
    }
}
