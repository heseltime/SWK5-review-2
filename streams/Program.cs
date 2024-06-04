using System.Text.Json;

class Program
{
    static void Main()
    {
        // Example 1: Testing streams ...
        using (Stream s = new FileStream("ascii.out", FileMode.Create))
        using (StreamWriter sw = new StreamWriter(s))
        {
            sw.WriteLine("Current Time {0}", DateTime.Now);
            for (int i = 0; i < 10; i++)
            {
                sw.Write("{0,4}", i);
            }
        }

        // Example 2: Serialization of a single person
        Person singlePerson = new Person("John", 30);

        using (Stream s1 = new FileStream("single_person.json", FileMode.Create))
        {
            JsonSerializer.Serialize(s1, singlePerson);
        }

        Person deserializedSinglePerson;
        using (Stream s1 = new FileStream("single_person.json", FileMode.Open))
        {
            deserializedSinglePerson = JsonSerializer.Deserialize<Person>(s1);
        }

        Console.WriteLine("Single Person -> Name: {0}, Age: {1}", deserializedSinglePerson.Name, deserializedSinglePerson.Age);

        // Example 3: Serialization of multiple persons
        List<Person> people = new List<Person>
        {
            new Person("John", 30),
            new Person("Jane", 25),
            new Person("Bob", 35)
        };

        using (Stream s2 = new FileStream("people.json", FileMode.Create))
        {
            JsonSerializer.Serialize(s2, people);
        }

        List<Person> deserializedPeople;
        using (Stream s2 = new FileStream("people.json", FileMode.Open))
        {
            deserializedPeople = JsonSerializer.Deserialize<List<Person>>(s2);
        }

        foreach (var person in deserializedPeople)
        {
            Console.WriteLine("Multiple People -> Name: {0}, Age: {1}", person.Name, person.Age);
        }
    }

    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
