namespace Sum_With_Unlimited_Coins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static int count = 0;

        static void Main()
        {
            int[] coins = new int[] { 1, 2, 5, 10, 20, 50, 100 };
                                    //{ 1, 2, 5, 10 };
            CountSumCombs(ref coins, 100);
                            //13);

            Console.WriteLine(count);
        }

        static void CountSumCombs(ref int[] coins, int targetSum, int tempSum = 0, int coinIndex = 0)
        {
            if (coinIndex < coins.Length)
            {
                tempSum += coins[coinIndex];

                if (tempSum < targetSum) { CountSumCombs(ref coins, targetSum, tempSum, coinIndex); }
                else if(tempSum == targetSum)
                {
                    count++;
                    return;
                }

                tempSum -= coins[coinIndex];
                coinIndex++;
                CountSumCombs(ref coins, targetSum, tempSum, coinIndex);
            }
        }
    }
}
