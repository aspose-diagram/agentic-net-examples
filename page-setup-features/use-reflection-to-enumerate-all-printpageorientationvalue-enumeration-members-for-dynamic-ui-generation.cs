using System.IO;
using System;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Obtain the Type object for the PrintPageOrientationValue enumeration
        Type enumType = typeof(PrintPageOrientationValue);

        // Use reflection to get all public static fields (the enum members)
        FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

        // Iterate through the fields and output their names and underlying values
        foreach (FieldInfo field in fields)
        {
            string name = field.Name;
            object rawValue = field.GetValue(null);
            int intValue = Convert.ToInt32(rawValue);
            Console.WriteLine($"{name} = {intValue}");
        }

        // The above list can be used to dynamically populate UI elements such as dropdowns.
    }
}
