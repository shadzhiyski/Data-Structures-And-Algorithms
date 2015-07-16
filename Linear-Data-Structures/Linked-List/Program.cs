namespace Linked_List
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new LinkedList<int>();
            
            list.Add(1);
            list.Add(348675);
            list.Add(34);
            list.Add(65);
            list.Add(8);

            list.Add(2);
            list.Add(17);
            list.Add(21);
            list.Add(21);
            list.Add(1);

            list.Add(8);
            list.Add(9);
            list.Add(8);
            list.Add(0);
            list.Add(-14);
            int i = 0;
            foreach (var item in list)
            {
                System.Console.WriteLine("{0} -> {1}", i++, item);
            }
            System.Console.WriteLine();

            System.Console.WriteLine(list.Remove(12));
            System.Console.WriteLine(list.Remove(18));
            System.Console.WriteLine();

            System.Console.WriteLine(list.LastIndexOf(8));
            System.Console.WriteLine(list.FirstIndexOf(8));
            System.Console.WriteLine(list.LastIndexOf(11));
            System.Console.WriteLine(list.FirstIndexOf(11));
            System.Console.WriteLine();

            i = 0;
            foreach (var item in list)
            {
                System.Console.WriteLine("{0} -> {1}", i++, item);
            }
        }
    }
}
