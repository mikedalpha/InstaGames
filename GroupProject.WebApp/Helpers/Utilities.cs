using System;
using System.Collections.Generic;

namespace GroupProject.WebApp.Helpers
{
    public static class Utilities
    {
        public static string SummarizeText(string description, int maxLength)
        {
            if (description.Length < maxLength) return description;

            var words = description.Split(' ');
            var totalCharacters = 0;
            var summary = new List<string>();

            foreach (var word in words)
            {
                summary.Add(word);
                totalCharacters += word.Length + 1;
                if (totalCharacters > maxLength)
                    break;
            }

            return String.Join(" ", summary) + "...";
        }

        public static int CalculateAge(DateTime date)
        {
            var age = DateTime.Today.Year - date.Year;
            if (date.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }
    }
}