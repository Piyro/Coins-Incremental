using CoinTowerIdle.Numbers;
using System;

namespace CoinTowerIdle.Numbers
{
    [Serializable]
    public struct BigNumber :
        IComparable<BigNumber>,
        IEquatable<BigNumber>
    {
        private const double BaseValue = 1000d;

        public double Mantissa;
        public int Exponent;

        public static readonly BigNumber Zero = new(0);
        public static readonly BigNumber One = new(1);

        public BigNumber(double value)
        {
            Mantissa = value;
            Exponent = 0;

            Normalize();
        }

        public BigNumber(double mantissa, int exponent)
        {
            Mantissa = mantissa;
            Exponent = exponent;

            Normalize();
        }

        private void Normalize()
        {
            if (Mantissa == 0)
            {
                Exponent = 0;
                return;
            }

            while (System.Math.Abs(Mantissa) >= BaseValue)
            {
                Mantissa /= BaseValue;
                Exponent += 3;
            }

            while (System.Math.Abs(Mantissa) < 1 &&
                   System.Math.Abs(Mantissa) > 0)
            {
                Mantissa *= BaseValue;
                Exponent -= 3;
            }
        }

        #region Operators

        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            if (a.Mantissa == 0) return b;
            if (b.Mantissa == 0) return a;

            if (a.Exponent == b.Exponent)
            {
                return new BigNumber(
                    a.Mantissa + b.Mantissa,
                    a.Exponent);
            }

            if (a.Exponent > b.Exponent)
            {
                double diff = (a.Exponent - b.Exponent) / 3.0;

                return new BigNumber(
                    a.Mantissa +
                    b.Mantissa / System.Math.Pow(BaseValue, diff),
                    a.Exponent);
            }
            else
            {
                double diff = (b.Exponent - a.Exponent) / 3.0;

                return new BigNumber(
                    a.Mantissa / System.Math.Pow(BaseValue, diff) +
                    b.Mantissa,
                    b.Exponent);
            }
        }

        public static BigNumber operator -(BigNumber a, BigNumber b)
        {
            return a + new BigNumber(-b.Mantissa, b.Exponent);
        }

        public static BigNumber operator *(BigNumber a, BigNumber b)
        {
            return new BigNumber(
                a.Mantissa * b.Mantissa,
                a.Exponent + b.Exponent);
        }

        public static BigNumber operator /(BigNumber a, BigNumber b)
        {
            return new BigNumber(
                a.Mantissa / b.Mantissa,
                a.Exponent - b.Exponent);
        }

        public static BigNumber operator *(BigNumber a, double b)
        {
            return new BigNumber(
                a.Mantissa * b,
                a.Exponent);
        }

        public static BigNumber operator /(BigNumber a, double b)
        {
            return new BigNumber(
                a.Mantissa / b,
                a.Exponent);
        }

        #endregion

        #region Comparisons

        public static bool operator >(BigNumber a, BigNumber b)
        {
            if (a.Exponent != b.Exponent)
                return a.Exponent > b.Exponent;

            return a.Mantissa > b.Mantissa;
        }

        public static bool operator <(BigNumber a, BigNumber b)
        {
            if (a.Exponent != b.Exponent)
                return a.Exponent < b.Exponent;

            return a.Mantissa < b.Mantissa;
        }

        public static bool operator >=(BigNumber a, BigNumber b)
        {
            return !(a < b);
        }

        public static bool operator <=(BigNumber a, BigNumber b)
        {
            return !(a > b);
        }

        public static bool operator ==(BigNumber a, BigNumber b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(BigNumber a, BigNumber b)
        {
            return !a.Equals(b);
        }

        #endregion

        #region Interfaces

        public int CompareTo(BigNumber other)
        {
            if (this > other) return 1;
            if (this < other) return -1;
            return 0;
        }

        public bool Equals(BigNumber other)
        {
            return Mantissa.Equals(other.Mantissa)
                && Exponent == other.Exponent;
        }

        public override bool Equals(object obj)
        {
            return obj is BigNumber other &&
                   Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Mantissa, Exponent);
        }

        #endregion

        #region Helpers

        public static BigNumber Max(BigNumber a, BigNumber b)
        {
            return a > b ? a : b;
        }

        public static BigNumber Min(BigNumber a, BigNumber b)
        {
            return a < b ? a : b;
        }

        public bool IsZero => Mantissa == 0;

        public override string ToString()
        {
            return BigNumberFormatter.Format(this);
        }

        #endregion

        #region Implicit

        public static implicit operator BigNumber(double value)
        {
            return new BigNumber(value);
        }

        #endregion
    }
}