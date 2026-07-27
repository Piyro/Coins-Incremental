using UnityEngine;

namespace CoinTowerIdle.Utilities
{
    public static class NumberFormatter
    {
        private static readonly string[] suffixes =
        {
            "",
            "K",
            "M",
            "B",
            "T",
            "Qa",
            "Qi",
            "Sx",
            "Sp",
            "Oc",
            "No"
        };

        public static string Format(double value)
        {
            int index = 0;

            while (value >= 1000 && index < suffixes.Length - 1)
            {
                value /= 1000;
                index++;
            }

            return $"{value:0.##}{suffixes[index]}";
        }
    }
}