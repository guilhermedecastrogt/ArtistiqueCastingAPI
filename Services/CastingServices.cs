﻿using System.Globalization;
using System.Text;

namespace ArtistiqueCastingAPI.Services;

public static class CastingServices
{
    public static List<string> ConverteStringSubCategoriesToList(string value)
    {
        string[] subCategoriesArray = value.Split(new string[]{", "}, StringSplitOptions.None);
        return new List<string>(subCategoriesArray);
    }
    public static string RemoveAccents(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        string normalized = input.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString();
    }
    
    public static int CalculateLevenshteinSimilarity(string s1, string s2)
    {
        int[,] distance = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i <= s1.Length; i++)
            distance[i, 0] = i;

        for (int j = 0; j <= s2.Length; j++)
            distance[0, j] = j;

        for (int i = 1; i <= s1.Length; i++)
        {
            for (int j = 1; j <= s2.Length; j++)
            {
                int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;

                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost);
            }
        }

        return distance[s1.Length, s2.Length];
    }
}