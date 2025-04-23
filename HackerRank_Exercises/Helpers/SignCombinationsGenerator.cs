using System;

namespace HackerRank_Exercises.Helpers;

public static class SignCombinationsGenerator
{
    public static int[][] GenerateJaggedCombinations(int[] components)
    {
        int length = components.Length;
        int totalCombinations = 1 << length;
        int[][] combinations = new int[totalCombinations][];

        for (int mask = 0; mask < totalCombinations; mask++)
        {
            combinations[mask] = CreateCombination(mask, components);
        }

        return combinations;
    }

    public static int[,] GenerateMatrixCombinations(int[] components)
    {
        int length = components.Length;
        int totalCombinations = 1 << length;
        int[,] combinations = new int[totalCombinations, length];

        for (int mask = 0; mask < totalCombinations; mask++)
        {
            int[] combination = CreateCombination(mask, components);
            for (int index = 0; index < combination.Length; index++)
            {
                combinations[mask, index] = combination[index];
            }
        }

        return combinations;
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
}
