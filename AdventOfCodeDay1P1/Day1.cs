namespace Day1
{
    public class Day1
    {
        public static void Main()
        {

            var data = File.ReadAllLines("DataSet.txt");

            var sum = data.Sum(x => GetCalibrationFromString(x));
        }

        public static double GetCalibrationFromString(string val)
        {
            val = ReplaceWord(val.ToLower());

            var first = val.Where(x => Char.IsDigit(x)).FirstOrDefault();
            var last = val.Where(x => Char.IsDigit(x)).LastOrDefault();

            return int.Parse(string.Concat(first, last));
        }

        public static string ReplaceWord(string val)
        {
            foreach(var num in WordToInt)
            {
                if(val.Contains(num.Key))
                    val = val.Replace(num.Key, num.Value);
            }

            return val;
        }

        public static Dictionary<string, string> WordToInt => new() {
            { "one","o1ne" },
            {"two","tw2o" },
            {"three","thr3ee" },
            {"four","fo4ur" },
            {"five","fi5ve" },
            {"six","s6ix" },
            {"seven","sev7en" },
            {"eight","eig8ht" },
            {"nine","ni9ne"}
        };
    }
}