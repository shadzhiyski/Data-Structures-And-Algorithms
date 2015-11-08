namespace Egyptian_Fractions
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static List<Fraction> fractions = new List<Fraction>();

        static void Main(string[] args)
        {
            SolveEgyptianFractionByFibonacci(new Fraction(521, 1050));

            foreach (var fraction in fractions)
            {
                Console.WriteLine(fraction);
            }
        }

        static void SolveEgyptianFractionByFibonacci(Fraction fraction)
        {
            var firstFraction =
                new Fraction(1, (int)Math.Ceiling((double)fraction.Denominator / fraction.Numerator));
            var secondFraction = fraction - firstFraction;
            fractions.Add(firstFraction);

            if(secondFraction.Numerator == 1)
            {
                fractions.Add(secondFraction);
            }
            else if(secondFraction.Numerator > 1)
            {
                SolveEgyptianFractionByFibonacci(secondFraction);                
            }
        }
    }
}
