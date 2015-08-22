namespace DictionariesHashTablesSets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static MyDictionary<string, string> phonebook = new MyDictionary<string, string>();

        static bool AddToPhonebook(string name, string number)
        {
            if (!phonebook.ContainsKey(name))
            {
                phonebook.Add(name, number);
                return true;
            }

            return false;
        }

        static string SearchInPhonebook(string name)
        {
            var number = phonebook.Get(name);
            return number;
        }

        static void ParseConsoleInputForPhonebook() // 3. Phonebook Problem
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) { break; }
                int separator = input.IndexOf(":");
                if (separator < 0)
                {
                    Console.WriteLine(
                        "Invalid format. Right format is: \n"
                        + "<command name>: <param1> <param2> ...");
                    continue;
                }

                string commandName = input.Substring(0, separator++).Trim();
                string[] parameters = input
                    .Substring(separator, input.Length - separator)
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (commandName.Equals("search"))
                {
                    foreach (var name in parameters)
                    {
                        var phone = SearchInPhonebook(name);
                        if (string.IsNullOrEmpty(phone))
                        {
                            Console.WriteLine("Contact {0} doesn't exists.", name);
                        }

                        Console.WriteLine("{0} -> {1}", name, phone);
                    }
                }
                else if (commandName.Equals("add"))
                {
                    if (!AddToPhonebook(parameters[0], parameters[1]))
                    {
                        Console.WriteLine("{0} is already in the phonebook.", parameters[0]);
                    }
                }
                else { Console.WriteLine("Unsupported command."); }
            }
        }

        static IEnumerable<KeyValue<char, int>> CountSymbols(string text)
        {
            var counter = new MyDictionary<char, int>();
            foreach (var character in text)
            {
                int sequence = 0;
                if (counter.ContainsKey(character))
                {
                    sequence = counter.Get(character);
                }

                counter.AddOrReplace(character, ++sequence);
            }

            return counter;
        }

        static void ParseConsoleInputToCountSymbols() // 2. Count Symbols Problem
        {
            string text = Console.ReadLine();
            var chars = CountSymbols(text).OrderBy(c => c.Key);

            foreach (var sequence in chars)
            {
                Console.WriteLine("{0}: {1}", sequence.Key, sequence.Value);
            }
        }

        static void Main()
        {
            ParseConsoleInputToCountSymbols();
            //ParseConsoleInputForPhonebook();
        }
    }
}
