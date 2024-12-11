// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using System.Reflection;
using System.Text.Json.Nodes;

Console.WriteLine("Hello, World!");
var anonObject = Anonymous.GetAnonymous();

foreach (PropertyInfo property in anonObject.GetType().GetProperties())
{
    var value = property.GetValue(anonObject);
    if (value != null && Anonymous.IsAnonymousType(value.GetType()))
    {
        Console.WriteLine($"{property.Name}:");
        foreach (PropertyInfo nestedProperty in value.GetType().GetProperties())
        {
            Console.WriteLine($"  {nestedProperty.Name}: {nestedProperty.GetValue(value)}");
        }
    }
    else if (value is Information information)
    {
        Console.WriteLine($"{property.Name}:");
        Console.WriteLine($"  salary: {information.Salary}");
        Console.WriteLine($"  word: {information.Word}");
        Console.WriteLine($"  country: {information.Country}");
    }
    else
    {
        Console.WriteLine($"{property.Name}: {value}");
    }
}



public class Anonymous
{
    public static object GetAnonymous()
    {
        return new { infor = new Information(200000, "nurse", "peru"), name = "jose", age = 22 };
    }

    public static bool IsAnonymousType(Type type)
    {
        return Attribute.IsDefined(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false)
               && type.IsGenericType && type.Name.Contains("AnonymousType")
               && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
               && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
    }

}

public class Information
{
    public int Salary { get; set; }
    public string ? Word { get; set; }
    public string ? Country { get; set; }

    public Information(int salary, string? word, string? country)
    {
        this.Salary = salary ;
        this.Word = word ;
        this.Country = country ;
    }

}