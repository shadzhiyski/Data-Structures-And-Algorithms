namespace Fractional_Knapsack
{
    using System;

    class Item
    {
        public int Value { get; set; }

        public int Quantity { get; set; }

        public Item(int value, int quantity)
        {
            Value = value;
            Quantity = quantity;
        }
    }
}
