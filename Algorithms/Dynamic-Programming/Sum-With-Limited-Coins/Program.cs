namespace Sum_With_Limited_Coins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static Dictionary<int, int> coinsBox =
            new Dictionary<int, int>();
        static List<int> currSum = new List<int>();
        static HashSet<string> foundSumCombinations = new HashSet<string>();
        static int count = 0;

        static void Main()
        {
            int[] coins = //new int[] { 50, 20, 20, 20, 20, 20, 10 };
                        //new int[] { 1, 2, 2, 5, 5, 10 };
                        //new int[] { 1, 2, 2, 5 };
                        new int[] { 1, 2, 2, 3, 3, 4, 6 };
            
            LoadWithCoins(ref coinsBox, coins);
            
            var currSum = new List<int>();
            FindCoinCombs(ref coins, //100);
                                    //13);
                                    //5);
                                    6);
            
            Console.WriteLine("Count: {0}", count);
        }

        static void FindCoinCombs(
            ref int[] coins,
            int targetSum, int tempSum = 0, int coinIndex = 0)
        {
            if (coinIndex < coins.Length
                && coinsBox[coins[coinIndex]] > 0)
            {
                coinsBox[coins[coinIndex]]--;
                currSum.Add(coins[coinIndex]);
                tempSum += coins[coinIndex];

                if (tempSum == targetSum)
                {
                    string foundSumCombination = string.Join(" + ", currSum);
                    
                    if (!foundSumCombinations.Contains(foundSumCombination))
                    {
                        count++;
                        foundSumCombinations.Add(foundSumCombination);
                        Console.WriteLine(foundSumCombination);
                    }
                }
                else if (tempSum < targetSum)
                {
                    // sums with the current coin
                    FindCoinCombs(ref coins, targetSum, tempSum, coinIndex);
                    // sums with the next coin
                    FindCoinCombs(ref coins, targetSum, tempSum, coinIndex + 1);
                }

                tempSum -= coins[coinIndex];
                currSum.RemoveAt(currSum.Count - 1);
                coinsBox[coins[coinIndex]]++;

                // if current coin is not needed more continue with next coin
                FindCoinCombs(ref coins, targetSum, tempSum, coinIndex + 1);
            }
        }

        static void LoadWithCoins(ref Dictionary<int, int> container, int[] coins)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                if (!container.ContainsKey(coins[i]))
                {
                    container.Add(coins[i], 0);
                }

                container[coins[i]]++;
            }

            // print coins in the container
            Console.WriteLine("Coins:");
            foreach (int coin in container.Keys)
            {
                Console.WriteLine("Value: {0} - Quantity: {1}", coin, container[coin]);
            }
            Console.WriteLine();
        }
    }
}
