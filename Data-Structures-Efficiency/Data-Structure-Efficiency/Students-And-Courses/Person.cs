using System;
namespace Students_And_Courses
{
    public class Person : IComparable<Person>
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompareTo(Person other)
        {
            return LastName.CompareTo(other.LastName);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
