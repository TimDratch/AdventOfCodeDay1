using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day2
{
    public static class Day2
    {
        public static void Main()
        {
            var data = File.ReadAllLines("DataSet.txt");
            var sum = 0;
            var power = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += GetGameStatus(data[i]) ? i + 1 : 0;
                power += GetPower(data[i]);

            }
        }
        public static int GetPower(string game)
        {
            var split = StringSplitter(game);

            var lowest = new Dictionary<ColorLimits, int?>()
                {
                    {ColorLimits.red, null },
                    {ColorLimits.green, null },
                    {ColorLimits.blue, null }
                };

            foreach (string s in split)
            {
                var values = s.Split(',');

                var colorSplit = new Dictionary<ColorLimits, string[]?>()
                {
                    {ColorLimits.red, values.Where(x => x.Contains("red")).ToArray() },
                    {ColorLimits.green, values.Where(x => x.Contains("green")).ToArray() },
                    {ColorLimits.blue, values.Where(x => x.Contains("blue")).ToArray() }
                };

                var roundMin = GetMinimum(colorSplit);

                foreach (var color in lowest)
                {
                    if (color.Value == null || color.Value < roundMin[color.Key])
                        lowest[color.Key] = roundMin[color.Key];
                }
            }

            return lowest.Values.Aggregate((x, y) => x * y).GetValueOrDefault();
        }

        public static Dictionary<ColorLimits, int?> GetMinimum(Dictionary<ColorLimits, string[]?> colors)
        {
                return new Dictionary<ColorLimits, int?>()
                {
                    {ColorLimits.red, colors[ColorLimits.red]?.Select(x => GetValue(x)).OrderByDescending(x => x).FirstOrDefault()},
                    {ColorLimits.green, colors[ColorLimits.green]?.Select(x => GetValue(x)).OrderByDescending(x => x).FirstOrDefault() },
                    {ColorLimits.blue, colors[ColorLimits.blue]?.Select(x => GetValue(x)).OrderByDescending(x => x).FirstOrDefault() }
                };
        }

        public static bool GetGameStatus(string game)
        {
            var split = StringSplitter(game);

            foreach (var item in split)
            {
                var values = item.Split(',');

                var red = values.Where(x => x.Contains("red")).ToArray();
                var green = values.Where(x => x.Contains("green")).ToArray();
                var blue = values.Where(x => x.Contains("blue")).ToArray();

                if (!CompileCount(ColorLimits.red, red) ||
                    !CompileCount(ColorLimits.green, green) ||
                    !CompileCount(ColorLimits.blue, blue))
                    return false;
            }

            return true;
        }

        public static bool CompileCount(ColorLimits colorLimits, string[] values)
        {
            return (int)colorLimits >= values.Sum(x => GetValue(x));
        }

        public static int GetValue(string str)
        {
            return int.Parse(Regex.Match(str, @"\d+").Value);
        }

        public enum ColorLimits
        {
            red = 12,
            green = 13,
            blue = 14,
        }

        public static string[] StringSplitter(string str)
        {
            return str.Split(':')
                .Last()
                .Split(';');
        }
    }
}