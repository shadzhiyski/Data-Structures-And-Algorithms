using System;
using System.Collections.Generic;

namespace Students_And_Courses
{
    class Program
    {
        private static SortedDictionary<string, SortedSet<Person>> studentsByCourses = 
            new SortedDictionary<string, SortedSet<Person>>();
        
        public static void ReadInput()
        {
            string input = null;
            while(true) 
            {
                input = Console.ReadLine();
                if (input.Equals("")) { break; }
                var personParams = ParseInputRow(input);
                AddPerson(personParams[0], personParams[1], personParams[2]);
            }
        }

        static IList<string> ParseInputRow(string rowData)
        {
            var personParams = rowData.Split('|');
            for (int i = 0; i < personParams.Length; i++)
            {
                personParams[i] = personParams[i].Trim();
            }

            return personParams;
        }

        static bool AddPerson(string firstName, string lastName, string courseName)
        {
            var person = new Person(firstName, lastName);
            if (!studentsByCourses.ContainsKey(courseName))
            {
                studentsByCourses.Add(courseName, new SortedSet<Person>());
            }

            return studentsByCourses[courseName].Add(person);
        }

        static void PrintStudentsByCourse()
        {
            foreach (var studentsByCourse in studentsByCourses)
            {
                Console.WriteLine(studentsByCourse.Key);
                foreach (var student in studentsByCourse.Value)
                {
                    Console.WriteLine("Name: {0}", student);
                }
            }
        }

        static void Main(string[] args)
        {
            ReadInput();
            PrintStudentsByCourse();
        }
    }
}
