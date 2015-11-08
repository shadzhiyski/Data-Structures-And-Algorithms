namespace Fractional_Knapsack
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            int capacity =  int.Parse(Console.ReadLine().Split(' ')[1]);
            Item[] item = ReadItems();
            int[] takenItemQuantity = SolveContinuousKnapsack(item, capacity);
            PrintItems(item, takenItemQuantity);
        }

        public static Item[] ReadItems()
        {
            int itemsCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            Item[] item = new Item[itemsCount];
            for (int i = 0; i < itemsCount; i++)
            {
                string inputRow = Console.ReadLine();
                string[] inputParams = inputRow
                    .Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                int itemValue = int.Parse(inputParams[0].Trim());
                int itemCount = int.Parse(inputParams[1].Trim());
                item[i] = new Item(itemValue, itemCount);
            }

            return item.OrderByDescending(i => i.Value / i.Quantity).ToArray();
        }

        private static void PrintItems(Item[] item, int[] takenItemQuantity)
        {
            double pricesSum = 0;
            for (int i = 0; i < takenItemQuantity.Length; i++)
            {
                if(takenItemQuantity[i] == 0) { break; }
                int leftItemQuantity = item[i].Quantity;
                double itemQuantityRatio = 
                    ((double)takenItemQuantity[i] / (takenItemQuantity[i] + leftItemQuantity));
                Console.WriteLine(
                    "Take {0:0.00}% of item with price {1} and weight {2:0.00}",
                    itemQuantityRatio * 100,
                    item[i].Value,
                    takenItemQuantity[i]);

                pricesSum += item[i].Value * itemQuantityRatio;
            }

            Console.WriteLine("Total price: {0:0.00}", pricesSum);
        }

        private static int[] SolveContinuousKnapsack(Item[] item, int freeQuantity)
        {
            int itemIndex = 0;
            int[] takenItemQuantity = new int[item.Length];
            while(freeQuantity-- > 0)
            {
                takenItemQuantity[itemIndex]++;
                if(--item[itemIndex].Quantity == 0
                    && ++itemIndex == item.Length) { break; }
            }

            return takenItemQuantity;
        }
    }
}
