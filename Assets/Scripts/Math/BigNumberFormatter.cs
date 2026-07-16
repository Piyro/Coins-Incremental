namespace CoinTowerIdle.Numbers
{
    public static class BigNumberFormatter
    {
        private static readonly string[] suffix =
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
            "Dc",
            "Ud",
            "Dd",
            "Td",
            "Qad",
            "Qid"
        };

        public static string Format(BigNumber number)
        {
            if (number.Mantissa == 0)
                return "0";

            int index = number.Exponent / 3;

            if (index >= 0 && index < suffix.Length)
            {
                return number.Mantissa.ToString("0.##")
                       + suffix[index];
            }

            return number.Mantissa.ToString("0.###")
                   + "e"
                   + number.Exponent;
        }
    }
}