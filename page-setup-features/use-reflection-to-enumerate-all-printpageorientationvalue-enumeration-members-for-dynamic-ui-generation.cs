using System.IO;
using System;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Obtain the enum type for PrintPageOrientationValue
        Type enumType = typeof(PrintPageOrientationValue);

        // Verify that the type is indeed an enum
        if (!enumType.IsEnum)
        {
            Console.WriteLine("The specified type is not an enumeration.");
            return;
        }

        // Enumerate enum members using the Enum helper methods
        Console.WriteLine("Enum members (using Enum.GetNames):");
        foreach (string name in Enum.GetNames(enumType))
        {
            // Parse the name back to its numeric value
            int numericValue = (int)Enum.Parse(enumType, name);
            Console.WriteLine($"{name} = {numericValue}");
        }

        // Enumerate enum members directly via reflection on the static fields
        Console.WriteLine("\nEnum members (using reflection):");
        FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            string name = field.Name;
            int numericValue = (int)field.GetValue(null);
            Console.WriteLine($"{name} = {numericValue}");
        }
    }
}
