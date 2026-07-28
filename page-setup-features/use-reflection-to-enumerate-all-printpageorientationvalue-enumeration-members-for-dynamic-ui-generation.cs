using System.IO;
using System;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Obtain the enum type via reflection
        Type enumType = typeof(PrintPageOrientationValue);

        // Enumerate all public static fields (the enum members)
        FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (FieldInfo field in fields)
        {
            string name = field.Name;
            int value = (int)field.GetValue(null);
            Console.WriteLine($"{name} = {value}");
        }
    }
}
