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
            "No",
            "Dc"
        };

        public static string Format(double value)
        {
            int suffix = 0;

            while (value >= 1000 &&
                   suffix < suffixes.Length - 1)
            {
                value /= 1000;
                suffix++;
            }

            return value.ToString("0.##") +
                   suffixes[suffix];
        }
    }
}