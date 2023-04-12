using System.Collections.Immutable;
using TestRecord;

public class Programm
{
    private static void Main(string[] args)
    {
        var list = new List<string> { "nik1", "nik2" }.ToImmutableList().WithValueSemantics();

        var p1 = new Person("Arthur", 10, new List<string> { "nik1", "nik2" }.ToImmutableList().WithValueSemantics());
        var p2 = new Person("Arthur", 10, new List<string> { "nik1", "nik2" }.ToImmutableList().WithValueSemantics());
        foreach (var i in p1.immutableList){
            Console.WriteLine(i);
        }
        Console.WriteLine(p1 == p2);
        Console.WriteLine(p1);
        Console.ReadLine();




        var po1 = new OnlyPerson("Arthur", 10);
        var po2 = new OnlyPerson("Arthur", 10);

        //    Console.WriteLine(po1 == po2);
        //    Console.WriteLine(po1.Equals(po2));


        //    Console.WriteLine(p1 == p2);

        //    Console.WriteLine(p1.Equals(p2));
        //    Console.WriteLine(p1 == p2);
        //}
    }
}

public record class OnlyPerson(string Name, int Age);

public record class Person(string Name, int Age, IImmutableList<string> immutableList);


//public record PersonWithOverrideEquals(string Name, int Age, List<string> Nicknames)
//{
//    public override bool Equals(object obj)
//    {
//        return obj is PersonWithOverrideEquals other && Equals(other);
//    }
//}

//public record PersonWithOverrideEquals(string Name, int Age, List<string> Nicknames)
//{
//    public static bool operator ==(PersonWithOverrideEquals left, PersonWithOverrideEquals right)
//    {
//        if (ReferenceEquals(left, right)) return true;
//        if (left is null || right is null) return false;

//        return left.Name == right.Name &&
//               left.Age == right.Age &&
//               (left.Nicknames == null && right.Nicknames == null || left.Nicknames.SequenceEqual(right.Nicknames));
//    }

//    public static bool operator !=(PersonWithOverrideEquals left, PersonWithOverrideEquals right)
//    {
//        return !(left == right);
//    }
//}

