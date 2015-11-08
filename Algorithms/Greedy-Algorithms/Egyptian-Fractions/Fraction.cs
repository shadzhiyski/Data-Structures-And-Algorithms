namespace Egyptian_Fractions
{
    internal class Fraction
    {
        public long Numerator { get; set; }
        public long Denominator { get; set; }

        public Fraction(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        protected static long LeastCommonMultiple(long num1, long num2)
        {
            long chekNum1 = num1, chekNum2 = num2;

            while (chekNum2 != chekNum1)
            {
                if (chekNum2 > chekNum1)
                {
                    chekNum1 += num1;
                }
                else
                {
                    chekNum2 += num2;
                }
            }

            return chekNum1;
        }

        public static Fraction operator - (Fraction fraction1, Fraction fraction2)
        {
            long leastCommonMultiple = 
                LeastCommonMultiple(fraction1.Denominator, fraction2.Denominator);
            long numerator1 = (leastCommonMultiple / fraction1.Denominator) * fraction1.Numerator;
            long numerator2 = (leastCommonMultiple / fraction2.Denominator) * fraction2.Numerator;

            return new Fraction(numerator1 - numerator2, leastCommonMultiple);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Numerator, Denominator);
        }
    }
}