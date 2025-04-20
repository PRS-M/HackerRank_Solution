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

class MagicSquareResult
{
    /*
     * Complete the 'formingMagicSquare' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY s as parameter.
     */
     
    private delegate int CellFormula(int a, int b);
    
    public static int FormingMagicSquare(List<List<int>> s)
    {
        /* Simplification - use of Edouard Lucas' method for creation of the magic squares
         * a and b can hold only values of 1 and 3, otherwise numbers would repeat
         * or exceed assigned range of numbers, while the center value is always equal to 5.
         * Additionally, to construct all combinations of the square
         * "-" sign must be added in all possible combinations to the numbers.
         */
                
        int centerValue = s[1][1];
        Console.WriteLine($"Center value: {centerValue}.");
        
        CellFormula[,] magicSquareFormulas = new CellFormula[,]
        {
            { (a, b) => 5 - b, (a, b) => 5 + (a + b), (a, b) => 5 - a },
            { (a, b) => 5 - (a - b), (a, b) => 5, (a, b) => 5 + (a - b) },
            { (a, b) => 5 + a, (a, b) => 5 - (a + b), (a, b) => 5 + b }
        };
        
        int[] components = {1, 3};
        int[][] combinations = CreateAllCombinations(components);
        
        int[][,] magicSquaresArray = CreateAllPossibleMagicSquares(combinations, magicSquareFormulas);
        
        int[] conversionCosts = GetAllConversionsCosts(combinations.Length, magicSquaresArray, s);

        return conversionCosts.Min();
    }
    
    private static int[][] CreateAllCombinations(int[] components)
    {
        int combinations = 1 << components.Length;
        int[][] combinedComponents = new int[combinations * 2][];
        
        for (int mask = 0; mask < combinations; mask++)
        {
            int[] combination = CreateCombination(mask, components);
            int[] reversedCombination = combination.Reverse().ToArray();
            
            combinedComponents[mask] = combination;
            combinedComponents[mask + combinations] = reversedCombination;
        }
        
        foreach (int[] combination in combinedComponents)
        {
            Console.WriteLine($"Combination: [{string.Join(", ", combination)}]");
        }
        
        Console.WriteLine();        
        return combinedComponents;
    }
    
    private static int[] CreateCombination(int mask, int[] components)
    {
        int length = components.Length;
        int[] combination = new int[length];
            
        for (int i = 0; i < length; i++)
        {
            if ((mask & (1 << i)) == 0)
            {
                combination[i] = components[i];
            }   
            else
            {
                combination[i] = -components[i];
            }
        }
        
        return combination;
    }
    
    private static int[][,] CreateAllPossibleMagicSquares(int[][] combinations, CellFormula[,] magicSquareFormulas)
    {
        int[][,] magicSquaresArray = new int[combinations.Length][,];
        
        for (int combinationNumber = 0; combinationNumber < combinations.Length; combinationNumber++)
        {
            int[] combination = combinations[combinationNumber];
            int[,] magicSquare = new int[3,3];
            
            for (int i = 0; i < magicSquareFormulas.GetLength(0); i++)
            {
                for (int j = 0; j < magicSquareFormulas.GetLength(1); j++)
                {
                    int cellValue = magicSquareFormulas[i,j](combination[0], combination[1]);
                    magicSquare[i,j] = cellValue;
                    Console.Write($"{cellValue} ");
                }
                
                Console.WriteLine();
            }
            
            magicSquaresArray[combinationNumber] = magicSquare;
            Console.WriteLine();
        }
        
        return magicSquaresArray;
    }
    
    private static int GetConversionCost(int value, int newValue)
    {
        return Math.Abs(value - newValue);
    }
    
    private static int[] GetAllConversionsCosts(int numberOfCases, int[][,] magicSquaresArray, List<List<int>> input)
    {
        int[] conversionCosts = new int[numberOfCases];
        
        for (int i = 0; i < magicSquaresArray.Length; i++)
        {
            int conversionCost = 0;
            for (int j = 0; j < magicSquaresArray[i].GetLength(0); j++)
            {
                for (int k = 0; k < magicSquaresArray[i].GetLength(1); k++)
                {
                    int cellValue = magicSquaresArray[i][j,k];
                    int inputCellValue = input[j][k];
                
                    conversionCost += GetConversionCost(inputCellValue, cellValue);
                }
            }
            
            conversionCosts[i] = conversionCost;
        }
        
        return conversionCosts;
    }
}

public class MagicSquareSolution
{
    public static int Main(string[] args)
    {
        // TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);
        TextWriter textWriter = new StreamWriter("./outputFile.txt", true);

        List<List<int>> s = new List<List<int>>();

        for (int i = 0; i < 3; i++)
        {
            s.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToInt32(sTemp)).ToList());
        }

        int result = MagicSquareResult.FormingMagicSquare(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();

        return result;
    }
}
